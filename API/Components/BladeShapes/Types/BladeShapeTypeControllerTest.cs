using System.Net;
using System.Text;
using System.Text.Json;
using Domain.Component.BladeShapes.BladeShapeTypes;
using FluentAssertions;
using Xunit;

namespace API.Components.BladeShapes.Types;

public class BladeShapeTypeControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public BladeShapeTypeControllerTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllBladeShapeTypes()
    {
        var response = await this._client.GetAsync("/api/blade-shape-types");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<BladeShapeType>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Theory]
    [InlineData("1a2b3c4d-5e6f-4789-a012-3456789abcde", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/blade-shape-types/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var shapeType = await response.Content.ReadFromJsonAsync<BladeShapeType>();
            shapeType.Should().NotBeNull();
            shapeType.Name.Should().Be("Drop Point");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedBladeShapeType()
    {
        var newId = Guid.NewGuid();
        var newShapeType = new BladeShapeType(newId, "Test Type");
        
        var json = JsonSerializer.Serialize(newShapeType, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/blade-shape-types", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/blade-shape-types/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<BladeShapeType>();
        fetched.Name.Should().Be("Test Type");

        await this._client.DeleteAsync($"/api/blade-shape-types/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = new Guid("2b3c4d5e-6f7a-4890-b123-456789abcdef");
        var updatedShapeType = new BladeShapeType(idToUpdate, "Updated Clip Point");
        
        var json = JsonSerializer.Serialize(updatedShapeType, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/blade-shape-types/{idToUpdate}", content);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/blade-shape-types/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<BladeShapeType>();
        
        fetched.Name.Should().Be("Updated Clip Point");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveBladeShapeType()
    {
        var idToDelete = Guid.NewGuid();
        var newShapeType = new BladeShapeType(idToDelete, "To Be Deleted");
        var json = JsonSerializer.Serialize(newShapeType, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/blade-shape-types", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/blade-shape-types/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var isDeleted = await deleteResponse.Content.ReadFromJsonAsync<bool>();
        isDeleted.Should().BeTrue();
        
        var getResponse = await this._client.GetAsync($"/api/blade-shape-types/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}