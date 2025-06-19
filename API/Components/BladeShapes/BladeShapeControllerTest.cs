using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.SimpleComponents.BladeShapes;
using Domain.Component.BladeShapes;
using FluentAssertions;
using Xunit;

namespace API.Components.BladeShapes;

public class BladeShapeControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public BladeShapeControllerTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private BladeShapeDto CreateValidDto(Guid? id = null)
    {
        return new BladeShapeDto
        {
            Id = id,
            TypeId = new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde"),
            Names = new Dictionary<string, string> { { "en", "Test Blade Shape" } },
            Price = 1000.0,
            TotalLength = 200,
            BladeLength = 100,
            BladeWidth = 30,
            BladeWeight = 150,
            SharpeningAngle = 20,
            RockwellHardnessUnits = 58,
            BladeShapePhotoId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            BladeShapeModelId = new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"),
            IsActive = true,
            SheathId = new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d")
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllBladeShapes()
    {
        var response = await this._client.GetAsync("/api/blade-shapes");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<BladeShape>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveBladeShapes()
    {
        var response = await this._client.GetAsync("/api/blade-shapes/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<BladeShape>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(a => a.IsActive);
    }

    [Theory]
    [InlineData("1f768e1c-7201-4a3d-9d48-a9de3f2b6e7a", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/blade-shapes/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var shape = await response.Content.ReadFromJsonAsync<BladeShape>();
            shape.Should().NotBeNull();
            shape.Name.TranslationDictionary["en"].Should().Be("Classic Drop Point");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedBladeShape()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/blade-shapes", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/blade-shapes/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<BladeShape>();
        fetched.Name.TranslationDictionary["en"].Should().Be("Test Blade Shape");

        await this._client.DeleteAsync($"/api/blade-shapes/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-shapes", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Names["en"] = "Updated Blade Shape";
        updatedDto.Price = 1999.99;
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/blade-shapes/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/blade-shapes/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<BladeShape>();
        
        fetched.Name.TranslationDictionary["en"].Should().Be("Updated Blade Shape");
        fetched.Price.Should().Be(1999.99);

        await this._client.DeleteAsync($"/api/blade-shapes/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveBladeShape()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-shapes", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/blade-shapes/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/blade-shapes/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-shapes", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/blade-shapes/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/blade-shapes/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<BladeShape>();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/blade-shapes/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/blade-shapes/{id}");
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<BladeShape>();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/blade-shapes/{id}");
    }

    [Fact]
    public async Task Activate_ShouldReturnNotFound_ForNonExistentId()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await this._client.PatchAsync($"/api/blade-shapes/activate/{nonExistentId}", null);
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}