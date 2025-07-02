namespace Marisa.Domain.Entities
{
    public class ProductComment
    {
        public Guid? Id { get; private set; }
        public Guid? ProductId { get; private set; }
        public Product? Product { get; private set; }
        public int? StarQuantity { get; private set; }
        public bool? RecommendProduct { get; private set; }
        public string? Comment { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? ImgProduct { get; private set; } // aqui pode ser uma lista e tal
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public ProductComment(Guid? id, Guid? productId, Product? product, int? starQuantity,
            bool? recommendProduct, string? comment, string? name, string? email, string? imgProduct,
            DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            StarQuantity = starQuantity;
            RecommendProduct = recommendProduct;
            Comment = comment;
            Name = name;
            Email = email;
            ImgProduct = imgProduct;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public ProductComment()
        {
        }

        public void SetId(Guid? id) => Id = id;
        public void SetProductId(Guid? productId) => ProductId = productId;
        public void SetProduct(Product? product) => Product = product;
        public void SetStarQuantity(int? starQuantity) => StarQuantity = starQuantity;
        public void SetRecommendProduct(bool? recommendProduct) => RecommendProduct = recommendProduct;
        public void SetComment(string? comment) => Comment = comment;
        public void SetName(string? name) => Name = name;
        public void SetEmail(string? email) => Email = email;
        public void SetImgProduct(string? imgProduct) => ImgProduct = imgProduct;
        public void SetCreatedAt(DateTime? createdAt) => CreatedAt = createdAt;
        public void SetUpdatedAt(DateTime? updatedAt) => UpdatedAt = updatedAt;
    }
}
