using Marisa.Domain.Entities;

namespace Marisa.Application.DTOs
{
    public class ProductAdditionalInfoDTO
    {
        public Guid? Id { get; set; }
        public List<string>? ImgsSecondary { get; set; }
        public List<ProductInfoSection>? AboutProduct { get; set; }
        public string? Composition { get; set; }
        public string? ShippingInformation { get; set; }
        public Guid? ProductId { get; set; }
        public ProductDTO? ProductDTO { get; set; }

        public ProductAdditionalInfoDTO() { }

        public ProductAdditionalInfoDTO(Guid? id, List<string>? imgsSecondary, List<ProductInfoSection>? aboutProduct, 
            string? composition, string? shippingInformation, Guid? productId, ProductDTO? productDTO)
        {
            Id = id;
            ImgsSecondary = imgsSecondary;
            AboutProduct = aboutProduct;
            Composition = composition;
            ShippingInformation = shippingInformation;
            ProductId = productId;
            ProductDTO = productDTO;
        }

        public Guid? SetId(Guid? id) => Id = id;
        public Guid? SetProductId(Guid? productId) => ProductId = productId;
    }
}
