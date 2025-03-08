using AutoMapper;
using Marisa.Application.CodeRandomUser;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Authentication;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.UtilityExternal.Interface;
using System.Text;

namespace Marisa.Application.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGeneratorUser _tokenGeneratorUser;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;
        private readonly ISendEmailUser _sendEmailUser;
        private readonly IUserSendCodeEmailDTOValidator _userSendCodeEmailDTOValidator;
        private readonly static CodeRandomDictionary _codeRandomDictionary = new();

        public UserAuthenticationService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, ITokenGeneratorUser tokenGeneratorUser,
            IUserCreateAccountFunction userCreateAccountFunction, ISendEmailUser sendEmailUser, IUserSendCodeEmailDTOValidator userSendCodeEmailDTOValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenGeneratorUser = tokenGeneratorUser;
            _userCreateAccountFunction = userCreateAccountFunction;
            _sendEmailUser = sendEmailUser;
            _userSendCodeEmailDTOValidator = userSendCodeEmailDTOValidator;
        }
       
        public async Task<ResultService<UserDTO>> GetByIdInfoUser(string userId)
        {
            try
            {
                var infoUser = await _userRepository.GetUserByIdInfoUser(Guid.Parse(userId));

                if (infoUser == null)
                    return ResultService.Fail<UserDTO>("Error user null");

                return ResultService.Ok(_mapper.Map<UserDTO>(infoUser));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserLoginDTO>> Login(string email, string password)
        {
            try
            {
                // In a real scenario, you would retrieve these values from your database
                //var user = _dbContext.Usertests.Where(x => x.Mobile == verify.MobileNo).Select(x => x).FirstOrDefault();
                var user = await _userRepository.GetUserInfoToLogin(email);

                if (user == null)
                    return ResultService.Fail<UserLoginDTO>("Error user null");

                var passwordHash = user.GetPasswordHash();
                var salt = user.GetSalt();

                if (passwordHash == null || salt == null)
                    return ResultService.Fail<UserLoginDTO>("Error password hash or salt is null");

                string storedHashedPassword = passwordHash;// "hashed_password_from_database";
                                                                     //string storedSalt = user.Salt; //"salt_from_database";
                byte[] storedSaltBytes = Convert.FromBase64String(salt);
                string enteredPassword = password; //"user_entered_password";

                // Convert the stored salt and entered password to byte arrays
                // byte[] storedSaltBytes = Convert.FromBase64String(user.Salt);
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

                // Concatenate entered password and stored salt
                byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
                Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

                var userReturnToFrontend = new UserDTO();

                // Hash the concatenated value
                string enteredPasswordHash = _userCreateAccountFunction.HashPassword(enteredPassword, storedSaltBytes);

                // Compare the entered password hash with the stored hash
                if (enteredPasswordHash == storedHashedPassword)
                {
                    userReturnToFrontend.SetName(user.Name);
                    userReturnToFrontend.SetEmail(user.Email);
                    userReturnToFrontend.SetId(user.GetId());
                    userReturnToFrontend.SetEmail(user.Email);

                    var token = _tokenGeneratorUser.Generator(user);

                    if (!token.IsSucess)
                        return ResultService.Fail<UserLoginDTO>(token.Message ?? "error validate token");

                    userReturnToFrontend.SetToken(token.Data.Acess_Token);

                    return ResultService.Ok(new UserLoginDTO(true, userReturnToFrontend));
                }
                else
                {
                    return ResultService.Fail(new UserLoginDTO(false, userReturnToFrontend));
                }
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserLoginDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserLoginDTO>> VerifyPasswordUser(string phone, string password)
        {
            return await VerifyPasswordUserIsValid(phone, password);
        }

        public async Task<ResultService<UserPasswordUpdateDTO>> ChangePasswordUser(UserChangePasswordDTO userChangePasswordDTO)
        {
            try
            {
                //var result = await VerifyPasswordUserIsValid(userChangePasswordDTO.Phone, userChangePasswordDTO.Password);

                //if (!result.IsSucess)
                //{
                //    return result;
                //}

                var user = await _userRepository.GetUserByEmailInfoUpdate(userChangePasswordDTO.Email);

                if (user == null)
                    return ResultService.Fail<UserPasswordUpdateDTO>("Error user null");

                string newPassword = userChangePasswordDTO.ConfirmPassword;

                byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();
                // Hash the password with the salt
                string hashedPassword = _userCreateAccountFunction.HashPassword(newPassword, saltBytes);
                string base64Salt = Convert.ToBase64String(saltBytes);

                //byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

                user.SetPasswordHash(hashedPassword);
                user.SetSalt(base64Salt);

                var userUpdate = await _userRepository.UpdateAsync(user);

                if (userUpdate == null)
                    return ResultService.Fail<UserPasswordUpdateDTO>("Error userUpdate it is null");

                return ResultService.Ok(new UserPasswordUpdateDTO(true));
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserPasswordUpdateDTO>(ex.Message);
            }
        }
       
        private async Task<ResultService<UserLoginDTO>> VerifyPasswordUserIsValid(string email, string password)
        {
            try
            {
                // In a real scenario, you would retrieve these values from your database
                //var user = _dbContext.Usertests.Where(x => x.Mobile == verify.MobileNo).Select(x => x).FirstOrDefault();
                var user = await _userRepository.GetUserInfoToLogin(email);

                if (user == null)
                    return ResultService.Fail<UserLoginDTO>("Error user null");

                var passwordHash = user.GetPasswordHash();
                var salt = user.GetSalt();

                if (passwordHash == null || salt == null)
                    return ResultService.Fail<UserLoginDTO>("Error password hash or salt is null");

                string storedHashedPassword = passwordHash;// "hashed_password_from_database";
                                                                     //string storedSalt = user.Salt; //"salt_from_database";
                byte[] storedSaltBytes = Convert.FromBase64String(salt);
                string enteredPassword = password; //"user_entered_password";

                // Convert the stored salt and entered password to byte arrays
                // byte[] storedSaltBytes = Convert.FromBase64String(user.Salt);
                byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

                // Concatenate entered password and stored salt
                byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
                Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

                var userReturnToFrontend = new UserDTO();

                // Hash the concatenated value
                string enteredPasswordHash = _userCreateAccountFunction.HashPassword(enteredPassword, storedSaltBytes);

                // Compare the entered password hash with the stored hash
                if (enteredPasswordHash == storedHashedPassword)
                {
                    return ResultService.Ok(new UserLoginDTO(true, userReturnToFrontend));
                }
                else
                {
                    return ResultService.Fail(new UserLoginDTO(false, userReturnToFrontend));
                }
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserLoginDTO>(ex.Message);
            }
        }

        public ResultService<CodeSendEmailUserDTO> SendCodeEmail(UserDTO? userDTO)
        {
            try
            {
                if (userDTO == null)
                    return ResultService.Fail<CodeSendEmailUserDTO>("Error DTO user null");

                var resultValidationUserDTO = _userSendCodeEmailDTOValidator.ValidateDTO(userDTO);

                if (!resultValidationUserDTO.IsValid)
                    return ResultService.RequestError<CodeSendEmailUserDTO>("validation error check the information", resultValidationUserDTO);

                if (userDTO.Email == null)
                    return ResultService.Fail<CodeSendEmailUserDTO>("User Not Provided Email");

                var email = userDTO.Email;

                var user = _mapper.Map<User>(userDTO);

                var randomCode = GerarNumeroAleatorio();
                _codeRandomDictionary.Add(email, randomCode);

                var resultSend = _sendEmailUser.SendCodeRandom(user, randomCode);

                if(!resultSend.IsSucess)
                    return ResultService.Fail(new CodeSendEmailUserDTO(null, false, false));

                var codeSend = new CodeSendEmailUserDTO(randomCode.ToString(), true, false);

                return ResultService.Ok(codeSend);
            }
            catch (Exception ex)
            {
                return ResultService.Fail<CodeSendEmailUserDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserConfirmCodeEmailDTO>> VerifyCodeEmail(UserConfirmCodeEmailDTO userConfirmCodeEmailDTO)
        {
            try
            {
                if (userConfirmCodeEmailDTO == null)
                    return ResultService.Fail<UserConfirmCodeEmailDTO>("DTO NULL");

                if (userConfirmCodeEmailDTO.Code == null || userConfirmCodeEmailDTO.Email == null)
                    return ResultService.Fail<UserConfirmCodeEmailDTO>("DTO property Is Null");

                var email = userConfirmCodeEmailDTO.Email;

                if (_codeRandomDictionary.Container(email, int.Parse(userConfirmCodeEmailDTO.Code)))
                {
                    await _unitOfWork.BeginTransaction();

                    var userConfirmCodeEmail = new UserConfirmCodeEmailDTO(null, null, userConfirmCodeEmailDTO.Email, true);

                    _codeRandomDictionary.Remove(email);

                    await _unitOfWork.Commit();
                    return ResultService.Ok(userConfirmCodeEmail);
                }
                else
                {
                    var userConfirmCodeEmail = new UserConfirmCodeEmailDTO(null, null, userConfirmCodeEmailDTO.Email, false);

                    return ResultService.Ok(userConfirmCodeEmail);
                }
            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserConfirmCodeEmailDTO>(ex.Message);
            }
        }

        private static int GerarNumeroAleatorio()
        {
            Random random = new Random();
            return random.Next(100000, 1000000);
        }
    }
}
