using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marisa.Infra.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // fazer migrations e ver se a tipagem está certo
            builder.ToTable("tb_marisa_products");

            builder.HasKey(e => e.Id)
                .HasName("pk_product");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_id");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasMaxLength(255);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasColumnName("code")
                .HasColumnType("bigint");

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnName("price");

            builder.Property(e => e.PriceDiscounted)
                .IsRequired()
                .HasColumnName("price_discounted");

            builder.Property(e => e.DiscountPercentage)
                .IsRequired()
                .HasColumnName("discount_percentage");

            builder.Property(e => e.InstallmentPrice)
                .IsRequired()
                .HasColumnName("installment_price");

            builder.Property(e => e.InstallmentTimesMarisaCard)
                .IsRequired()   
                .HasColumnName("installment_times_marisa_card");

            builder.Property(e => e.InstallmentTimesCreditCard)
               .IsRequired()
               .HasColumnName("installment_times_credit_card");

            builder.Property(e => e.Colors)
                .IsRequired()
                .HasColumnName("colors")
                .HasColumnType("varchar[]");

            builder.Property(e => e.SizesAvailable)
               .IsRequired()
               .HasColumnName("sizes_available")
               .HasColumnType("varchar[]");

            builder.Property(e => e.ImageUrl)
               .IsRequired()
               .HasColumnName("image_url");

            builder.Property(e => e.QuantityAvailable)
              .IsRequired()
              .HasColumnName("quantity_available");

            builder.Property(e => e.CreatedAt)
               .IsRequired()
               .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
              .IsRequired()
              .HasColumnName("updated_at");

            builder.Property(e => e.Type)
            .IsRequired()
            .HasColumnName("type")
            .HasColumnType("varchar(150)");

            builder.Property(e => e.Category)
           .IsRequired()
           .HasColumnName("category")
           .HasColumnType("varchar(150)");
        }
    }
}
