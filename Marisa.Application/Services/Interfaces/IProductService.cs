using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IProductService 
    {
        public Task<ResultService<ProductDTO>> GetProductById(Guid? productId);
        public Task<ResultService<ProductDTO>> GetProductIfExist(string productId);
        public Task<ResultService<List<ProductDTO>>> GetAllProductType(string type);
        public Task<ResultService<ProductDTO>> Create(ProductDTO? productDTO);
        public Task<ResultService<ProductDTO>> Delete(Guid productId);
    }
}
