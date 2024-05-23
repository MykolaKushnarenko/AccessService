using DD.AccessService.ApplicationCore.Interfaces;
using DD.AccessService.Infrastructure.Authentication;
using DD.AccessService.Infrastructure.Authentication.Configurations;
using DD.AccessService.Infrastructure.Persistence;
using DD.AccessService.Infrastructure.Persistence.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;

namespace DD.AccessService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration builderConfiguration)
    {
        serviceCollection.Configure<JwtOptions>(builderConfiguration.GetSection("Jwt"));

        serviceCollection.AddDbContextPool<ApplicationDbContext>(x =>
        {
            x.UseSqlServer(builderConfiguration.GetSection("Database:ConnectionStringSecret").Value!,
                o => o.EnableRetryOnFailure());
        });

        serviceCollection.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        serviceCollection.AddSingleton<ISystemClock, SystemClock>();

        serviceCollection.AddScoped<IUserStore, UserStore>();
        serviceCollection.AddScoped<IApiKeyStore, ApiKeyStore>();

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();


        return serviceCollection;
    }
}