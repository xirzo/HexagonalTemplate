using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateName.Infrastructure.Npgsql.Tests;

public class IntegrationTest1 : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:18.3-alpine").Build();
    private ServiceProvider? _serviceProvider;

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        _serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(_container.GetConnectionString())
                .ScanIn(typeof("REPLACE WITH CLASS FROM INFRASTRUCTURE PROJECT WITH MIGRATIONS").Assembly)
                .For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using IServiceScope scope = _serviceProvider.CreateScope();
        IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    public async Task DisposeAsync()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }

        await _container.DisposeAsync();
    }
}