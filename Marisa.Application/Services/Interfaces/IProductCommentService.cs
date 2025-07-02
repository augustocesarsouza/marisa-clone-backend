using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IProductCommentService
    {
        public Task<ResultService<ProductCommentDTO>> GetProductCommentById(Guid productCommentId);
        public Task<ResultService<List<ProductCommentDTO>>> GetAllProductCommentByUserIdAndProductId(Guid productId);
        public Task<ResultService<ProductCommentDTO>> Create(ProductCommentCreate? productCommentCreate);
        public Task<ResultService<ProductCommentDTO>> Delete(Guid productCommentId);
    }
}
