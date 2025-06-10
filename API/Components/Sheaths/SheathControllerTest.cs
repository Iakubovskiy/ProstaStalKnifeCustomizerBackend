using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.SimpleComponents.Sheaths;
using Domain.Component.Sheaths;
using FluentAssertions;
using Xunit;

namespace API.Components.Sheaths;

public class SheathControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public SheathControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        this._client.DefaultRequestHeaders.Add("Currency", "uah");
    }

    private SheathDto CreateValidDto(Guid? id = null)
    {
        return new SheathDto
        {
            Id = id,
            TypeId = new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde"), // Drop Point
            Name = new Dictionary<string, string> { { "en", "Test Sheath" } },
            Price = 500.0,
            SheathModelId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            IsActive = true
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllSheaths()
    {
        var response = await this._client.GetAsync("/api/sheaths");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Sheath>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveSheaths()
    {
        var response = await this._client.GetAsync("/api/sheaths/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Sheath>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(s => s.IsActive);
    }

    [Theory]
    [InlineData("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/sheaths/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var sheath = await response.Content.ReadFromJsonAsync<Sheath>();
            sheath.Should().NotBeNull();
            sheath.Name.TranslationDictionary["en"].Should().Be("Kydex Sheath for Drop Point");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedSheath()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/sheaths", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/sheaths/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<Sheath>();
        fetched.Name.TranslationDictionary["en"].Should().Be("Test Sheath");

        await this._client.DeleteAsync($"/api/sheaths/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/sheaths", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Name["en"] = "Updated Sheath";
        updatedDto.Price = 999.0;
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/sheaths/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/sheaths/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<Sheath>();
        
        fetched.Name.TranslationDictionary["en"].Should().Be("Updated Sheath");
        fetched.Price.Should().Be(999.0);

        await this._client.DeleteAsync($"/api/sheaths/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveSheath()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/sheaths", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/sheaths/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/sheaths/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/sheaths", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/sheaths/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/sheaths/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<Sheath>();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/sheaths/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/sheaths/{id}");
        getAfterActivate.StatusCode.Should().Be(HttpStatusCode.OK);;
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<Sheath>();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/sheaths/{id}");
    }
}