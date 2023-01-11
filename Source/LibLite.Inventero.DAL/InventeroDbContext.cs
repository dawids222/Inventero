using LibLite.Inventero.DAL.Converters;
using LibLite.Inventero.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibLite.Inventero.DAL
{
    public class InventeroDbContext : DbContext
    {
        public InventeroDbContext(DbContextOptions options) : base(options) { }
        public InventeroDbContext() { }

        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            ConfigureDateTimeConventions(builder);
        }

        private void ConfigureDateTimeConventions(ModelConfigurationBuilder builder)
        {
            builder
                .Properties<DateTime>()
                .HaveConversion<DateTimeUtcConverter>();
            builder
                .Properties<DateTime?>()
                .HaveConversion<NullableDateTimeUtcConverter>();
        }
    }
}
