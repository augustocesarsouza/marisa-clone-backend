using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<ResultService<CreateUserDTO>> Create(UserDTO? userDTO);
        public Task<ResultService<UserDTO>> UpateProfile(UserDTO? userDTO);
        public Task<ResultService<ChangePasswordUserReturnDTO>> ChangePassword(ChangePasswordUser? changePasswordUser);
        public Task<ResultService<UserDTO>> ChangePasswordUserToken(UserChangePasswordToken userChangePasswordToken);
        public Task<ResultService<UserTokenSentEmail>> SendTokenChangePassword(string email);
    }
}
