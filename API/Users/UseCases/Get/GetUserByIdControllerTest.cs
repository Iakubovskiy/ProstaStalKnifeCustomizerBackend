using System.Net;
using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using Domain.Users;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Get;

public class GetUserByIdControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public GetUserByIdControllerTest(CustomWebAppFactory factory)
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
    public async Task GetUserById_ShouldReturnUser_WhenCalledByAdmin()
    {
        var adminClient = await GetAuthenticatedClientAsync("Admin");
        var userId = new Guid("5e7ca909-b9cb-492a-9bfe-cfe2f3995e15");
        
        var response = await adminClient.GetAsync($"/api/users/{userId}");
        
        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<User>();
        user.Should().NotBeNull();
        user.Email.Should().Be("user@example.com");
    }

    [Fact]
    public async Task GetUserById_ShouldReturnNotFound_ForNonExistentId()
    {
        var adminClient = await GetAuthenticatedClientAsync("Admin");
        var nonExistentId = Guid.NewGuid();
        
        var response = await adminClient.GetAsync($"/api/users/{nonExistentId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task GetUserById_ShouldReturnForbidden_WhenCalledByUser()
    {
        var userClient = await GetAuthenticatedClientAsync("User");
        var adminId = new Guid("906a1671-2648-4fa2-85e6-2bd521928b8b");

        var response = await userClient.GetAsync($"/api/users/{adminId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}