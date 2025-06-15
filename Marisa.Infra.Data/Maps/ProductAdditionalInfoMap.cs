using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Marisa.Infra.Data.Maps
{
    public class ProductAdditionalInfoMap : IEntityTypeConfiguration<ProductAdditionalInfo>
    {
        public void Configure(EntityTypeBuilder<ProductAdditionalInfo> builder)
        {
            builder.ToTable("tb_product_additional_info");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_additional_info");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_additional_info_id");

            builder.Property(e => e.ImgsSecondary)
               .IsRequired(true)
              .HasColumnName("imgs_secondary");

            builder.Property(e => e.AboutProduct)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<ProductInfoSection>>(v, (JsonSerializerOptions?)null)
                )
                .IsRequired(true)
                .HasColumnName("about_product");

            builder.Property(e => e.Composition)
               .IsRequired(true)
              .HasColumnName("composition");

            builder.Property(e => e.ShippingInformation)
               .IsRequired(true)
              .HasColumnName("shipping_information");

            builder.Property(e => e.ProductId)
                .IsRequired(true)
               .HasColumnName("product_id");

            builder.HasIndex(e => e.ProductId)
               .HasDatabaseName("ix_product_additional_info_product_id");

            builder.HasOne(x => x.Product);
        }
    }
}
