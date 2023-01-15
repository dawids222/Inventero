using Microsoft.EntityFrameworkCore;

namespace LibLite.Inventero.DAL.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<int> SaveChangesAndClearAsync(this DbContext context)
        {
            try { return await context.SaveChangesAsync(); }
            finally { context.ChangeTracker.Clear(); }
        }
    }
}
