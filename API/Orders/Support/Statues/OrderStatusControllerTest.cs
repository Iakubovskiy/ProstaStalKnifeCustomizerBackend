using FluentAssertions;
using Xunit;

namespace API.Orders.Support.Statues;

public class OrderStatusControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public OrderStatusControllerTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllOrderStatuses_ShouldReturnAllEnumNames()
    {
        var response = await this._client.GetAsync("/api/order-statuses");
        
        response.EnsureSuccessStatusCode();
        var statuses = await response.Content.ReadFromJsonAsync<List<string>>();
        
        statuses.Should().NotBeNull();
        statuses.Should().HaveCount(4);
        statuses.Should().ContainInOrder("New", "Pending", "Completed", "Canceled");
    }
}