using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.SimpleComponents.Textures;
using Domain.Component.Textures;
using FluentAssertions;
using Xunit;

namespace API.Components.Textures;

public class TextureTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public TextureTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllTextures()
    {
        var response = await this._client.GetAsync("/api/textures");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Texture>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Theory]
    [InlineData("a1b2c3d4-e5f6-4789-abcd-ef0123456789", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/textures/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        var texture = await response.Content.ReadFromJsonAsync<Texture>();
        texture.Should().NotBeNull();
        texture.Name.Should().Be("Metal Brushed");
        texture.NormalMap.Id.Should().Be(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        texture.RoughnessMap.Id.Should().Be(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedTexture()
    {
        var newTextureId = new Guid("11111111-2222-3333-4444-555555555555");
        var fileEntityId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c");

        var newTextureDto = new TextureDto
        {
            Id = newTextureId,
            Name = "Test Texture",
            NormalMapId = fileEntityId,
            RoughnessMapId = fileEntityId
        };

        var json = JsonSerializer.Serialize(newTextureDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/textures", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/textures/{newTextureId}");
        responseFromGet.EnsureSuccessStatusCode();
        
        var fetchedTexture = await responseFromGet.Content.ReadFromJsonAsync<Texture>();
        fetchedTexture.Should().NotBeNull();
        fetchedTexture.Name.Should().Be("Test Texture");
        
        await this._client.DeleteAsync($"/api/textures/{newTextureId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndUpdateTexture()
    {
        var textureToUpdateId = new Guid("b2c3d4e5-f6a7-4890-bcde-f01234567890");
        var newFileEntityId = new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d");

        var updatedTextureDto = new TextureDto
        {
            Id = textureToUpdateId,
            Name = "Updated Wood Oak",
            NormalMapId = newFileEntityId,
            RoughnessMapId = newFileEntityId
        };
        
        var json = JsonSerializer.Serialize(updatedTextureDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/textures/{textureToUpdateId}", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseFromGet = await this._client.GetAsync($"/api/textures/{textureToUpdateId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetchedTexture = await responseFromGet.Content.ReadFromJsonAsync<Texture>();
        
        fetchedTexture.Should().NotBeNull();
        fetchedTexture.Name.Should().Be("Updated Wood Oak");
        fetchedTexture.NormalMap.Id.Should().Be(newFileEntityId);
    }
    
    [Fact]
    public async Task Delete_ShouldSucceedForExistingTexture()
    {
        var newTextureId = new Guid("99999999-8888-7777-6666-555555555555");
        var fileEntityId = new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d");
        var newTextureDto = new TextureDto
        {
            Id = newTextureId,
            Name = "To Be Deleted",
            NormalMapId = fileEntityId,
            RoughnessMapId = fileEntityId
        };
        var json = JsonSerializer.Serialize(newTextureDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/textures", content);

        var deleteResponse = await this._client.DeleteAsync($"/api/textures/{newTextureId}");
        
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var isDeleted = await deleteResponse.Content.ReadFromJsonAsync<bool>();
        isDeleted.Should().BeTrue();
        
        var getResponse = await this._client.GetAsync($"/api/textures/{newTextureId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var nonExistentId = new Guid("00000000-0000-0000-0000-000000000000");
        
        var response = await this._client.DeleteAsync($"/api/textures/{nonExistentId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}