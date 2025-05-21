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

        public async Task<Address?> GetAddressFirstRegister()
        {
            var address = await _context
                .Address
                .FirstOrDefaultAsync();

            return address;
        }

        public async Task<Address?> GetAddressSetAsPrimary()
        {
            var address = await _context
                .Address
                .Where(x => x.MainAddress == true)
                .FirstOrDefaultAsync();

            return address;
        }

        public async Task<Address?> CheckIfUserAlreadyHasARegisteredAddress(Guid? userId)
        {
            var address = await _context
                .Address
                .Where(u => u.UserId == userId)
                .Select(x => new Address(x.Id, null, null, null, null, null, null, null, null, null, null, null, x.MainAddress))
                .FirstOrDefaultAsync();

            return address;
        }

        public async Task<List<Address>?> GetAllAddressByUserId(Guid? userId)
        {
            var address = await _context
                .Address
                .Where(u => u.UserId == userId)
                .Select(s => new Address(s.Id, s.AddressNickname, s.AddressType, s.RecipientName, s.ZipCode, s.Street,
                s.Number, s.Complement, s.Neighborhood, s.City, s.State, s.RecipientName, s.MainAddress, s.UserId,
                s.User != null
                ? new User(s.User.Id, s.User.Name, null, null, s.User.Cpf, s.User.Gender, s.User.CellPhone,
                null, null, null, null) : null))
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
                null, null, null, null, null, null, s.MainAddress, s.UserId,
                s.User != null 
                ? new User(s.User.Id, s.User.Name, null, null, s.User.Cpf, s.User.Gender, null,
                null, null, null, null) : null))
                .FirstOrDefaultAsync();

            return address;
        }
    }
}
