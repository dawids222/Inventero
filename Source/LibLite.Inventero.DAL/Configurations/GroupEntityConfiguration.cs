using LibLite.Inventero.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibLite.Inventero.DAL.Configurations
{
    internal class GroupEntityConfiguration : EntityConfiguration<GroupEntity>
    {
        protected override void ConfigureProperties(EntityTypeBuilder<GroupEntity> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(MAX_STRING_LENGTH)
                .IsRequired();
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<GroupEntity> builder) { }
    }
}
