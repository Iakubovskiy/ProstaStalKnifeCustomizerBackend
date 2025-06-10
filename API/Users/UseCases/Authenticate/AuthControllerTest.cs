using System.Net;
using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Authenticate;

public class AuthControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public AuthControllerTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Theory]
    [InlineData("admin@example.com", "Admin123!")]
    [InlineData("user@example.com", "User123!")]
    public async Task Login_ShouldReturnToken_WithValidCredentials(string username, string password)
    {
        var loginDto = new LoginDto { Username = username, Password = password };
        var json = JsonSerializer.Serialize(loginDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/login", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        responseContent.Should().ContainKey("token");
        responseContent["token"].Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("admin@example.com", "WrongPassword")]
    [InlineData("nonexistent@example.com", "AnyPassword")]
    public async Task Login_ShouldReturnUnauthorized_WithInvalidCredentials(string username, string password)
    {
        var loginDto = new LoginDto { Username = username, Password = password };
        var json = JsonSerializer.Serialize(loginDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/login", content);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Logout_ShouldReturnOk()
    {
        var response = await this._client.PostAsync("/api/logout", null);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}