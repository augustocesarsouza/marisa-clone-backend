using Marisa.Domain.Enums;

namespace Marisa.Application.DTOs
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public long? Code { get; set; }
        public double? Price { get; set; }
        public double? PriceDiscounted { get; set; }
        public int? DiscountPercentage { get; set; }
        public double? InstallmentPrice { get; set; } //Valor da parcela
        public int? InstallmentTimesMarisaCard { get; set; } //Quantidade de parcelas cartão marisa
        public int? InstallmentTimesCreditCard { get; set; } //Quantidade de parcelas cartão de credito
        public List<string>? Colors { get; set; }
        public List<string>? SizesAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrlBase64 { get; set; }
        public int? QuantityAvailable { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? Type { get; set; }

        public ProductDTO(Guid? id, string? title, long? code, double? price, double? priceDiscounted, int? discountPercentage, 
            double? installmentPrice, int? installmentTimesMarisaCard, int? installmentTimesCreditCard, List<string>? colors, 
            List<string>? sizesAvailable, string? imageUrl, int? quantityAvailable, DateTime? createdAt, DateTime? updatedAt, string? type)
        {
            Id = id;
            Title = title;
            Code = code;
            Price = price;
            PriceDiscounted = priceDiscounted;
            DiscountPercentage = discountPercentage;
            InstallmentPrice = installmentPrice;
            InstallmentTimesMarisaCard = installmentTimesMarisaCard;
            InstallmentTimesCreditCard = installmentTimesCreditCard;
            Colors = colors;
            SizesAvailable = sizesAvailable;
            ImageUrl = imageUrl;
            QuantityAvailable = quantityAvailable;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Type = type;
        }

        public ProductDTO()
        {
        }

        public Guid? SetId(Guid? id) => Id = id;
        public string? SetTitle(string? title) => Title = title;
        public long? SetCode(long? code) => Code = code;
        public double? SetPrice(double? price) => Price = price;
        public double? SetPriceDiscounted(double? priceDiscounted) => PriceDiscounted = priceDiscounted;
        public int? SetDiscountPercentage(int? discountPercentage) => DiscountPercentage = discountPercentage;
        public double? SetInstallmentPrice(double? installmentPrice) => InstallmentPrice = installmentPrice;
        public int? SetInstallmentTimesMarisaCard(int? times) => InstallmentTimesMarisaCard = times;
        public int? SetInstallmentTimesCreditCard(int? times) => InstallmentTimesCreditCard = times;
        public List<string>? SetColors(List<string>? colors) => Colors = colors;
        public List<string>? SetSizesAvailable(List<string>? sizes) => SizesAvailable = sizes;
        public string? SetImageUrl(string? url) => ImageUrl = url;
        public int? SetQuantityAvailable(int? quantity) => QuantityAvailable = quantity;
        public DateTime? SetCreatedAt(DateTime? createdAt) => CreatedAt = createdAt;
        public DateTime? SetUpdatedAt(DateTime? updatedAt) => UpdatedAt = updatedAt;
        public void SetType(string type)
        {
            Type = type;
        }
    }
}
