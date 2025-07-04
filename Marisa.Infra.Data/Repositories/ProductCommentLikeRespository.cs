using Marisa.Domain.Entities;
using Marisa.Domain.Repositories;
using Marisa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Marisa.Infra.Data.Repositories
{
    public class ProductCommentLikeRespository : GenericRepository<ProductCommentLike>, IProductCommentLikeRespository
    {
        private readonly ApplicationDbContext _context;

        public ProductCommentLikeRespository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductCommentLike?> GetProductCommentLikeById(Guid? productCommentLikeId)
        {
            var productCommentLike = await _context
               .ProductCommentLikes
               .Where(u => u.Id == productCommentLikeId)
               .FirstOrDefaultAsync();

            return productCommentLike;
        }

        public async Task<List<ProductCommentLike>?> GetAllProductCommentLikeByProductId(Guid? productId)
        {
            var productCommentLikes = await _context
               .ProductCommentLikes
               .Where(u => u.ProductId == productId)
               .Select(x => new ProductCommentLike(x.Id, x.ProductId, null, x.UserId, null, x.ProductCommentId,
               null, x.Reaction, null))
               .ToListAsync();

            return productCommentLikes;
        }

        public async Task<ProductCommentLike?> GetByProductCommentIdAndUser(Guid? productCommentId, Guid? userId)
        {
            var productCommentLike = await _context
               .ProductCommentLikes
               .Where(u => u.ProductCommentId == productCommentId && u.UserId == userId)
               .AsTracking()
               .FirstOrDefaultAsync();

            return productCommentLike;
        }
    }
}
