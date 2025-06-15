using Marisa.Domain.Entities;

namespace Marisa.Domain.Repositories
{
    public interface IProductAdditionalInfoRepository : IGenericRepository<ProductAdditionalInfo>
    {
        public Task<ProductAdditionalInfo?> GetProductAdditionalInfoById(Guid? id);
        public Task<ProductAdditionalInfo?> GetProductAdditionalInfoByProductId(Guid? productId);
    }
}
