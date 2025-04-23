using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        public Task<Address?> GetAddressById(Guid? id);
        public Task<Address?> GetAddressFirstRegister();
        public Task<Address?> CheckIfUserAlreadyHasARegisteredAddress(Guid? userId);
        public Task<List<Address>?> GetAllAddressByUserId(Guid? userId);
        public Task<Address?> GetAddressByIdWithUser(Guid? id);
    }
}
