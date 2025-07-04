using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IProductCommentLikeRespository : IGenericRepository<ProductCommentLike>
    {
        public Task<ProductCommentLike?> GetProductCommentLikeById(Guid? productCommentLikeId);
        public Task<List<ProductCommentLike>?> GetAllProductCommentLikeByProductId(Guid? productId);
        public Task<ProductCommentLike?> GetByProductCommentIdAndUser(Guid? productCommentId, Guid? userId);
    }
}
