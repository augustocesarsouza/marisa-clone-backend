using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marisa.Infra.Data.Maps
{
    public class ProductCommentsMap : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.ToTable("tb_product_comments");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_comments");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_comments_id");

            builder.Property(e => e.ProductId)
               .IsRequired(true)
              .HasColumnName("product_id");

            builder.HasOne(x => x.Product);

            builder.Property(e => e.StarQuantity)
               .IsRequired(true)
              .HasColumnName("star_quantity");

            builder.Property(e => e.RecommendProduct)
               .IsRequired(true)
              .HasColumnName("recommend_product");

            builder.Property(e => e.Comment)
              .IsRequired(true)
             .HasColumnName("comment");

            builder.Property(e => e.Name)
              .IsRequired(true)
             .HasColumnName("name");

            builder.Property(e => e.Email)
              .IsRequired(true)
             .HasColumnName("email");

            builder.Property(e => e.ImgProduct)
              .IsRequired(true)
             .HasColumnName("img_product");

            builder.Property(e => e.CreatedAt)
               .IsRequired(true)
              .HasColumnName("created_at");

            builder.Property(e => e.UpdatedAt)
               .IsRequired(true)
              .HasColumnName("updated_at");
        }
    }
}
