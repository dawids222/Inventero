using LibLite.Inventero.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibLite.Inventero.DAL.Configurations
{
    internal abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        protected readonly int MAX_STRING_LENGTH = 255;

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            ConfigureProperties(builder);
            ConfigureRelationships(builder);
        }

        protected abstract void ConfigureProperties(EntityTypeBuilder<T> builder);
        protected abstract void ConfigureRelationships(EntityTypeBuilder<T> builder);
    }
}
