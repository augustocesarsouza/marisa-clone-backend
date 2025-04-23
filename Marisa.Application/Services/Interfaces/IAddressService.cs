using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IAddressService
    {
        public Task<ResultService<AddressDTO>> GetAddressDTOById(Guid? id);
        public Task<ResultService<List<AddressDTO>>> GetAllAddressByUserId(Guid? userId);
        public Task<ResultService<AddressDTO>> Create(AddressDTO? addressDTO);
        public Task<ResultService<AddressConfirmCodeEmailDTO>> VerifyCodeEmailToAddNewAddress(UserConfirmCodeEmailDTO userConfirmCodeEmailDTO);
        public Task<ResultService<AddressDTO>> Update(AddressDTO? addressDTO);
        public Task<ResultService<AddressDTO>> DeleteAddress(Guid? addressId);
    }
}
