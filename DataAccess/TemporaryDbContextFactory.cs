using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_product.DataAccess
{
    public class TemporaryDbContextFactory : DbContext
    {
        public AppDbContext Create(DbContextOptions options)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EccomerceProduct;Trusted_Connection=True;MultipleActiveResultSets=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name));
            return new AppDbContext(builder.Options);
        }
    }
}