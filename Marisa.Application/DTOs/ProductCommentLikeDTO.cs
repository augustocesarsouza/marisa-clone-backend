using Marisa.Domain.Enums;

namespace Marisa.Application.DTOs
{
    public class ProductCommentLikeDTO
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public ProductDTO? ProductDTO { get; set; }
        public Guid? UserId { get; set; }
        public UserDTO? UserDTO { get; set; }
        public Guid? ProductCommentId { get; set; }
        public ProductCommentDTO? ProductCommentDTO { get; set; }
        public ReactionType Reaction { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ProductCommentLikeDTO(Guid? id, Guid? productId, ProductDTO? productDTO, Guid? userId, UserDTO? userDTO, 
            Guid? productCommentId, ProductCommentDTO? productCommentDTO, ReactionType reaction, DateTime? createdAt)
        {
            Id = id;
            ProductId = productId;
            ProductDTO = productDTO;
            UserId = userId;
            UserDTO = userDTO;
            ProductCommentId = productCommentId;
            ProductCommentDTO = productCommentDTO;
            Reaction = reaction;
            CreatedAt = createdAt;
        }

        public ProductCommentLikeDTO()
        {
        }


        public void SetId(Guid? id) => Id = id;
        public void SetProductId(Guid? productId) => ProductId = productId;
        public void SetProductDTO(ProductDTO? productDTO) => ProductDTO = productDTO;
        public void SetUserId(Guid? userId) => UserId = userId;
        public void SetUserDTO(UserDTO? userDTO) => UserDTO = userDTO;
        public void SetProductCommentId(Guid? productCommentId) => ProductCommentId = productCommentId;
        public void SetProductCommentDTO(ProductCommentDTO? productCommentDTO) => ProductCommentDTO = productCommentDTO;
        public void SetReaction(ReactionType reaction) => Reaction = reaction;
        public void SetCreatedAt(DateTime? createdAt) => CreatedAt = createdAt;
    }
}
