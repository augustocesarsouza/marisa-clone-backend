using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        public Task<Address?> GetAddressById(Guid? id);
        public Task<Address?> GetAddressByIdWithUser(Guid? id);
    }
}
