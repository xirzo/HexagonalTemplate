using Microsoft.AspNetCore.Builder;

namespace TemplateName.Presentation.Rest.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapGrpcServices(this WebApplication app)
    {
        return app;
    }
}