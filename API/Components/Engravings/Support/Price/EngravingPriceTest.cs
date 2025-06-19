using System.Net;
using System.Text;
using System.Text.Json;
using Domain.Component.Engravings.Support;
using FluentAssertions;
using Xunit;

namespace API.Components.Engravings.Support.Price;

public class EngravingPriceTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    private class PriceResponseDto
    {
        public double Price { get; set; }
    }

    public EngravingPriceTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Theory]
    [InlineData("uah", 250.0, HttpStatusCode.OK)]
    [InlineData("usd", 250.0/41.0 , HttpStatusCode.OK)]
    [InlineData("eur", 250.0/44.0, HttpStatusCode.OK)]
    [InlineData(null, 0, HttpStatusCode.BadRequest)]
    [InlineData("xyz", 0, HttpStatusCode.NotFound)]
    public async Task GetEngravingPrice_Test(string currency, double expectedPrice, HttpStatusCode expectedStatusCode)
    {
        
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/engraving-prices");
        if (currency != null)
        {
            request.Headers.Add("Currency", currency);
        }

        var response = await this._client.SendAsync(request);

        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var priceResponse = await response.Content.ReadFromJsonAsync<PriceResponseDto>(this._jsonOptions);
            priceResponse.Should().NotBeNull();
            Math.Abs(priceResponse.Price).Should().Be(Math.Abs(expectedPrice));
        }
    }

    [Fact]
    public async Task CreateEngravingPrice_ShouldSucceed()
    {
        var newPriceId = new Guid("11111111-2222-3333-4444-555555555555");
        var newPrice = new EngravingPrice(newPriceId, 300);
        
        var json = JsonSerializer.Serialize(newPrice, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/engraving-prices", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdPrice = await response.Content.ReadFromJsonAsync<EngravingPrice>();
        createdPrice.Price.Should().Be(300);

        await this._client.DeleteAsync($"/api/engraving-prices/{newPriceId}");
    }

    [Fact]
    public async Task UpdateEngravingPrice_ShouldSucceed()
    {
        var idToUpdate = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var updatedPrice = new EngravingPrice(idToUpdate, 999);

        var json = JsonSerializer.Serialize(updatedPrice, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/engraving-prices/{idToUpdate}", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getRequest = new HttpRequestMessage(HttpMethod.Get, "/api/engraving-prices");
        getRequest.Headers.Add("Currency", "uah");
        var getResponse = await this._client.SendAsync(getRequest);
        var fetchedPriceResponse = await getResponse.Content.ReadFromJsonAsync<PriceResponseDto>(this._jsonOptions);
        
        fetchedPriceResponse.Should().NotBeNull();
        fetchedPriceResponse.Price.Should().Be(999);
    }
    
    [Fact]
    public async Task UpdateEngravingPrice_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var nonExistentId = new Guid("00000000-0000-0000-0000-000000000000");
        var updatedPrice = new EngravingPrice(nonExistentId, 1);
        var json = JsonSerializer.Serialize(updatedPrice, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/engraving-prices/{nonExistentId}", content);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteEngravingPrice_ShouldSucceed()
    {
        var idToDelete = new Guid("22222222-3333-4444-5555-666666666666");
        var newPrice = new EngravingPrice(idToDelete, 500);
        var json = JsonSerializer.Serialize(newPrice, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var postResponse = await this._client.PostAsync("/api/engraving-prices", content);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var deleteResponse = await this._client.DeleteAsync($"/api/engraving-prices/{idToDelete}");
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await deleteResponse.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
        responseContent.Should().ContainKey("isDeleted").WhoseValue.Should().BeTrue();
        
        var secondDeleteResponse = await this._client.DeleteAsync($"/api/engraving-prices/{idToDelete}");
        secondDeleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeleteEngravingPrice_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var nonExistentId = new Guid("00000000-0000-0000-0000-000000000000");
        
        var response = await this._client.DeleteAsync($"/api/engraving-prices/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}