namespace Marisa.Domain.Entities
{
    public class Product
    {
        public Guid? Id { get; private set; }
        public string? Title { get; private set; }
        public long? Code { get; private set; }
        public double? Price { get; private set; }
        public double? PriceDiscounted { get; private set; }
        public int? DiscountPercentage { get; private set; }
        public double? InstallmentPrice { get; private set; } //Valor da parcela
        public int? InstallmentTimesMarisaCard { get; private set; } //Quantidade de parcelas cartão marisa
        public int? InstallmentTimesCreditCard { get; private set; } //Quantidade de parcelas cartão de credito
        public List<string>? Colors { get; private set; }
        public List<string>? SizesAvailable { get; private set; }
        public string? ImageUrl { get; private set; }
        public int? QuantityAvailable { get; private set; }
        //public Guid? UserId { get; private set; } aqui da para colocar SellerUser
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public string? Type { get; private set; }
        public string? Category { get; private set; }

        // fazer outra tabela com as imagens principais e a parte de "Sobre o produto" "Composição" "Informações sobre o frete"

        public Product(Guid? id, string? title, long? code, double? price, double? priceDiscounted, int? discountPercentage, double? installmentPrice, 
            int? installmentTimesMarisaCard, int? installmentTimesCreditCard, List<string>? colors, List<string>? sizesAvailable, string? imageUrl, 
            int? quantityAvailable, DateTime? createdAt, DateTime? updatedAt, string? type, string? category)
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
            Category = category;
        }

        public Product()
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
        public void SetCategory(string category)
        {
            Category = category;
        }
    }
}
