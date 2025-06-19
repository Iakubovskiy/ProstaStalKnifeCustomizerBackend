using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.TexturedComponents.Data.Dto.HandleColors;
using Domain.Component.Handles;
using FluentAssertions;
using Xunit;

namespace API.Components.Handles;

public class HandleControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public HandleControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        this._client.DefaultRequestHeaders.Add("Currency", "uah");
    }

    private HandleColorDto CreateValidDto(Guid? id = null)
    {
        return new HandleColorDto
        {
            Id = id,
            Colors = new Dictionary<string, string> { { "en", "Test Color" } },
            ColorCode = "#123456",
            IsActive = true,
            Materials = new Dictionary<string, string> { { "en", "Test Material" } },
            TextureId = new Guid("c9d0e1f2-a3b4-4567-2345-678901234567"),
            ColorMapId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            HandleModelId = new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"),
            Price = 1234.5,
            BladeShapeTypeId = new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde")
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllHandles()
    {
        var response = await this._client.GetAsync("/api/handles");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Handle>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveHandles()
    {
        var response = await this._client.GetAsync("/api/handles/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Handle>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(a => a.IsActive);
    }

    [Theory]
    [InlineData("11a1b1c1-d1e1-41f1-81a1-b1c1d1e1f1a1", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/handles/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var handle = await response.Content.ReadFromJsonAsync<Handle>();
            handle.Should().NotBeNull();
            handle.Color.TranslationDictionary["en"].Should().Be("Black");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedHandle()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/handles", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/handles/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<Handle>();
        fetched.Color.TranslationDictionary["en"].Should().Be("Test Color");

        await this._client.DeleteAsync($"/api/handles/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/handles", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Colors["en"] = "Updated Color";
        updatedDto.Price = 5432.1;
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/handles/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/handles/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<Handle>();
        
        fetched.Color.TranslationDictionary["en"].Should().Be("Updated Color");
        fetched.Price.Should().Be(5432.1);

        await this._client.DeleteAsync($"/api/handles/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveHandle()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/handles", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/handles/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/handles/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/handles", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/handles/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/handles/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<Handle>();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/handles/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/handles/{id}");
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<Handle>();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/handles/{id}");
    }
}