using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DD.AccessService.Infrastructure.Persistence
{
    internal class MigrationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public virtual ApplicationDbContext CreateDbContext(params string[] args)
        {
            var serviceCollection = new ServiceCollection()
                    .AddDbContextPool<ApplicationDbContext>(x => x.UseSqlServer(GetConnectionString(args)));

            return serviceCollection
                .BuildServiceProvider()
                .GetRequiredService<ApplicationDbContext>();
        }
        
        private string GetConnectionString(params string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.local.json", true)
                .Build();

            return configuration.GetSection("Database:ConnectionStringSecret").Value!;
        }
    }
}
