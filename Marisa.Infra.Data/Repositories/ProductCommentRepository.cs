using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class ProductCommentRepository : GenericRepository<ProductComment>, IProductCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductComment?> GetProductCommentByIdToDelete(Guid? productCommentid)
        {
            var productComment = await _context
               .ProductComments
               .Where(u => u.Id == productCommentid)
               .FirstOrDefaultAsync();

            return productComment;
        }

        public async Task<ProductComment?> GetProductCommentByIdIfExist(Guid? productCommentid)
        {
            var productComment = await _context
               .ProductComments
               .Where(u => u.Id == productCommentid)
               .Select(x => new ProductComment(x.Id, null, null, null, null, null, null, null, null, null, null))
               .FirstOrDefaultAsync();

            return productComment;
        }

        public async Task<List<ProductComment>?> GetAllProductCommentByUserIdAndProductId(Guid? productId)
        {
            var productComments = await _context
               .ProductComments
               .Where(u => u.ProductId == productId)
               //.Select(x => new ProductComment(null, x.UserId, null, x.ProductId, null, x.Comment, null, null))
               .ToListAsync();

            return productComments;
        }

        public async Task<ProductComment?> GetProductCommentById(Guid? productCommentid)
        {
            var productComment = await _context
                 .ProductComments
                 .Where(u => u.Id == productCommentid)
                 .FirstOrDefaultAsync();

            if (productComment?.CreatedAt is DateTime utcCreatedAt)
            {
                var localTimeZone = TimeZoneInfo.Local;
                var createdAt = TimeZoneInfo.ConvertTimeFromUtc(utcCreatedAt, localTimeZone);
                productComment.SetCreatedAt(createdAt);
            }

            if (productComment?.UpdatedAt is DateTime utcUpdatedAt)
            {
                var localTimeZone = TimeZoneInfo.Local;
                var updatedAt = TimeZoneInfo.ConvertTimeFromUtc(utcUpdatedAt, localTimeZone);
                productComment.SetUpdatedAt(updatedAt);
            }

            return productComment;
        }
    }
}
