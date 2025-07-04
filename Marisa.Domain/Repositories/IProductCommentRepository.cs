using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IProductCommentRepository : IGenericRepository<ProductComment>
    {
        public Task<ProductComment?> GetProductCommentByIdToDelete(Guid? productCommentid);
        public Task<ProductComment?> GetProductCommentByIdIfExist(Guid? productCommentid);
        public Task<List<ProductComment>?> GetAllProductCommentByUserIdAndProductId(Guid? productId);
        public Task<ProductComment?> GetProductCommentById(Guid? productCommentid);
    }
}
