namespace Marisa.Domain.Entities
{
    public class UserProductLike
    {
        public Guid? Id { get; private set; }
        public Guid? ProductId { get; private set; }
        public Product? Product { get; private set; }
        public Guid? UserId { get; private set; }
        public User? User { get; private set; }
        public DateTime? LikedAt { get; set; } = DateTime.UtcNow;

        public UserProductLike(Guid? id, Guid? productId, Product? product, Guid? userId, User? user, DateTime? likedAt)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            UserId = userId;
            User = user;
            LikedAt = likedAt;
        }

        public UserProductLike()
        {
        }

        public void SetId(Guid? id) => Id = id;
        public void SetProductId(Guid? productId) => ProductId = productId;
        public void SetProduct(Product? product) => Product = product;
        public void SetUserId(Guid? userId) => UserId = userId;
        public void SetUser(User? user) => User = user;
        public void SetLikedAt(DateTime? likedAt) => LikedAt = likedAt;
    }
}
