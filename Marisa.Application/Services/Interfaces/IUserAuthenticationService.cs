using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IUserAuthenticationService
    {
        public Task<ResultService<UserDTO>> GetByIdInfoUser(string userId);
        public Task<ResultService<UserLoginDTO>> Login(string email, string password);
        public Task<ResultService<UserLoginDTO>> VerifyPasswordUser(string phone, string password);
        public ResultService<CodeSendEmailUserDTO> SendCodeEmail(UserDTO? userDTO);
        public Task<ResultService<UserConfirmCodeEmailDTO>> VerifyCodeEmail(UserConfirmCodeEmailDTO userConfirmCodeEmailDTO);
    }
}
