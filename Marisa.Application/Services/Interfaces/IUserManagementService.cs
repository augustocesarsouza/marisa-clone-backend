using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<ResultService<UserDTO>> Create(UserDTO? userDTO);
    }
}
