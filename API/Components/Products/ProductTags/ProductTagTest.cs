using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.SimpleComponents.Products.ProductTags;
using Domain.Component.Product;
using FluentAssertions;
using Xunit;

namespace API.Components.Products.ProductTags;

public class ProductTagTest: IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public ProductTagTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
    }

    [Fact]
    public async Task ShouldReturnProductTags()
    {
        var response = await this._client.GetAsync("/api/product-tags");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<ProductTag>>();
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Theory]
    [InlineData("a1b2c3d4-e5f6-7890-1234-567890abcdef", HttpStatusCode.OK)]
    [InlineData("a1b2c3d4-e5f6-7890-1234-567890abc123", HttpStatusCode.NotFound)]
    public async Task GetProductTagByIdTest(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/product-tags/{id}");
        response.StatusCode.Should().Be(expectedStatusCode);
        if (expectedStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }
        var productTag = await response.Content.ReadFromJsonAsync<ProductTag>();
        productTag.Should().NotBeNull();
        productTag.Tag.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Handmade");
        productTag.Tag.TranslationDictionary.Should().ContainKey("ua").WhoseValue.Should().Be("Ручна робота");
    }

    [Fact]
    public async Task CreateProductTagTest()
    {
        Guid id = new Guid("8540f288-af93-497f-b9bb-5f28e788c05f");
        ProductTagDto newProductTag = new ProductTagDto();
        newProductTag.Id = id;
        newProductTag.Names = new Dictionary<string, string>
        {
            { "en", "Test product tag" },
            { "ua", "Тест тег продукту" },
        };
        var jsonData = JsonSerializer.Serialize(newProductTag, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await this._client.PostAsync("/api/product-tags", content);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/product-tags/{id}");
        responseFromGet.EnsureSuccessStatusCode();
        
        await this._client.DeleteAsync($"/api/product-tags/{id}");
    }

    [Fact]
    public async Task UpdateProductTagTest()
    {
        Guid id = new Guid("b2c3d4e5-f6a7-8901-2345-67890abcdef1");
        ProductTagDto newProductTagData = new ProductTagDto();
        newProductTagData.Id = id;
        newProductTagData.Names = new Dictionary<string, string>
        {
            { "en", "Test product tag update" },
            { "ua", "Тест тег продукту оновлення" },
        };
        var jsonData = JsonSerializer.Serialize(newProductTagData, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await this._client.PutAsync($"/api/product-tags/{id}", content);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseFromGet = await this._client.GetAsync($"/api/product-tags/{id}");
        responseFromGet.EnsureSuccessStatusCode();
        var data = await responseFromGet.Content.ReadFromJsonAsync<ProductTag>();
        data.Should().NotBeNull();
        data.Tag.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Test product tag update");
        data.Tag.TranslationDictionary.Should().ContainKey("ua").WhoseValue.Should().Be("Тест тег продукту оновлення");
    }
    
    [Fact]
    public async Task EngravingTagTest_Delete()
    {
        Guid id = new Guid("542566af-96a4-4bdb-89ae-21f10576bd81");
        ProductTagDto newEngravingTag = new ProductTagDto();
        newEngravingTag.Id = id;
        newEngravingTag.Names = new Dictionary<string, string>
        {
            { "en", "Test delete" },
            { "ua", "Тест видалення" },
        };
        var json = JsonSerializer.Serialize(newEngravingTag, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postResponse = await _client.PostAsync("/api/product-tags", content);
        postResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        
        var deleteResponse = await _client.DeleteAsync($"/api/product-tags/{id}");
        deleteResponse.EnsureSuccessStatusCode();
        var responseContent = await deleteResponse.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
        responseContent.Should().ContainKey("isDeleted").WhoseValue.Should().BeTrue();
        
        var response = await this._client.GetAsync($"/api/product-tags/{id}");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}