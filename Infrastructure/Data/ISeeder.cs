namespace Infrastructure.Data;

public interface ISeeder
{
    int Priority { get; }
    public Task SeedAsync();
}