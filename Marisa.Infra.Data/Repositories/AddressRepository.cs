using System.Linq;
using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Address?> GetAddressById(Guid? id)
        {
            var address = await _context
                .Address
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return address;
        }

        public async Task<List<Address>?> GetAllAddressByUserId(Guid? userId)
        {
            var address = await _context
                .Address
                .Where(u => u.UserId == userId)
                .ToListAsync();

            return address;
        }

        public async Task<Address?> GetAddressByIdWithUser(Guid? id)
        {
            var stringEmpty = string.Empty;

            var address = await _context
                .Address
                .Where(u => u.Id == id)
                .Select(s => new Address(s.Id, s.AddressNickname, s.AddressType, null, null, null,
                null, null, null, null, null, null, s.UserId,
                s.User != null 
                ? new User(s.User.Id, s.User.Name, null, null, s.User.Cpf, s.User.Gender, null,
                null, null, null, null) : null))
                .FirstOrDefaultAsync();

            return address;
        }
    }
}
