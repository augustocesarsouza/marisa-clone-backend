using Marisa.Domain.Entities;

namespace Marisa.Application.DTOs
{
    public class UserProductLikeDTO
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public string? ProductIdString { get; set; }
        public ProductDTO? ProductDTO { get; set; }
        public Guid? UserId { get; set; }
        public string? UserIdString { get; set; }
        public UserDTO? UserDTO { get; set; }
        public DateTime? LikedAt { get; set; }

        public UserProductLikeDTO(Guid? id, Guid? productId, ProductDTO? productDTO, Guid? userId, UserDTO? userDTO, DateTime? likedAt)
        {
            Id = id;
            ProductId = productId;
            ProductDTO = productDTO;
            UserId = userId;
            UserDTO = userDTO;
            LikedAt = likedAt;
        }

        public UserProductLikeDTO()
        {
        }

        public void SetId(Guid? id) => Id = id;
        public void SetProductId(Guid? productId) => ProductId = productId;
        public void SetProduct(ProductDTO? productDTO) => ProductDTO = productDTO;
        public void SetUserId(Guid? userId) => UserId = userId;
        public void SetUser(UserDTO? userDTO) => UserDTO = userDTO;
        public void SetLikedAt(DateTime? likedAt) => LikedAt = likedAt;
    }
}
