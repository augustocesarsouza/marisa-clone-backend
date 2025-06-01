

using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByIdToDelete(Guid? productId)
        {
            var product = await _context
               .Products
               .Where(u => u.Id == productId)
               .FirstOrDefaultAsync();

            return product;
        }

        public async Task<List<Product>?> GetAllProductType(string type)
        {
            var products = await _context
               .Products
               .Where(u => u.Type == type)
               .Select(x => new Product(x.Id, x.Title, x.Code, x.Price, x.PriceDiscounted,
               x.DiscountPercentage, x.InstallmentPrice, x.InstallmentTimesMarisaCard, x.InstallmentTimesCreditCard,
               x.Colors, x.SizesAvailable, x.ImageUrl, x.QuantityAvailable, null, null, x.Type, x.Category))
               .ToListAsync();

            return products;
        }

        public async Task<Product?> GetProductById(Guid? productId)
        {
            var product = await _context
               .Products
               .Where(u => u.Id == productId)
               .FirstOrDefaultAsync();

            if (product?.CreatedAt is DateTime utcCreatedAt)
            {
                var localTimeZone = TimeZoneInfo.Local;
                var createdAt = TimeZoneInfo.ConvertTimeFromUtc(utcCreatedAt, localTimeZone);
                product.SetCreatedAt(createdAt);
            }

            if (product?.UpdatedAt is DateTime utcUpdatedAt)
            {
                var localTimeZone = TimeZoneInfo.Local;
                var updatedAt = TimeZoneInfo.ConvertTimeFromUtc(utcUpdatedAt, localTimeZone);
                product.SetUpdatedAt(updatedAt);
            }

            return product;
        }
    }
}
