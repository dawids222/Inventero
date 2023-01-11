using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibLite.Inventero.DAL.Factories
{
    public class InventeroMigrationDbContextFactory : IDesignTimeDbContextFactory<InventeroDbContext>
    {
        public InventeroDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InventeroDbContext>();
            builder.UseSqlite("Data Source=migration.db.tmp");

            return new InventeroDbContext(builder.Options);
        }
    }
}
