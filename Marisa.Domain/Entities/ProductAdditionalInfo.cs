using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Marisa.Domain.Entities
{
    public class ProductAdditionalInfo
    {
        public Guid? Id { get; private set; }
        public List<string>? ImgsSecondary { get; private set; }
        public List<ProductInfoSection>? AboutProduct { get; private set; }
        public string? Composition { get; private set; }
        public string? ShippingInformation { get; private set; }
        public Guid? ProductId { get; private set; }
        public Product? Product { get; private set; }

        public ProductAdditionalInfo()
        {
        }

        public ProductAdditionalInfo(Guid? id, List<string>? imgsSecondary, List<ProductInfoSection>?
            aboutProduct, string? composition, string? shippingInformation, Guid? productId)
        {
            Id = id;
            ImgsSecondary = imgsSecondary;
            AboutProduct = aboutProduct;
            Composition = composition;
            ShippingInformation = shippingInformation;
            ProductId = productId;
        }

        public Guid? SetId(Guid? id) => Id = id;
        public void SetImgsSecondary(List<string> imgs) => ImgsSecondary = imgs;
        public void SetAboutProduct(List<ProductInfoSection> about) => AboutProduct = about;
        public void SetComposition(string composition) => Composition = composition;
        public void SetShippingInformation(string shipping) => ShippingInformation = shipping;
        public void SetProductId(Guid productId) => ProductId = productId;
    }

    public class ProductInfoSection
    {
        public string Title { get; set; } = default!;
        public List<string> Items { get; set; } = new();

        public ProductInfoSection() { }

        public ProductInfoSection(string title, List<string> items)
        {
            Title = title;
            Items = items;
        }

        public void SetTitle(string title) => Title = title;
        public void SetItems(List<string> items) => Items = items;
    }
}
