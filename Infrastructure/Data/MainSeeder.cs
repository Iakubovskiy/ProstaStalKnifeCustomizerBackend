namespace Infrastructure.Data;

public class MainSeeder
{
    private readonly IEnumerable<ISeeder> _seeders;

    public MainSeeder(IEnumerable<ISeeder> seeders)
    {
        _seeders = seeders.OrderBy(seeder => seeder.Priority);
    }

    public async Task SeedAsync()
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync();
        }
    }
}
