using System.Reflection;
using Microsoft.OpenApi.Models;

namespace DD.AccessServer.Api.Configurations;

internal static class SwaggerConfiguration
{
    internal static IServiceCollection AddSwaggerConfiguration(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo {Title = "Access service", Version = "v1"});
        
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
 
            option.IncludeXmlComments(xmlPath);
        });

        return serviceCollection;
    }
}