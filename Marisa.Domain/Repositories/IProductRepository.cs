using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<Product?> GetProductByIdToDelete(Guid? productId);
        public Task<Product?> GetProductById(Guid? productId);
    }
}
