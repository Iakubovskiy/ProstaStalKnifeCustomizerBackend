using System.Net;
using Domain.Files;
using FluentAssertions;
using Xunit;

namespace API.Files;

public class FileTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public FileTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateFile_ShouldSucceed_AndReturnCorrectCloudFrontUrl()
    {
        var fileContentBytes = new byte[] { 1, 2, 3, 4, 5 };
        var fileContent = new ByteArrayContent(fileContentBytes);
        var formData = new MultipartFormDataContent();
        var fileName = "test-file.png";
        formData.Add(fileContent, "file", fileName);
        
        var response = await this._client.PostAsync("/api/files", formData);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var createdFile = await response.Content.ReadFromJsonAsync<FileEntity>();
        createdFile.Should().NotBeNull();
        createdFile.Id.Should().NotBeEmpty();
        createdFile.FileUrl.Should().NotBeNullOrEmpty();
        
        createdFile.FileUrl.Should().Contain("dn3q1zwh2eggm.cloudfront.net");
        createdFile.FileUrl.Should().EndWith(fileName);
    }
    
    [Fact]
    public async Task CreateFile_ShouldReturnBadRequest_WhenFileIsMissing()
    {
        var formData = new MultipartFormDataContent();
        
        var response = await this._client.PostAsync("/api/files", formData);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}