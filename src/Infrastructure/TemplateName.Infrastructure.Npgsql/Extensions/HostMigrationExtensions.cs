using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace TemplateName.Infrastructure.Npgsql.Extensions;

public static class HostMigrationExtensions
{
    public static IHost MigrateUp(this IHost host)
    {
        using IServiceScope scope = host.Services.CreateScope();
        ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger>();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

        try
        {
            logger.LogInformation("Migrating database schema");
            runner.MigrateUp();
        }
        catch (Exception ex)
        {
            logger.LogError($"Migration failed: {ex.Message}");
            throw;
        }

        return host;
    }
}