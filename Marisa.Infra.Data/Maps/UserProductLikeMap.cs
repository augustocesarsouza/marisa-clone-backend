using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marisa.Infra.Data.Maps
{
    public class UserProductLikeMap : IEntityTypeConfiguration<UserProductLike>
    {
        public void Configure(EntityTypeBuilder<UserProductLike> builder)
        {
            builder.ToTable("tb_user_product_likes");

            builder.HasKey(e => e.Id)
                .HasName("pk_user_product_likes");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("user_product_likes_id");

            builder.Property(e => e.ProductId)
                .IsRequired(true)
               .HasColumnName("product_id");

            builder.HasOne(x => x.Product);

            builder.Property(e => e.UserId)
                .IsRequired(true)
               .HasColumnName("user_id");

            builder.HasOne(x => x.User);

            builder.Property(e => e.LikedAt)
                .IsRequired(true)
               .HasColumnName("liked_at");
        }
    }
}
