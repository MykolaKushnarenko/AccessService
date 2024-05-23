using DD.AccessService.ApplicationCore.Common.PipelineBehaviours;
using Microsoft.Extensions.DependencyInjection;

namespace DD.AccessService.ApplicationCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AtomicScopeBehaviour<,>).Assembly));

        return serviceCollection;
    }
}