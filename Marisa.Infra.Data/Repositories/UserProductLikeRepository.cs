using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class UserProductLikeRepository : GenericRepository<UserProductLike>, IUserProductLikeRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProductLikeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserProductLike?> GetUserProductLikeById(Guid? userProductLikeId)
        {
            var userProductLike = await _context
               .UserProductLikes
               .Where(u => u.Id == userProductLikeId)
               .FirstOrDefaultAsync();

            if (userProductLike?.LikedAt is DateTime utcCreatedAt)
            {
                var localTimeZone = TimeZoneInfo.Local;
                var likedAt = TimeZoneInfo.ConvertTimeFromUtc(utcCreatedAt, localTimeZone);
                userProductLike.SetLikedAt(likedAt);
            }

            return userProductLike;
        }

        public async Task<UserProductLike?> GetUserProductLikeByProductId(Guid? productId, Guid? userId)
        {
            var userProductLike = await _context
               .UserProductLikes
               .Where(u => u.ProductId == productId && u.UserId == userId)
               .Select(x => new UserProductLike(null, x.ProductId, null, null, null, null))
               .FirstOrDefaultAsync();

            return userProductLike;
        }

        public async Task<UserProductLike?> GetUserProductLikeByProductIdFullProp(Guid? productId, Guid? userId)
        {
            var userProductLike = await _context
               .UserProductLikes
               .Where(u => u.ProductId == productId && u.UserId == userId)
               .FirstOrDefaultAsync();

            return userProductLike;
        }

        public async Task<List<UserProductLike>?> GetAllUserProductLike()
        {
            var userProductLikes = await _context
               .UserProductLikes
               .ToListAsync();

            return userProductLikes;
        }
    }
}
