using Marisa.Application.DTOs;

namespace Marisa.Application.Services.Interfaces
{
    public interface IUserProductLikeService
    {
        public Task<ResultService<UserProductLikeDTO>> GetUserProductLikeById(Guid? userProductLikeId);
        public Task<ResultService<UserProductLikeDTO>> GetUserProductLikeByProductId(Guid? productId, Guid? userId);
        public Task<ResultService<List<UserProductLikeDTO>>> GetAllUserProductLike();
        public Task<ResultService<UserProductLikeCreateOrDeleteDTO>> Create(UserProductLikeDTO? userProductLikeDTO);
    }
}
