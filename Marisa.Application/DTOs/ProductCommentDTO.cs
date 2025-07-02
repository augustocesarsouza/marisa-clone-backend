using Marisa.Domain.Entities;

namespace Marisa.Application.DTOs
{
    public class ProductCommentDTO
    {
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public string? ProductIdString { get; set; }
        public ProductDTO? ProductDTO { get; set; }
        public int? StarQuantity { get; set; }
        public bool? RecommendProduct { get; set; }
        public string? Comment { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ImgProduct { get; set; } // aqui pode ser uma lista e tal
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProductCommentDTO(Guid? id, Guid? productId, string? productIdString, ProductDTO? productDTO, int? starQuantity, 
            bool? recommendProduct, string? comment, string? name, string? email, string? imgProduct, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            ProductId = productId;
            ProductIdString = productIdString;
            ProductDTO = productDTO;
            StarQuantity = starQuantity;
            RecommendProduct = recommendProduct;
            Comment = comment;
            Name = name;
            Email = email;
            ImgProduct = imgProduct;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public ProductCommentDTO()
        {
        }

        public void SetId(Guid? id) => Id = id;
        public void SetProductId(Guid? productId) => ProductId = productId;
        public void SetProductIdString(string? productIdString) => ProductIdString = productIdString;
        public void SetProductDTO(ProductDTO? productDTO) => ProductDTO = productDTO;
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
