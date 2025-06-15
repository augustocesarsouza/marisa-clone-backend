using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IUserProductLikeRepository : IGenericRepository<UserProductLike>
    {
        public Task<UserProductLike?> GetUserProductLikeById(Guid? userProductLikeId);
        public Task<UserProductLike?> GetUserProductLikeByProductId(Guid? productId, Guid? userId);
        public Task<UserProductLike?> GetUserProductLikeByProductIdFullProp(Guid? productId, Guid? userId);
        public Task<List<UserProductLike>?> GetAllUserProductLike();
    }
}
