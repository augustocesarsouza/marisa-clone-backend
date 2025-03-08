using AutoMapper;
using Marisa.Application.DTOs;
using Marisa.Application.DTOs.Validations.Interfaces;
using Marisa.Application.Services.Interfaces;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.CloudinaryConfigClass;
using Marisa.Infra.Data.UtilityExternal.Interface;

namespace Marisa.Application.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCreateDTOValidator _userCreateDTOValidator;
        private readonly IUserCreateAccountFunction _userCreateAccountFunction;
        private readonly ICloudinaryUti _cloudinaryUti;

        public UserManagementService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork,
            IUserCreateDTOValidator userCreateDTOValidator, IUserCreateAccountFunction userCreateAccountFunction,
            ICloudinaryUti cloudinaryUti)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userCreateDTOValidator = userCreateDTOValidator;
            _userCreateAccountFunction = userCreateAccountFunction;
            _cloudinaryUti = cloudinaryUti;
        }

        public async Task<ResultService<UserDTO>> Create(UserDTO? userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<UserDTO>("userDTO is null");

            var validationUser = _userCreateDTOValidator.ValidateDTO(userDTO);

            if (!validationUser.IsValid)
                return ResultService.RequestError<UserDTO>("validation error check the information", validationUser);

            string password = userDTO.Password;

            byte[] saltBytes = _userCreateAccountFunction.GenerateSalt();
            // Hash the password with the salt
            string hashedPassword = _userCreateAccountFunction.HashPassword(password, saltBytes);
            string base64Salt = Convert.ToBase64String(saltBytes);

            byte[] retrievedSaltBytes = Convert.FromBase64String(base64Salt);

            try
            {
                await _unitOfWork.BeginTransaction();

                Guid idUser = Guid.NewGuid();

                User userCreate = new User();
                string birthDateString = userDTO.BirthDateString;

                var birthDateSlice = birthDateString.Split('/');
                var day = birthDateSlice[0];
                var month = birthDateSlice[1];
                var year = birthDateSlice[2];

                var birthDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                var birthDateUtc = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

                if (userDTO.UserImage.Length > 0)
                {
                    CloudinaryCreate result = await _cloudinaryUti.CreateMedia(userDTO.UserImage, "imgs-backend-frontend-marisa/img-user", 320, 320);

                    if (result.ImgUrl == null || result.PublicId == null)
                    {
                        await _unitOfWork.Rollback();
                        return ResultService.Fail<UserDTO>("error when create ImgPerfil");
                    }

                    var img = result.ImgUrl;

                    userCreate = new User(idUser, userDTO.Name, userDTO.Email, birthDateUtc, userDTO.Cpf, userDTO.Gender, 
                        userDTO.CellPhone, userDTO.Telephone, hashedPassword, base64Salt, img);
                }
                else
                {
                    userCreate = new User(idUser, userDTO.Name, userDTO.Email, birthDateUtc, userDTO.Cpf, userDTO.Gender, 
                        userDTO.CellPhone, userDTO.Telephone, hashedPassword, base64Salt, "");
                }

                var data = await _userRepository.CreateAsync(userCreate);

                if (data == null)
                    return ResultService.Fail<UserDTO>("error when create user null value");

                await _unitOfWork.Commit();

                data.SetPasswordHash("");
                data.SetSalt("");

                var userDTOMap = _mapper.Map<UserDTO>(data);

                return ResultService.Ok(userDTOMap);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDTO>(ex.Message);
            }
        }
    }
}
