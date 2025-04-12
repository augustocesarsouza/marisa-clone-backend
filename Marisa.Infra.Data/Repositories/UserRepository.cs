using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(Guid? id)
        {
            var user = await _context
                 .Users
                 .Where(u => u.Id == id)
                 .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByIdToChangePassword(Guid? id)
        {
            var user = await _context
                 .Users
                 .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context
                 .Users
                 .Where(u => u.Email == email)
                 .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByIdInfoUser(Guid id)
        {
            var user = await _context
                .Users
            .Where(u => u.Id == id)
                .Select(x => new User(x.Id, x.Name, x.Email, x.BirthDate, x.Cpf, x.Gender, x.CellPhone, x.Telephone, "", "", x.UserImage))
            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetInfoToUpdateProfile(Guid id)
        {
            var user = await _context
                .Users
            .Where(u => u.Id == id)
                .Select(x => new User(Guid.Empty, x.Name, "", x.BirthDate, x.Cpf, x.Gender, x.CellPhone, x.Telephone, "", "", ""))
            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByIdCheckIfExist(Guid id)
        {
            var user = await _context
                .Users
            .Where(u => u.Id == id)
                .Select(x => new User(x.Id, x.Name, "", DateTime.Now, "", 'a', "", "", "", "", ""))
            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserInfoToLogin(string email)
        {
            var user = await _context
            .Users
                .Where(u => u.Email == email)
                .Select(x => new User(x.Id, x.Name, x.Email, DateTime.Now, x.Cpf, 'p', "", "", x.PasswordHash, x.Salt, x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByEmailInfoUpdate(string email)
        {
            var user = await _context
            .Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserByName(string name)
        {
            var user = await _context
                .Users
                .Where(u => u.Name == name)
                .Select(x => new User(x.Id, x.Name, "a", DateTime.Now, "", 'p', "", "", "", "", x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetIfUserExistEmail(string email)
        {
            var user = await _context
                .Users
                .Where(u => u.Email == email)
                .Select(x => new User(x.Id, x.Name, x.Email, DateTime.Now, "", 'p', "", "", "", "", x.UserImage))
                .FirstOrDefaultAsync();

            return user;
        }
    }
}