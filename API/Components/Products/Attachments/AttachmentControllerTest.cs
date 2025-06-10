using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.Products.Attachments;
using Domain.Component.Product.Attachments;
using FluentAssertions;
using Xunit;

namespace API.Components.Products.Attachments;

public class AttachmentControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public AttachmentControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private AttachmentDto CreateValidDto(Guid? id = null)
    {
        return new AttachmentDto
        {
            Id = id,
            IsActive = true,
            ImageFileId = new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            Name = new Dictionary<string, string> { { "en", "Test Attachment" } },
            Title = new Dictionary<string, string> { { "en", "Test Title" } },
            Description = new Dictionary<string, string> { { "en", "Test Description" } },
            MetaTitle = new Dictionary<string, string> { { "en", "Meta Title" } },
            MetaDescription = new Dictionary<string, string> { { "en", "Meta Description" } },
            TagsIds = new List<Guid> { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef") },
            TypeId = new Guid("1a1b1c1d-1e1f-4a2b-8c3d-4e5f6a7b8c9d"),
            Color = new Dictionary<string, string> { { "en", "Black" } },
            Price = 100.0,
            Material = new Dictionary<string, string> { { "en", "Test Material" } },
            ModelFileId = new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d")
        };
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllAttachments()
    {
        var response = await this._client.GetAsync("/api/attachments");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Attachment>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Fact]
    public async Task GetAllActive_ShouldReturnOnlyActiveAttachments()
    {
        var response = await this._client.GetAsync("/api/attachments/active");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<Attachment>>();

        data.Should().NotBeNull();
        data.Count.Should().Be(8);
        data.Should().OnlyContain(a => a.IsActive);
    }

    [Theory]
    [InlineData("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetById_Test(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/attachments/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.OK)
        {
            var attachment = await response.Content.ReadFromJsonAsync<Attachment>();
            attachment.Should().NotBeNull();
            attachment.Name.TranslationDictionary["en"].Should().Be("Titanium Belt Clip");
        }
    }

    [Fact]
    public async Task Create_ShouldSucceedAndReturnCreatedAttachment()
    {
        var newAttachmentId = Guid.NewGuid();
        var newDto = this.CreateValidDto(newAttachmentId);
        var json = JsonSerializer.Serialize(newDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/attachments", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var responseFromGet = await this._client.GetAsync($"/api/attachments/{newAttachmentId}");
        responseFromGet.EnsureSuccessStatusCode();
        var fetched = await responseFromGet.Content.ReadFromJsonAsync<Attachment>();
        fetched.Name.TranslationDictionary["en"].Should().Be("Test Attachment");

        await this._client.DeleteAsync($"/api/attachments/{newAttachmentId}");
    }

    [Fact]
    public async Task Update_ShouldSucceedAndApplyChanges()
    {
        var idToUpdate = Guid.NewGuid();
        var initialDto = this.CreateValidDto(idToUpdate);
        var initialJson = JsonSerializer.Serialize(initialDto, this._jsonOptions);
        var initialContent = new StringContent(initialJson, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/attachments", initialContent);
        
        var updatedDto = this.CreateValidDto(idToUpdate);
        updatedDto.Name["en"] = "Updated Name";
        updatedDto.Price = 999.99;
        var updatedJson = JsonSerializer.Serialize(updatedDto, this._jsonOptions);
        var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

        var response = await this._client.PutAsync($"/api/attachments/{idToUpdate}", updatedContent);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/attachments/{idToUpdate}");
        var fetched = await getResponse.Content.ReadFromJsonAsync<Attachment>();
        
        fetched.Name.TranslationDictionary["en"].Should().Be("Updated Name");
        fetched.Price.Should().Be(999.99);

        await this._client.DeleteAsync($"/api/attachments/{idToUpdate}");
    }

    [Fact]
    public async Task Delete_ShouldSucceedAndRemoveAttachment()
    {
        var idToDelete = Guid.NewGuid();
        var dto = this.CreateValidDto(idToDelete);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/attachments", content);
        
        var deleteResponse = await this._client.DeleteAsync($"/api/attachments/{idToDelete}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getResponse = await this._client.GetAsync($"/api/attachments/{idToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeactivateAndActivate_ShouldChangeIsActiveStatus()
    {
        var id = Guid.NewGuid();
        var dto = this.CreateValidDto(id);
        var json = JsonSerializer.Serialize(dto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await this._client.PostAsync("/api/attachments", content);

        var deactivateResponse = await this._client.PatchAsync($"/api/attachments/deactivate/{id}", null);
        deactivateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterDeactivate = await this._client.GetAsync($"/api/attachments/{id}");
        var attachmentAfterDeactivate = await getAfterDeactivate.Content.ReadFromJsonAsync<Attachment>();
        attachmentAfterDeactivate.IsActive.Should().BeFalse();
        
        var activateResponse = await this._client.PatchAsync($"/api/attachments/activate/{id}", null);
        activateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var getAfterActivate = await this._client.GetAsync($"/api/attachments/{id}");
        var attachmentAfterActivate = await getAfterActivate.Content.ReadFromJsonAsync<Attachment>();
        attachmentAfterActivate.IsActive.Should().BeTrue();

        await this._client.DeleteAsync($"/api/attachments/{id}");
    }
}