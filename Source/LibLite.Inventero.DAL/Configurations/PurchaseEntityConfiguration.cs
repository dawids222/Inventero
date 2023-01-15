using LibLite.Inventero.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibLite.Inventero.DAL.Configurations
{
    internal class PurchaseEntityConfiguration : EntityConfiguration<PurchaseEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder
                .Property(x => x.Amount)
                .IsRequired();
            builder
                .Property(x => x.UnitPrice)
                .IsRequired();
            builder
                .Property(x => x.Date)
                .IsRequired();
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder
                .HasOne(x => x.Product)
                .WithMany();
            builder
                .Navigation(x => x.Product)
                .AutoInclude();
        }
    }
}
