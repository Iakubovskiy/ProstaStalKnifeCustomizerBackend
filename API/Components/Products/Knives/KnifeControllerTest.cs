using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.Products.Attachments;
using Application.Components.Products.Knives;
using Application.Components.SimpleComponents.Engravings;
using Domain.Component.Product.Knife;
using FluentAssertions;
using Xunit;

namespace API.Components.Products.Knives;

public class KnifeControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public KnifeControllerTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        this._client.DefaultRequestHeaders.Add("Currency", "uah");
    }

    private KnifeDto CreateValidDto(Guid? id = null)
    {
        return new KnifeDto
        {
            Id = id,
            IsActive = true,
            ImageFileId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            Names = new Dictionary<string, string> { { "en", "Test Knife" } },
            Titles = new Dictionary<string, string> { { "en", "Test Title" } },
            Descriptions = new Dictionary<string, string> { { "en", "Test Description" } },
            MetaTitles = new Dictionary<string, string> { { "en", "Meta Title" } },
            MetaDescriptions = new Dictionary<string, string> { { "en", "Meta Description" } },
            TagsIds = new List<Guid> { new Guid("a7b8c9d0-e1f2-3456-789a-bcdef1234567") },
            ShapeId = new Guid("1f768e1c-7201-4a3d-9d48-a9de3f2b6e7a"),
            BladeCoatingColorId = new Guid("ecf8f3c7-1b3d-4e9a-8c4f-6a2b8d9c1e0a"),
            HandleId = new Guid("11a1b1c1-d1e1-41f1-81a1-b1c1d1e1f1a1"),
            SheathId = new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
            SheathColorId = new Guid("a1a1a1a1-1111-4111-8111-a1a1a1a1a1a1"),
            ExistingEngravingIds = new List<Guid> { new Guid("e1a1b2c3-d4e5-4f6a-8b9c-0d1e2f3a4b5c") },
            NewEngravings = new List<EngravingDto>(),
            ExistingAttachmentIds = new List<Guid> { new Guid("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa") },
            NewAttachments = new List<AttachmentDto>()
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllKnives()
    {
        var response = await this._client.GetAsync("/api/knives");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Knife>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveKnives()
    {
        var response = await this._client.GetAsync("/api/knives/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Knife>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(9);
        data.Should().OnlyContain(k => k.IsActive);
    }

    [Theory]
    [InlineData("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/knives/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var knife = await response.Content.ReadFromJsonAsync<Knife>();
            knife.Should().NotBeNull();
            knife.Name.TranslationDictionary["en"].Should().Be("Operator");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedKnife()
    {
        var newId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/knives", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/knives/{newId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<Knife>();
        fetched.Should().NotBeNull();
        fetched.Name.TranslationDictionary["en"].Should().Be("Test Knife");

        await this._client.DeleteAsync($"/api/knives/{newId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/knives", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Names["en"] = "Updated Knife";
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/knives/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/knives/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<Knife>();
        fetched.Should().NotBeNull();
        fetched.Name.TranslationDictionary["en"].Should().Be("Updated Knife");

        await this._client.DeleteAsync($"/api/knives/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveKnife()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/knives", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/knives/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/knives/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/knives", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/knives/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/knives/{id}");
        var itemAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<Knife>();
        itemAfterDeactivate.Should().NotBeNull();
        itemAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/knives/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/knives/{id}");
        var itemAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<Knife>();
        itemAfterActivate.Should().NotBeNull();
        itemAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/knives/{id}");
    }
}