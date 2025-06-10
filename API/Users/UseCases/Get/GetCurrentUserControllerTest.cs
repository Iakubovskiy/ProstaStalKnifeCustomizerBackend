using System.Net;
using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using Domain.Users;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Get;

public class GetCurrentUserControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public GetCurrentUserControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetCurrentUser_ShouldReturnCurrentUser_WhenAuthenticated()
    {
        var loginDto = new LoginDto { Username = "user@example.com", Password = "User123!" };
        var json = JsonSerializer.Serialize(loginDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var responseLogin = await this._client.PostAsync("/api/login", content);
        var tokenData = await responseLogin.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        this._client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData["token"]);
        
        var response = await this._client.GetAsync("/api/users/me");

        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<User>();
        user.Should().NotBeNull();
        user.Email.Should().Be("user@example.com");
        user.Id.Should().Be(new Guid("5e7ca909-b9cb-492a-9bfe-cfe2f3995e15"));
    }

    [Fact]
    public async Task GetCurrentUser_ShouldReturnUnauthorized_WhenNotAuthenticated()
    {
        var response = await this._client.GetAsync("/api/users/me");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}