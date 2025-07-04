using Marisa.Domain.Enums;

namespace Marisa.Application.DTOs
{
    public class ProductCommentLikeCreateOrDelete
    {
        public ReactionType Reaction { get; set; }

        public ProductCommentLikeCreateOrDelete(ReactionType reaction)
        {
            Reaction = reaction;
        }

        public ProductCommentLikeCreateOrDelete()
        {
        }

        public void SetReactionType(ReactionType reaction) => Reaction = reaction;
    }
}
