using System.Net;
using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using Domain.Users;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Get;

public class GetAllUsersControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public GetAllUsersControllerTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private async Task<HttpClient> GetAuthenticatedClientAsync(string role)
    {
        var email = role == "Admin" ? "admin@example.com" : "user@example.com";
        var password = role == "Admin" ? "Admin123!" : "User123!";

        var loginDto = new LoginDto { Username = email, Password = password };
        var json = JsonSerializer.Serialize(loginDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/login", content);
        var tokenData = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        var token = tokenData["token"];

        var authClient = this._factory.CreateClient();
        authClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return authClient;
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnAllUsers_WhenCalledByAdmin()
    {
        var adminClient = await GetAuthenticatedClientAsync("Admin");
        
        var response = await adminClient.GetAsync("/api/users");
        
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<User>>();
        users.Should().NotBeNull();
        users.Count.Should().Be(2);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnForbidden_WhenCalledByUser()
    {
        var userClient = await GetAuthenticatedClientAsync("User");
        
        var response = await userClient.GetAsync("/api/users");
        
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnUnauthorized_WhenCalledAnonymously()
    {
        var response = await this._client.GetAsync("/api/users");
        
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}