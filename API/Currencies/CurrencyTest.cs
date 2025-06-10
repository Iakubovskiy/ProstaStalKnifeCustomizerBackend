using System.Net;
using System.Text;
using System.Text.Json;
using Domain.Currencies;
using FluentAssertions;
using Xunit;

namespace API.Currencies;

public class CurrencyTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public CurrencyTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllCurrencies()
    {
        var response = await this._client.GetAsync("/api/currencies");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Currency>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(3);
    }

    [Theory]
    [InlineData("44b659d8-08a7-48c0-9748-573f5d600739", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/currencies/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        var currency = await response.Content.ReadFromJsonAsync<Currency>();
        currency.Should().NotBeNull();
        currency.Name.Should().Be("uah");
        currency.ExchangeRate.Should().Be(1.0);
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedCurrency()
    {
        var newCurrencyId = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d");
        var newCurrency = new Currency(newCurrencyId, "PLN", 10.5);

        var json = JsonSerializer.Serialize(newCurrency, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/currencies", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/currencies/{newCurrencyId}");
        responseFromGet.EnsureSuccessStatusCode();
        
        var fetchedCurrency = await responseFromGet.Content.ReadFromJsonAsync<Currency>();
        fetchedCurrency.Should().NotBeNull();
        fetchedCurrency.Name.Should().Be("pln");
        fetchedCurrency.ExchangeRate.Should().Be(10.5);
        
        await this._client.DeleteAsync($"/api/currencies/{newCurrencyId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndUpdateCurrency()
    {
        var currencyToUpdateId = new Guid("5c5da2f8-8109-4555-9738-0b8a7805323b");
        var updatedCurrency = new Currency(currencyToUpdateId, "USD_Updated", 42.5);
        
        var json = JsonSerializer.Serialize(updatedCurrency, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/currencies/{currencyToUpdateId}", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseFromGet = await this._client.GetAsync($"/api/currencies/{currencyToUpdateId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetchedCurrency = await responseFromGet.Content.ReadFromJsonAsync<Currency>();
        
        fetchedCurrency.Should().NotBeNull();
        fetchedCurrency.Name.Should().Be("usd_updated");
        fetchedCurrency.ExchangeRate.Should().Be(42.5);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var nonExistentId = new Guid("00000000-0000-0000-0000-000000000000");
        var updatedCurrency = new Currency(nonExistentId, "test", 1.0);
        var json = JsonSerializer.Serialize(updatedCurrency, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/currencies/{nonExistentId}", content);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    
    [Fact]
    public async Task Delete_ShouldSucceedForExistingCurrency()
    {
        var newCurrencyId = new Guid("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e");
        var newCurrency = new Currency(newCurrencyId, "todelete", 5.0);
        var json = JsonSerializer.Serialize(newCurrency, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var postResponse = await this._client.PostAsync("/api/currencies", content);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var deleteResponse = await this._client.DeleteAsync($"/api/currencies/{newCurrencyId}");
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var isDeleted = await deleteResponse.Content.ReadFromJsonAsync<bool>();
        isDeleted.Should().BeTrue();
        
        var getResponse = await this._client.GetAsync($"/api/currencies/{newCurrencyId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}