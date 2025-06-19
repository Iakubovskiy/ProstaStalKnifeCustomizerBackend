using System.Net;
using System.Text;
using System.Text.Json;
using Application.Orders.Support.DeliveryTypes.Data;
using Domain.Orders.Support;
using FluentAssertions;
using Xunit;

namespace API.Orders.Support.DeliveryTypes;

public class DeliveryTypeTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public DeliveryTypeTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetAllDeliveryTypes_ShouldReturnAllTypes()
    {
        var response = await this._client.GetAsync("/api/delivery-types");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<DeliveryType>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }
    
    [Fact]
    public async Task GetAllActiveDeliveryTypes_ShouldReturnOnlyActiveTypes()
    {
        var response = await this._client.GetAsync("/api/delivery-types/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<DeliveryType>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(9);
        data.Should().OnlyContain(d => d.IsActive);
    }

    [Theory]
    [InlineData("b6d15e84-3d69-4b74-b270-cb5e9fd0d2d3", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetDeliveryTypesById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/delivery-types/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var deliveryType = await response.Content.ReadFromJsonAsync<DeliveryType>();
            deliveryType.Should().NotBeNull();
            deliveryType.Name.TranslationDictionary["en"].Should().Be("Nova Poshta");
            deliveryType.Price.Should().Be(50.0);
        }
    }

    [Fact]
    public async Task CreateDeliveryType_ShouldSucceed()
    {
        var newDeliveryTypeId = new Guid("ff0a1338-51e4-4a2e-846d-97e3f283d6a6");
        var newDeliveryTypeDto = new DeliveryTypeDto
        {
            Id = newDeliveryTypeId,
            Names = new Dictionary<string, string>
            {
                { "en", "Test Delivery" },
                { "ua", "Тестова Доставка" }
            },
            Comments = new Dictionary<string, string>
            {
                { "en", "Test Comment" },
                { "ua", "Тестовий Коментар" }
            },
            Price = 99.99,
            IsActive = true
        };
        
        var json = JsonSerializer.Serialize(newDeliveryTypeDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/delivery-types", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/delivery-types/{newDeliveryTypeId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetchedType = await responseFromGet.Content.ReadFromJsonAsync<DeliveryType>();
        
        fetchedType.Should().NotBeNull();
        fetchedType.Name.TranslationDictionary["en"].Should().Be("Test Delivery");
        fetchedType.Price.Should().Be(99.99);
        
        await this._client.DeleteAsync($"/api/delivery-types/{newDeliveryTypeId}");
    }

    [Fact]
    public async Task UpdateDeliveryType_ShouldSucceed()
    {
        var idToUpdate = new Guid("79beccad-8373-4f94-935d-43dd8d97975c");
        var updatedDeliveryTypeDto = new DeliveryTypeDto
        {
            Names = new Dictionary<string, string>
            {
                { "en", "Updated Courier" },
                { "ua", "Оновлений Кур'єр" }
            },
            Comments = new Dictionary<string, string>
            {
                { "en", "Updated Comment" },
                { "ua", "Оновлений Коментар" }
            },
            Price = 120.50,
            IsActive = false
        };
        
        var json = JsonSerializer.Serialize(updatedDeliveryTypeDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/delivery-types/{idToUpdate}", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseFromGet = await this._client.GetAsync($"/api/delivery-types/{idToUpdate}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetchedType = await responseFromGet.Content.ReadFromJsonAsync<DeliveryType>();
        
        fetchedType.Should().NotBeNull();
        fetchedType.Name.TranslationDictionary["en"].Should().Be("Updated Courier");
        fetchedType.Price.Should().Be(120.50);
        fetchedType.IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteDeliveryType_ShouldSucceed()
    {
        var idToDelete = new Guid("d290fb77-e4c2-4d0e-b46d-9a928440b3cd");
        var newDeliveryTypeDto = new DeliveryTypeDto
        {
            Id = idToDelete,
            Names = new Dictionary<string, string> { { "en", "To Be Deleted" } },
            Price = 1,
            IsActive = true
        };
        var json = JsonSerializer.Serialize(newDeliveryTypeDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var postResponse = await this._client.PostAsync("/api/delivery-types", content);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/delivery-types/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await deleteResponse.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
        
        responseContent.Should().ContainKey("isDeleted").WhoseValue.Should().BeTrue();
        
        var response = await this._client.GetAsync($"/api/delivery-types/{idToDelete}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = new Guid("e8b91597-2f10-4585-bbe1-d62c6ae0147e");

        var deactivateResponse = await this._client.PatchAsync($"/api/delivery-types/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/delivery-types/{id}");
        var typeAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<DeliveryType>();
        typeAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/delivery-types/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/delivery-types/{id}");
        var typeAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<DeliveryType>();
        typeAfterActivate.IsActive.Should().BeTrue();
    }
}