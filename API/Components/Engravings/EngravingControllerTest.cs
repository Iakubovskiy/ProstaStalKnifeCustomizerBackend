using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.SimpleComponents.Engravings;
using Domain.Component.Engravings;
using FluentAssertions;
using Xunit;

namespace API.Components.Engravings;

public class EngravingControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public EngravingControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        this._client.DefaultRequestHeaders.Add("Currency", "uah");
    }

    private EngravingDto CreateValidDto(Guid? id = null)
    {
        return new EngravingDto
        {
            Id = id,
            Name = new Dictionary<string, string> { { "en", "Test Engraving" } },
            Description = new Dictionary<string, string> { { "en", "Test Description" } },
            PictureId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            Side = 1,
            Text = "Test Text",
            Font = "Arial",
            LocationX = 1.0, LocationY = 2.0, LocationZ = 3.0,
            RotationX = 4.0, RotationY = 5.0, RotationZ = 6.0,
            ScaleX = 7.0, ScaleY = 8.0, ScaleZ = 9.0,
            TagsIds = new List<Guid> { new Guid("40050eb8-9c1e-48a4-8070-4edefd1c08f3") },
            IsActive = true
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllEngravings()
    {
        var response = await this._client.GetAsync("/api/engravings");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Engraving>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }
    
    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveEngravings()
    {
        var response = await this._client.GetAsync("/api/engravings/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Engraving>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(a => a.IsActive);
    }

    [Theory]
    [InlineData("e1a1b2c3-d4e5-4f6a-8b9c-0d1e2f3a4b5c", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/engravings/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var engraving = await response.Content.ReadFromJsonAsync<Engraving>();
            engraving.Should().NotBeNull();
            engraving.Name.TranslationDictionary["en"].Should().Be("Vegvisir Compass");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedEngraving()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/engravings", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/engravings/{newId}");
        responseFromGet.EnsureSuccessStatusCode();

        await this._client.DeleteAsync($"/api/engravings/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/engravings", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Name["en"] = "Updated Engraving";
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/engravings/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/engravings/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<Engraving>();
        fetched.Name.TranslationDictionary["en"].Should().Be("Updated Engraving");
        
        await this._client.DeleteAsync($"/api/engravings/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveEngraving()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/engravings", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/engravings/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/engravings/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/engravings", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/engravings/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/engravings/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<Engraving>();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/engravings/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/engravings/{id}");
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<Engraving>();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/engravings/{id}");
    }
}