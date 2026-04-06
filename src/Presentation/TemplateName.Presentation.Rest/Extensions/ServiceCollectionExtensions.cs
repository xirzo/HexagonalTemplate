using Microsoft.Extensions.DependencyInjection;

namespace TemplateName.Presentation.Rest.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRestApi(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}