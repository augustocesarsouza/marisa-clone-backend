using AutoMapper;
using Marisa.Application.CodeRandomUser.Interface;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.DTOs.Validations.UserValidator;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.CloudinaryConfigClass;
using Marisa.Infra.Data.UtilityExternal.Interface;
using System.Text;

namespace Marisa.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCreateDTOValidator _userCreateDTOValidator;
        private readonly IUserUpdateProfileDTOValidator _userUpdateProfileDTOValidator;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;
        private readonly IChangePasswordUserDTOValidator _changePasswordUserDTOValidator;
        private readonly IUserChangePasswordTokenDTOValidator _userChangePasswordTokenDTOValidator;
        private readonly ICloudinaryUti _cloudinaryUti;
        private readonly ICodeRandomDictionary _codeRandomDictionary;
        private readonly IQuantityAttemptChangePasswordDictionary _quantityAttemptChangePasswordDictionary;
        private readonly ITokenGeneratorUser _tokenGeneratorUser;
        private readonly ISendEmailUser _sendEmailUser;

        public UserManagementService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IUserCreateDTOValidator userCreateDTOValidator, IUserUpdateProfileDTOValidator userUpdateProfileDTOValidator,
            IUserCreateAccountFunction userCreateAccountFunction, ICloudinaryUti cloudinaryUti, IChangePasswordUserDTOValidator changePasswordUserDTOValidator,
            IUserChangePasswordTokenDTOValidator userChangePasswordTokenDTOValidator, ICodeRandomDictionary codeRandomDictionary, 
            IQuantityAttemptChangePasswordDictionary quantityAttemptChangePasswordDictionary, ITokenGeneratorUser tokenGeneratorUser,
            ISendEmailUser sendEmailUser)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userCreateDTOValidator = userCreateDTOValidator;
            _userUpdateProfileDTOValidator = userUpdateProfileDTOValidator;
            _userCreateAccountFunction = userCreateAccountFunction;
            _changePasswordUserDTOValidator = changePasswordUserDTOValidator;
            _userChangePasswordTokenDTOValidator = userChangePasswordTokenDTOValidator;
            _cloudinaryUti = cloudinaryUti;
            _codeRandomDictionary = codeRandomDictionary;
            _quantityAttemptChangePasswordDictionary = quantityAttemptChangePasswordDictionary;
            _tokenGeneratorUser = tokenGeneratorUser;
            _sendEmailUser = sendEmailUser;
        }

        public async Task<ResultService<CreateUserDTO>> Create(UserDTO? userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<CreateUserDTO>("userDTO is null");

            var validationUser = _userCreateDTOValidator.ValidateDTO(userDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<CreateUserDTO>("validation error check the information", validationUser);

            try
            {
                await _unitOfWork.BeginTransaction();

                var tokenForCreation = userDTO.TokenForCreation ?? 0;
                var email = userDTO.Email ?? "";

                var valueContain = _codeRandomDictionary.Container(email, tokenForCreation);

                var createUserDTO = new CreateUserDTO(false, null);

                //if (!valueContain)
                //{
                //    // token errado retorna um erro personalizado da para criar um "DTO" que contem tipo Se o token está certo e o "DTO"

                //    return ResultService.Ok(createUserDTO);
                //}

                string password = userDTO.Password ?? "";

                byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();
                // Hash the password with the salt
                string hashedPassword = _userCreateAccountFunction.HashPassword(password, saltBytes);
                string base64Salt = Convert.ToBase64String(saltBytes);

                byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

                Guid idUser = Guid.NewGuid();

                User userCreate = new User();
                string birthDateString = userDTO.BirthDateString ?? "";

                var birthDateSlice = birthDateString.Split('/');
                var day = birthDateSlice[0];
                var month = birthDateSlice[1];
                var year = birthDateSlice[2];
                
                {/* Quando fazer o Product no backend de algum jeito colocar uma variavel que saiba qual é o 'blusa, acessorios-e-bolsas, tricos' só com Product */}

                var birthDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

                if (userDTO.UserImage?.Length > 0)
                {
                    CloudinaryCreate result = await _cloudinaryUti.CreateMedia(userDTO.UserImage, "imgs-backend-frontend-marisa/img-user", 320, 320);

                    if (result.ImgUrl == null || result.PublicId == null)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<CreateUserDTO>("error when create ImgPerfil");
                    }

                    var img = result.ImgUrl;

                    userCreate = new User(idUser, userDTO.Name ?? "", userDTO.Email ?? "", birthDateUtc, userDTO.Cpf ?? "", userDTO.Gender ?? 'o',
                        userDTO.CellPhone ?? "", userDTO.Telephone ?? "", hashedPassword, base64Salt, img);
                }
                else
                {
                    userCreate = new User(idUser, userDTO.Name ?? "", userDTO.Email ?? "", birthDateUtc, userDTO.Cpf ?? "", userDTO.Gender ?? 'o',
                        userDTO.CellPhone ?? "", userDTO.Telephone ?? "", hashedPassword, base64Salt, "");
                }

                var data = await _userRepository.CreateAsync(userCreate);

                if (data == null)
                    return ResultService.Fail<CreateUserDTO>("error when create user null value");

                await _unitOfWork.Commit();

                data.SetPasswordHash("");
                data.SetSalt("");

                var userDTOMap = _mapper.Map<UserDTO>(data);
                createUserDTO.SetTokenIsValid(true);
                createUserDTO.SetUserDTO(userDTOMap);

                return ResultService.Ok(createUserDTO);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CreateUserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserDTO>> UpateProfile(UserDTO? userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<UserDTO>("userDTO is null");

            var validationUser = _userUpdateProfileDTOValidator.ValidateDTO(userDTO);
            
            if (!validationUser.IsValid)
                return ResultService.RequestError<UserDTO>("validation error check the information", validationUser);

            try
            {
                var userId = userDTO.Id;

                var userToUpdate = await _userRepository.GetUserById(userId);

                if (userToUpdate == null)
                    return ResultService.Fail<UserDTO>("Error user Not Found");

                string birthDateString = userDTO.BirthDateString ?? "";

                var birthDateSlice = birthDateString.Split('/');
                var day = birthDateSlice[0];
                var month = birthDateSlice[1];
                var year = birthDateSlice[2];

                var birthDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

                userToUpdate.SetValueUpdateUser(userDTO.Name ?? "", birthDateUtc, userDTO.Gender ?? 'o', userDTO.CellPhone ?? "", userDTO.Telephone ?? "");

                var userUpdate = await _userRepository.UpdateAsync(userToUpdate);

                if(userUpdate == null)
                    return ResultService.Fail<UserDTO>("Error userUpdate it is null");

                return ResultService.Ok(new UserDTO(userUpdate.Id, userUpdate.Name, null, userUpdate.BirthDate, userUpdate.Cpf, userUpdate.Gender, userUpdate.CellPhone,
                    userUpdate.Telephone, null, null, null));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<ChangePasswordUserReturnDTO>> ChangePassword(ChangePasswordUser? changePasswordUser)
        {
            if (changePasswordUser == null)
                return ResultService.Fail<ChangePasswordUserReturnDTO>("userDTO is null");

            var validationUser = _changePasswordUserDTOValidator.ValidateDTO(changePasswordUser);

            if (!validationUser.IsValid)
                return ResultService.RequestError<ChangePasswordUserReturnDTO>("validation error check the information", validationUser);

            try
            {
                var userId = Guid.Parse(changePasswordUser.UserId ?? "");

                var (value, timeLeft) = _quantityAttemptChangePasswordDictionary.GetKey(userId.ToString());
                var nextQuantity = value + 1;

                if (nextQuantity == 4)
                    return ResultService.Fail(new ChangePasswordUserReturnDTO(false, false, nextQuantity, timeLeft));

                var userToUpdate = await _userRepository.GetUserByIdToChangePassword(userId);

                if (userToUpdate == null)
                    return ResultService.Fail<ChangePasswordUserReturnDTO>("Error user Not Found");

                var password = changePasswordUser.CurrentPassword ?? "";
                var passwordHash = userToUpdate.GetPasswordHash();
                var salt = userToUpdate.GetSalt();

                if (passwordHash == null || salt == null)
                    return ResultService.Fail<ChangePasswordUserReturnDTO>("Error password hash or salt is null");

                string storedHashedPassword = passwordHash;
                byte[] storedSaltBytes = Convert.FromBase64String(salt);
                string enteredPassword = password;

                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

                byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
                Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

                var userReturnToFrontend = new UserDTO();

                string enteredPasswordHash = _userCreateAccountFunction.HashPassword(enteredPassword, storedSaltBytes);

                if (enteredPasswordHash == storedHashedPassword)
                {
                    var newPassword = changePasswordUser.ConfirmNewPassword ?? "";

                    byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();

                    string hashedPassword = _userCreateAccountFunction.HashPassword(newPassword, saltBytes);
                    string base64Salt = Convert.ToBase64String(saltBytes);

                    userToUpdate.SetValuePasswordHashAndSalt(hashedPassword, base64Salt);

                    var userUpdate = await _userRepository.UpdateAsync(userToUpdate);

                    if (userUpdate == null)
                        return ResultService.Fail<ChangePasswordUserReturnDTO>("Error userUpdate it is null");

                    return ResultService.Ok(new ChangePasswordUserReturnDTO(true, true, 0, TimeSpan.Zero));
                }
                else
                {
                    if (nextQuantity == 3)
                    {
                        _quantityAttemptChangePasswordDictionary.AddTimer(userId.ToString(), nextQuantity);
                        return ResultService.Fail(new ChangePasswordUserReturnDTO(false, false, nextQuantity, timeLeft));
                    }

                    _quantityAttemptChangePasswordDictionary.Add(userId.ToString(), nextQuantity);

                    return ResultService.Fail(new ChangePasswordUserReturnDTO(false, false, nextQuantity, timeLeft));
                }
            }
            catch (Exception ex)
            {
                return ResultService.Fail<ChangePasswordUserReturnDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserTokenSentEmail>> SendTokenChangePassword(string email)
        {
            var userTokenSentEmail = new UserTokenSentEmail(false, "");

            try
            {
                var user = await _userRepository.GetIfUserExistEmail(email);

                if (user == null)
                    return ResultService.Fail(userTokenSentEmail);

                var tokenResult = _tokenGeneratorUser.GeneratorTokenUrlChangeEmail(user);
                var token = tokenResult.Data.Acess_Token;
                var sendTokenEmail = _sendEmailUser.SendUrlChangePassword(user, token);

                userTokenSentEmail.SetMessage(sendTokenEmail.Message ?? "");

                if (!sendTokenEmail.IsSucess)
                    return ResultService.Fail(userTokenSentEmail);

                userTokenSentEmail.SetTokenSentEmail(true);
                userTokenSentEmail.SetMessage("token sent sucessfully");

                return ResultService.Ok(userTokenSentEmail);
            }
            catch (Exception ex)
            {
                userTokenSentEmail.SetMessage(ex.Message);
                return ResultService.Fail(userTokenSentEmail);
            }
        }

        public async Task<ResultService<UserDTO>> ChangePasswordUserToken(UserChangePasswordToken userChangePasswordToken)
        {
            var validateDTO = _userChangePasswordTokenDTOValidator.ValidateDTO(userChangePasswordToken);

            if (!validateDTO.IsValid)
                return ResultService.RequestError<UserDTO>("validation error check the information", validateDTO);

            try
            {
                var userId = Guid.Parse(userChangePasswordToken.UserId ?? "");
                var user = await _userRepository.GetUserById(userId);

                if (user == null)
                    return ResultService.Fail<UserDTO>("error DTO Is Null");

                var newPassword = userChangePasswordToken.NewPassword ?? "";

                byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();

                string hashedPassword = _userCreateAccountFunction.HashPassword(newPassword, saltBytes);
                string base64Salt = Convert.ToBase64String(saltBytes);

                user.SetValuePasswordHashAndSalt(hashedPassword, base64Salt);

                var userUpdate = await _userRepository.UpdateAsync(user);

                if (userUpdate == null)
                    return ResultService.Fail<UserDTO>("Error userUpdate it is null");

                var userDTOMap = _mapper.Map<UserDTO>(userUpdate);

                return ResultService.Ok(userDTOMap);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }
    }
}
