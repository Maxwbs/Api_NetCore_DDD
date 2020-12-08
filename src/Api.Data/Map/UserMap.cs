using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Api.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(u => u.id).HasName("ID");
            
            builder.HasIndex(u => u.email)
                    .IsUnique();

            builder.Property(u => u.nome)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(u => u.email)
                    .HasMaxLength(100);
        }
    }
}
