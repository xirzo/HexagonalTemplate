using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using TemplateName.Application.Extensions;
using TemplateName.Infrastructure.Npgsql.Extensions;
using TemplateName.Presentation.Grpc.Extensions;
using TemplateName.Presentation.Rest.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddNpgsql(builder.Configuration)
    .AddMigrator(builder.Configuration)
    .AddRestApi()
    .AddGrpcApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGrpcReflectionService();
}

app.MigrateUp();
app.MapControllers();
app.MapGrpcServices();
app.Run();