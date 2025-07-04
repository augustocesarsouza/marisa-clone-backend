using Marisa.Domain.Enums;

namespace Marisa.Application.DTOs
{
    public class ProductCommentLikeCreate
    {
        public string? UserId { get; set; }
        public string? ProductCommentId { get; set; }
        public string? ProductId { get; set; }
        public ReactionType Reaction { get; set; }

        public ProductCommentLikeCreate(string? userId, string? productCommentId, string? productId, ReactionType reaction)
        {
            UserId = userId;
            ProductCommentId = productCommentId;
            ProductId = productId;
            Reaction = reaction;
        }

        public ProductCommentLikeCreate()
        {
        }
    }
}
