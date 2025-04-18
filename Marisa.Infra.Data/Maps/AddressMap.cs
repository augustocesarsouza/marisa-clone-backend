using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marisa.Infra.Data.Maps
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("tb_address");

            builder.HasKey(e => e.Id)
                .HasName("pk_address");

            builder.Property(e => e.Id)
                .IsRequired()
                .HasColumnType("uuid")
                .HasColumnName("address_id");

            builder.Property(e => e.AddressNickname)
                .IsRequired()
                .HasColumnName("address_nickname")
                .HasMaxLength(255);

            builder.Property(e => e.AddressType)
                .IsRequired()
                .HasColumnName("address_type")
                .HasMaxLength(100);

            builder.Property(e => e.RecipientName)
                .IsRequired()
                .HasColumnName("recipient_name")
                .HasMaxLength(100);

            builder.Property(e => e.ZipCode)
                .IsRequired()
                .HasColumnName("zip_code")
                .HasMaxLength(10);

            builder.Property(e => e.Street)
                .IsRequired()
                .HasColumnName("street")
                .HasMaxLength(200);

            builder.Property(e => e.Number)
                .IsRequired()
                .HasColumnName("number");

            builder.Property(e => e.Complement)
                .IsRequired()
                .HasColumnName("complement")
                .HasMaxLength(200);

            builder.Property(e => e.Neighborhood)
                .IsRequired()
                .HasColumnName("neighborhood")
                .HasMaxLength(200);

            builder.Property(e => e.City)
                .IsRequired()
                .HasColumnName("city")
                .HasMaxLength(100);

            builder.Property(e => e.State)
                .IsRequired()
                .HasColumnName("state")
                .HasMaxLength(50);

            builder.Property(e => e.ReferencePoint)
                .IsRequired()
                .HasColumnName("reference_point")
                .HasMaxLength(200);

            //builder.HasIndex(e => e.UserId)
            //   .HasDatabaseName("ix_user_cupons_user_id");

            builder.Property(e => e.UserId)
                .IsRequired()
               .HasColumnName("user_id");

            builder.HasOne(e => e.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_address_user"); ;
        }
    }
}