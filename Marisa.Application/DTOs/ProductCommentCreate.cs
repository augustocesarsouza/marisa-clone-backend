namespace Marisa.Application.DTOs
{
    public class ProductCommentCreate
    {
        public string? ProductId { get; set; }
        public int? StarQuantity { get; set; }
        public bool? RecommendProduct { get; set; }
        public string? Comment { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ImgProduct { get; set; }

        public ProductCommentCreate(string? productId, int? starQuantity, bool? recommendProduct, string? comment, string? name, string? email, string? imgProduct)
        {
            ProductId = productId;
            StarQuantity = starQuantity;
            RecommendProduct = recommendProduct;
            Comment = comment;
            Name = name;
            Email = email;
            ImgProduct = imgProduct;
        }

        public ProductCommentCreate()
        {
        }

        public void SetProductIdString(string? productId) => ProductId = productId;
        public void SetStarQuantity(int? starQuantity) => StarQuantity = starQuantity;
        public void SetRecommendProduct(bool? recommendProduct) => RecommendProduct = recommendProduct;
        public void SetComment(string? comment) => Comment = comment;
        public void SetName(string? name) => Name = name;
        public void SetEmail(string? email) => Email = email;
        public void SetImgProduct(string? imgProduct) => ImgProduct = imgProduct;
    }
}
