using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class ProductAdditionalInfoRepository : GenericRepository<ProductAdditionalInfo>, IProductAdditionalInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductAdditionalInfoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductAdditionalInfo?> GetProductAdditionalInfoById(Guid? id)
        {
            var productAdditionalInfos = await _context
                .ProductAdditionalInfos
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            return productAdditionalInfos;
        }

        public async Task<ProductAdditionalInfo?> GetProductAdditionalInfoByProductId(Guid? productId)
        {
            var productAdditionalInfos = await _context
                .ProductAdditionalInfos
                .Where(u => u.ProductId == productId)
                .FirstOrDefaultAsync();

            return productAdditionalInfos;
        }
    }
}
