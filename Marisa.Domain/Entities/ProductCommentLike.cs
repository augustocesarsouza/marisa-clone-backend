using Marisa.Domain.Enums;

namespace Marisa.Domain.Entities
{
    public class ProductCommentLike
    {
        public Guid? Id { get; private set; }
        public Guid? ProductId { get; private set; }
        public Product? Product { get; private set; }
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }
        public Guid? ProductCommentId { get; private set; }
        public ProductComment? ProductComment { get; private set; }
        public ReactionType Reaction { get; private set; }
        public DateTime? CreatedAt { get; private set; }

        public ProductCommentLike(Guid? id, Guid? productId, Product? product, Guid? userId, User? user, 
            Guid? productCommentId, ProductComment? productComment, ReactionType reaction, DateTime? createdAt)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            UserId = userId;
            User = user;
            ProductCommentId = productCommentId;
            ProductComment = productComment;
            Reaction = reaction;
            CreatedAt = createdAt;
        }

        public ProductCommentLike()
        {
        }

        public void SetId(Guid? id) => Id = id;
        public void SetProductId(Guid? productId) => ProductId = productId;
        public void SetProduct(Product? product) => Product = product;
        public void SetUserId(Guid? userId) => UserId = userId;
        public void SetUser(User? user) => User = user;
        public void SetProductCommentId(Guid? productCommentId) => ProductCommentId = productCommentId;
        public void SetProductComment(ProductComment? productComment) => ProductComment = productComment;
        public void SetReaction(ReactionType reaction) => Reaction = reaction;
        public void SetCreatedAt(DateTime? createdAt) => CreatedAt = createdAt;
    }
}
