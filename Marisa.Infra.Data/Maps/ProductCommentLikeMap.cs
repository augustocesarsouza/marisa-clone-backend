using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marisa.Infra.Data.Maps
{
    public class ProductCommentLikeMap : IEntityTypeConfiguration<ProductCommentLike>
    {
        public void Configure(EntityTypeBuilder<ProductCommentLike> builder)
        {
            builder.ToTable("tb_product_comment_likes");

            builder.HasKey(e => e.Id)
                .HasName("pk_product_comment_likes");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("product_comment_likes_id");

            builder.Property(e => e.ProductId)
               .IsRequired(true)
              .HasColumnName("product_id");

            builder.HasOne(x => x.Product);

            builder.Property(e => e.UserId)
               .IsRequired(true)
              .HasColumnName("user_id");

            builder.HasOne(x => x.User);

            builder.Property(e => e.ProductCommentId)
               .IsRequired(true)
              .HasColumnName("product_comment_id");

            builder.HasOne(x => x.ProductComment);

            builder.Property(e => e.Reaction)
               .IsRequired(true)
              .HasColumnName("reaction")
              .HasConversion<int>(); ;

            builder.Property(e => e.CreatedAt)
              .IsRequired(true)
             .HasColumnName("created_at");
        }
    }
}
