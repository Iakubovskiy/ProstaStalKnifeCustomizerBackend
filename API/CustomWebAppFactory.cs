using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using Xunit;
using Domain.Component.Product;

namespace API;

public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;

    public CustomWebAppFactory()
    {
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("test_db")
            .WithUsername("test_user")
            .WithPassword("test_password")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<DBContext>>();
            services.RemoveAll<DBContext>();

            services.AddDbContext<DBContext>((sp, options) =>
            {
                var loggerFactory = LoggerFactory.Create(logBuilder =>
                {
                    logBuilder.AddConsole();
                });

                options.UseNpgsql(_dbContainer.GetConnectionString());
                
                options.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var optionsBuilder = new DbContextOptionsBuilder<DBContext>()
            .UseNpgsql(_dbContainer.GetConnectionString());

        await using (var dbContextForMigration = new DBContext(optionsBuilder.Options))
        {
            await dbContextForMigration.Database.MigrateAsync();
        }

        using (var scope = this.Services.CreateScope())
        {
            var mainSeeder = scope.ServiceProvider.GetRequiredService<MainSeeder>();
            await mainSeeder.SeedAsync();
        }

        await using (var dbContextForUpdate = new DBContext(optionsBuilder.Options))
        {
            var allProducts = await dbContextForUpdate.Set<Product>().ToListAsync();

            dbContextForUpdate.UpdateRange(allProducts);

            await dbContextForUpdate.SaveChangesAsync();
        }
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
    
    public string GetConnectionStringForDiagnostics()
    {
        return _dbContainer.GetConnectionString();
    }
}