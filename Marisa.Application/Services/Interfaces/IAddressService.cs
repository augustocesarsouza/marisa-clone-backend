using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<ResultService<AddressDTO>> GetAddressDTOById(Guid? id);
        public Task<ResultService<AddressDTO>> Create(AddressDTO? addressDTO);
    }
}
