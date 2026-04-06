using Microsoft.Extensions.DependencyInjection;

namespace TemplateName.Presentation.Grpc.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcApi(this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
}