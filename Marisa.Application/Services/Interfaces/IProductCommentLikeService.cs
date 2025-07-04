using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IProductCommentLikeService
    {
        public Task<ResultService<ProductCommentLikeDTO>> GetProductCommentLikeById(Guid? productCommentLikeId);
        public Task<ResultService<List<ProductCommentLikeDTO>>> GetAllProductCommentLikeByProductId(Guid? productId);
        public Task<ResultService<ProductCommentLikeCreateOrDelete>> Create(ProductCommentLikeCreate? productCommentLikeCreate);
    }
}
