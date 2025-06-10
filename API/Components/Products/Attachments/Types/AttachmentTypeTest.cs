using System.Net;
using System.Text;
using System.Text.Json;
using Application.Components.Products.Attachments.Type;
using Domain.Component.Product.Attachments;
using FluentAssertions;
using Xunit;

namespace API.Components.Products.Attachments.Types;

public class AttachmentTypeTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public AttachmentTypeTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task ShouldReturnAttachmentTypes()
    {
        var response = await this._client.GetAsync("/api/attachment-types");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<AttachmentType>>();
        
        data.Should().NotBeNull();
        data.Count.Should().Be(10);
    }

    [Theory]
    [InlineData("1a1b1c1d-1e1f-4a2b-8c3d-4e5f6a7b8c9d", HttpStatusCode.OK)]
    [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.NotFound)]
    public async Task GetAttachmentTypeByIdTest(Guid id, HttpStatusCode expectedStatusCode)
    {
        var response = await this._client.GetAsync($"/api/attachment-types/{id}");
        
        response.StatusCode.Should().Be(expectedStatusCode);

        if (expectedStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        var attachmentType = await response.Content.ReadFromJsonAsync<AttachmentType>();
        attachmentType.Should().NotBeNull();
        attachmentType.Name.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Belt Clip");
        attachmentType.Name.TranslationDictionary.Should().ContainKey("ua").WhoseValue.Should().Be("Кліпса на пояс");
    }

    [Fact]
    public async Task CreateAttachmentTypeTest()
    {
        var newAttachmentType = new AttachmentTypeDto
        {
            Name = new Dictionary<string, string>
            {
                { "en", "Test Attachment Type" },
                { "ua", "Тестовий Тип Кріплення" }
            }
        };

        var json = JsonSerializer.Serialize(newAttachmentType, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/attachment-types", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var createdType = await response.Content.ReadFromJsonAsync<AttachmentType>();
        createdType.Should().NotBeNull();
        createdType.Id.Should().NotBeEmpty();
        
        var responseFromGet = await this._client.GetAsync($"/api/attachment-types/{createdType.Id}");
        responseFromGet.EnsureSuccessStatusCode();
        
        var fetchedType = await responseFromGet.Content.ReadFromJsonAsync<AttachmentType>();
        fetchedType.Name.TranslationDictionary.Should().ContainKey("en").WhoseValue.Should().Be("Test Attachment Type");
        
        await this._client.DeleteAsync($"/api/attachment-types/{createdType.Id}");
    }
    
    [Fact]
    public async Task DeleteAttachmentTypeTest()
    {
        var newAttachmentType = new AttachmentTypeDto
        {
            Name = new Dictionary<string, string>
            {
                { "en", "To Be Deleted" },
                { "ua", "Для Видалення" }
            }
        };
        
        var json = JsonSerializer.Serialize(newAttachmentType, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var postResponse = await this._client.PostAsync("/api/attachment-types", content);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdType = await postResponse.Content.ReadFromJsonAsync<AttachmentType>();
        createdType.Should().NotBeNull();

        var deleteResponse = await this._client.DeleteAsync($"/api/attachment-types/{createdType.Id}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var isDeleted = await deleteResponse.Content.ReadFromJsonAsync<bool>();
        isDeleted.Should().BeTrue();
        
        var getResponse = await this._client.GetAsync($"/api/attachment-types/{createdType.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}