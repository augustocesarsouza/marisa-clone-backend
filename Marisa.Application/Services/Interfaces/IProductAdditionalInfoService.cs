using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IProductAdditionalInfoService
    {
        public Task<ResultService<ProductAdditionalInfoDTO>> GetProductAdditionalInfoById(Guid? productAdditionalInfoId);
        public Task<ResultService<ProductAdditionalInfoDTO>> GetProductAdditionalInfoByProductId(Guid? productId);
        public Task<ResultService<ProductAdditionalInfoDTO>> Create(ProductAdditionalInfoDTO productAdditionalInfoDTO);
        public Task<ResultService<ProductAdditionalInfoDTO>> DeleteProductAdditionalInfo(Guid? productId);
    }
}
