using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserById(Guid? id);
        public Task<User?> GetUserByIdToChangePassword(Guid? id);
        public Task<User?> GetUserByEmail(string email);
        public Task<User?> GetUserByIdInfoUser(Guid id);
        public Task<User?> GetInfoToUpdateProfile(Guid id);
        public Task<User?> GetUserByIdCheckIfExist(Guid id);
        public Task<User?> GetUserInfoToLogin(string email);
        public Task<User?> GetUserByEmailInfoUpdate(string email);
        public Task<User?> GetUserByName(string name);
        public Task<User?> GetIfUserExistEmail(string email);
    }
}
