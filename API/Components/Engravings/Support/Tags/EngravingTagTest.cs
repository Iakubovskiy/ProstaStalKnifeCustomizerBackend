using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.SimpleComponents.Engravings.EngravingTags;
using Domain.Component.Engravings.Support;
using FluentAssertions;
using Xunit;

namespace API.Components.Engravings.Support.Tags;

public class EngravingTagTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public EngravingTagTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
    }

    [Fact]
    public async Task EngravingTagTest_Get()
    {
        CustomWebAppFactory factory = new CustomWebAppFactory();
        var response = await this._client.GetAsync("/api/engraving-tags");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<EngravingTag>>();
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }
    
    [Theory]
    [InlineData("40050eb8-9c1e-48a4-8070-4edefd1c08f3", HttpStatusCode.OK)]
    [InlineData("40050eb8-9c1e-48a4-8070-4edefd1c0821", HttpStatusCode.NotFound)]
    public async Task EngravingTagTest_GetById(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/engraving-tags/{id}");
        response.StatusCode.Should().Be(expectedStatusCode);
        if (expectedStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }
        var data = await response.Content.ReadFromJsonAsync<EngravingTag>();
        data.Should().NotBeNull();
        data.Name.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Scandinavian");
        data.Name.TranslationDictionary.Should().ContainKey("ua").WhoseValue.Should().Be("Скандинавське");
    }
    [Fact]
    public async Task EngravingTagTest_Create()
    {
        Guid id = new Guid("542566af-96a4-4bdb-89ae-44f10576bd8b");
        EngravingTagDto newEngravingTag = new EngravingTagDto();
        newEngravingTag.Id = id;
        newEngravingTag.Names = new Dictionary<string, string>
        {
            { "en", "Test" },
            { "ua", "Тест" },
        };
        var json = JsonSerializer.Serialize(newEngravingTag, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postResponse = await _client.PostAsync("/api/engraving-tags", content);
        postResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        
        var response = await this._client.GetAsync($"/api/engraving-tags/{id}");
        var data = await response.Content.ReadFromJsonAsync<EngravingTag>();
        data.Should().NotBeNull();
        data.Name.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Test");
        data.Name.TranslationDictionary.Should().ContainKey("ua").WhoseValue.Should().Be("Тест");
    }
    [Fact]
    public async Task EngravingTagTest_Update()
    {
        Guid id = new Guid("40050eb8-9c1e-48a4-8070-4edefd1c08f3");
        EngravingTagDto newEngravingTag = new EngravingTagDto();
        newEngravingTag.Id = id;
        newEngravingTag.Names = new Dictionary<string, string>
        {
            { "en", "Test English" },
            { "ua", "Тест укр" },
        };
        var json = JsonSerializer.Serialize(newEngravingTag, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var putResponse = await _client.PutAsync($"/api/engraving-tags/{id}", content);
        putResponse.EnsureSuccessStatusCode();
        
        var response = await this._client.GetAsync($"/api/engraving-tags/{id}");
        var data = await response.Content.ReadFromJsonAsync<EngravingTag>();
        data.Should().NotBeNull();
        data.Name.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Test English");
        data.Name.TranslationDictionary.Should().ContainKey("ua").WhoseValue.Should().Be("Тест укр");
    }
    [Fact]
    public async Task EngravingTagTest_Delete()
    {
        Guid id = new Guid("542566af-96a4-4bdb-89ae-44f10576bd81");
        EngravingTagDto newEngravingTag = new EngravingTagDto();
        newEngravingTag.Id = id;
        newEngravingTag.Names = new Dictionary<string, string>
        {
            { "en", "Test" },
            { "ua", "Тест" },
        };
        var json = JsonSerializer.Serialize(newEngravingTag, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postResponse = await _client.PostAsync("/api/engraving-tags", content);
        postResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        
        var deleteResponse = await _client.DeleteAsync($"/api/engraving-tags/{id}");
        deleteResponse.EnsureSuccessStatusCode();
        var responseContent = await deleteResponse.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
        responseContent.Should().ContainKey("isDeleted").WhoseValue.Should().BeTrue();
        
        var response = await this._client.GetAsync($"/api/engraving-tags/{id}");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}