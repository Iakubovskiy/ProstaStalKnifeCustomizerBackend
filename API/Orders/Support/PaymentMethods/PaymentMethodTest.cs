using System.Net;
using System.Text;
using System.Text.Json;
using Application.Orders.Support.PaymentMethods.Data;
using Domain.Orders.Support;
using FluentAssertions;
using Xunit;

namespace API.Orders.Support.PaymentMethods;

public class PaymentMethodTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public PaymentMethodTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetAllPaymentMethods_ShouldReturnAllMethods()
    {
        var response = await this._client.GetAsync("/api/payment-methods");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<PaymentMethod>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(5);
    }
    
    [Fact]
    public async Task GetAllActivePaymentMethods_ShouldReturnOnlyActiveMethods()
    {
        var response = await this._client.GetAsync("/api/payment-methods/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<PaymentMethod>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(4);
        data.Should().OnlyContain(p => p.IsActive);
    }

    [Theory]
    [InlineData("a1e7b8c9-d0f1-4a2b-8c3d-4e5f6a7b8c9d", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetPaymentMethodsById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/payment-methods/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var paymentMethod = await response.Content.ReadFromJsonAsync<PaymentMethod>();
            paymentMethod.Should().NotBeNull();
            paymentMethod.Name.TranslationDictionary["en"].Should().Be("Cash on Delivery");
        }
    }

    [Fact]
    public async Task CreatePaymentMethod_ShouldSucceed()
    {
        var newMethodId = new Guid("111aaa22-bbb3-ccc4-ddd5-eeefff000111");
        var newMethodDto = new PaymentMethodDto
        {
            Id = newMethodId,
            Names = new Dictionary<string, string> { { "en", "Test Method" } },
            Descriptions = new Dictionary<string, string> { { "en", "Test Description" } },
            IsActive = true
        };
        
        var json = JsonSerializer.Serialize(newMethodDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/payment-methods", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/payment-methods/{newMethodId}");
        responseFromGet.EnsureSuccessStatusCode();
        
        await this._client.DeleteAsync($"/api/payment-methods/{newMethodId}");
    }

    [Fact]
    public async Task UpdatePaymentMethod_ShouldSucceed()
    {
        var idToUpdate = new Guid("333ccc44-ddd5-eee6-fff0-111222333444");
        var initialDto = new PaymentMethodDto
        {
            Id = idToUpdate,
            Names = new Dictionary<string, string> { { "en", "Initial Method" } },
            Descriptions = new Dictionary<string, string> { { "en", "Initial Desc" } },
            IsActive = true
        };
        var jsonInitial = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var contentInitial = new StringContent(jsonInitial, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/payment-methods", contentInitial);

        var updatedMethodDto = new PaymentMethodDto
        {
            Names = new Dictionary<string, string> { { "en", "Updated Method" } },
            Descriptions = new Dictionary<string, string> { { "en", "Updated Desc" } },
            IsActive = false
        };
        var jsonUpdated = JsonSerializer.Serialize(updatedMethodDto, this._jsonOptions);
        var contentUpdated = new StringContent(jsonUpdated, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/payment-methods/{idToUpdate}", contentUpdated);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseFromGet = await this._client.GetAsync($"/api/payment-methods/{idToUpdate}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetchedMethod = await responseFromGet.Content.ReadFromJsonAsync<PaymentMethod>();
        
        fetchedMethod.Name.TranslationDictionary["en"].Should().Be("Updated Method");
        fetchedMethod.IsActive.Should().BeFalse();
        
        await this._client.DeleteAsync($"/api/payment-methods/{idToUpdate}");
    }

    [Fact]
    public async Task DeletePaymentMethod_ShouldSucceed()
    {
        var idToDelete = new Guid("222bbb33-ccc4-ddd5-eee6-fff000111222");
        var newMethodDto = new PaymentMethodDto
        {
            Id = idToDelete,
            Names = new Dictionary<string, string> { { "en", "To Be Deleted" } },
            Descriptions = new Dictionary<string, string> { { "en", "To Be Deleted Desc" } },
            IsActive = true
        };
        var json = JsonSerializer.Serialize(newMethodDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var postResponse = await this._client.PostAsync("/api/payment-methods", content);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/payment-methods/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var response = await this._client.GetAsync($"/api/payment-methods/{idToDelete}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = new Guid("444ddd55-eee6-fff0-1112-223334445555");
        var initialDto = new PaymentMethodDto
        {
            Id = id,
            Names = new Dictionary<string, string> { { "en", "Activable Method" } },
            Descriptions = new Dictionary<string, string> { { "en", "Desc" } },
            IsActive = true
        };
        var json = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/payment-methods", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/payment-methods/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/payment-methods/{id}");
        var typeAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<PaymentMethod>();
        typeAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/payment-methods/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/payment-methods/{id}");
        var typeAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<PaymentMethod>();
        typeAfterActivate.IsActive.Should().BeTrue();
        
        await this._client.DeleteAsync($"/api/payment-methods/{id}");
    }
}