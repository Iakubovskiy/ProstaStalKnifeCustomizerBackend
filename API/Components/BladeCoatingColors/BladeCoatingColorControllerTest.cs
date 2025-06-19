using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.TexturedComponents.Data.Dto.BladeCoatings;
using Domain.Component.BladeCoatingColors;
using FluentAssertions;
using Xunit;

namespace API.Components.BladeCoatingColors;

public class BladeCoatingColorControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public BladeCoatingColorControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private BladeCoatingDto CreateValidDto(Guid? id = null)
    {
        return new BladeCoatingDto
        {
            Id = id,
            Types = new Dictionary<string, string> { { "en", "Test Coating" } },
            Colors = new Dictionary<string, string> { { "en", "Test Color" } },
            ColorCode = "#123456",
            EngravingColorCode = "#654321",
            ColorMapId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            Price = 150.0,
            TextureId = new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789")
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllBladeCoatingColors()
    {
        var response = await this._client.GetAsync("/api/blade-coating-colors");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<BladeCoatingColor>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveBladeCoatingColors()
    {
        var response = await this._client.GetAsync("/api/blade-coating-colors/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<BladeCoatingColor>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(a => a.IsActive);
    }

    [Theory]
    [InlineData("ecf8f3c7-1b3d-4e9a-8c4f-6a2b8d9c1e0a", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/blade-coating-colors/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var coating = await response.Content.ReadFromJsonAsync<BladeCoatingColor>();
            coating.Should().NotBeNull();
            coating.Type.TranslationDictionary["en"].Should().Be("Satin Finish");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedBladeCoatingColor()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/blade-coating-colors", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/blade-coating-colors/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<BladeCoatingColor>();
        fetched.Type.TranslationDictionary["en"].Should().Be("Test Coating");

        var deleteResult = await this._client.DeleteAsync($"/api/blade-coating-colors/{newId}");
        deleteResult.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-coating-colors", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Types["en"] = "Updated Coating";
        updatedDto.Price = 999.99;
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/blade-coating-colors/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/blade-coating-colors/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<BladeCoatingColor>();
        
        fetched.Type.TranslationDictionary["en"].Should().Be("Updated Coating");
        fetched.Price.Should().Be(999.99);

        await this._client.DeleteAsync($"/api/blade-coating-colors/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveBladeCoatingColor()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-coating-colors", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/blade-coating-colors/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/blade-coating-colors/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-coating-colors", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/blade-coating-colors/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/blade-coating-colors/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<BladeCoatingColor>();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/blade-coating-colors/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/blade-coating-colors/{id}");
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<BladeCoatingColor>();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/blade-coating-colors/{id}");
    }
}