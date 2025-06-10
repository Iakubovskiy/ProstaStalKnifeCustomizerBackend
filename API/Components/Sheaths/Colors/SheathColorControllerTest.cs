using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.ComponentsWithType.UseCases.Get;
using Domain.Component.Sheaths.Color;
using FluentAssertions;
using Xunit;

namespace API.Components.Sheaths.Colors;

public class SheathColorControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public SheathColorControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        this._client.DefaultRequestHeaders.Add("Currency", "uah");
    }

    private object CreateValidDto(Guid? id = null)
    {
        var bladeShapeTypeId = new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde");
        
        return new
        {
            Id = id,
            Color = new Dictionary<string, string> { { "en", "Test Color" } },
            Material = new Dictionary<string, string> { { "en", "Test Material" } },
            ColorCode = "#123456",
            EngravingColorCode = "#654321",
            Prices = new List<object>
            {
                new { TypeId = bladeShapeTypeId, Price = 250.0 }
            },
            IsActive = true,
            TextureId = (Guid?)new Guid("d4e5f6a7-b8c9-4012-def0-123456789012"),
            ColorMapId = (Guid?)new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c")
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllSheathColors()
    {
        var response = await this._client.GetAsync("/api/sheath-colors");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<SheathColorResponsePresenter>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveSheathColors()
    {
        var response = await this._client.GetAsync("/api/sheath-colors/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<SheathColorResponsePresenter>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(s => s.IsActive);
    }

    [Theory]
    [InlineData("a1a1a1a1-1111-4111-8111-a1a1a1a1a1a1", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/sheath-colors/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var sheathColor = await response.Content.ReadFromJsonAsync<SheathColorResponsePresenter>();
            sheathColor.Should().NotBeNull();
            sheathColor.Color["en"].Should().Be("Black");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedSheathColor()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/sheath-colors", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/sheath-colors/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<SheathColorResponsePresenter>();
        fetched.Should().NotBeNull();
        fetched.Color["en"].Should().Be("Test Color");
        fetched.Prices.Should().NotBeEmpty();

        await this._client.DeleteAsync($"/api/sheath-colors/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/sheath-colors", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        (updatedDto as dynamic).Color["en"] = "Updated Color";
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/sheath-colors/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/sheath-colors/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<SheathColorResponsePresenter>();
        fetched.Should().NotBeNull();
        fetched.Color["en"].Should().Be("Updated Color");
        
        await this._client.DeleteAsync($"/api/sheath-colors/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveSheathColor()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/sheath-colors", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/sheath-colors/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/sheath-colors/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/sheath-colors", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/sheath-colors/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/sheath-colors/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<SheathColorResponsePresenter>();
        itemAfterDeactivate.Should().NotBeNull();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/sheath-colors/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/sheath-colors/{id}");
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<SheathColorResponsePresenter>();
        itemAfterActivate.Should().NotBeNull();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/sheath-colors/{id}");
    }
}