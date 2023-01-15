using LibLite.Inventero.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibLite.Inventero.DAL.Configurations
{
    internal class ProductEntityConfiguration : EntityConfiguration<ProductEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<ProductEntity> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(MAX_STRING_LENGTH)
                .IsRequired();
            builder
                .Property(x => x.Price)
                .IsRequired();
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<ProductEntity> builder)
        {
            builder
                .HasOne(x => x.Group)
                .WithMany();
            builder
                .Navigation(x => x.Group)
                .AutoInclude();
        }
    }
}
