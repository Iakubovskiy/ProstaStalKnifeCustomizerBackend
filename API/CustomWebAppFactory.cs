using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace API;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {
            var contextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DBContext>));
            if (contextDescriptor != null)
                services.Remove(contextDescriptor);

            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DBContext));
            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            services.AddDbContext<DBContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DBContext>();
            db.Database.EnsureCreated();

            var seeder = scope.ServiceProvider.GetRequiredService<MainSeeder>();
            seeder.SeedAsync().GetAwaiter().GetResult();
        });
    }
}