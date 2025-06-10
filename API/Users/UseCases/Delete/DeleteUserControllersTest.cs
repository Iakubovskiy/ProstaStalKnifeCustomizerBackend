using System.Net;
using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using Application.Users.UseCases.Registration;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Delete;

public class DeleteUserControllersTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public DeleteUserControllersTest(CustomWebAppFactory factory)
    {
        this._factory = factory;
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    private async Task<HttpClient> GetAuthenticatedClientAsync(string email, string password)
    {
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

    private async Task<Guid> RegisterTestUser(HttpClient adminClient, string email, string password, string role)
    {
        var newUserId = Guid.NewGuid();
        var registrationDto = new RegisterDto { Id = newUserId, Email = email, Password = password, PasswordConfirmation = password, Role = role };
        var json = JsonSerializer.Serialize(registrationDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await adminClient.PostAsync("/api/admin/register", content);
        response.EnsureSuccessStatusCode();
        return newUserId;
    }
    
    [Fact]
    public async Task DeleteUser_ShouldSucceed_WhenCalledByAdmin()
    {
        var adminClient = await GetAuthenticatedClientAsync("admin@example.com", "Admin123!");
        var userIdToDelete = await RegisterTestUser(adminClient, "deletethis@example.com", "Password123!", "User");

        var response = await adminClient.DeleteAsync($"/api/users/{userIdToDelete}");
        
        response.EnsureSuccessStatusCode();

        var getResponse = await adminClient.GetAsync($"/api/users/{userIdToDelete}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeleteCurrentUser_ShouldSucceed_WhenAuthenticated()
    {
        var adminClient = await GetAuthenticatedClientAsync("admin@example.com", "Admin123!");
        var userEmail = "deleteme@example.com";
        var userPassword = "Password123!";
        await RegisterTestUser(adminClient, userEmail, userPassword, "User");
        
        var userClient = await GetAuthenticatedClientAsync(userEmail, userPassword);
        
        var response = await userClient.DeleteAsync("/api/users/me");
        
        response.EnsureSuccessStatusCode();

        var loginDto = new LoginDto { Username = userEmail, Password = userPassword };
        var json = JsonSerializer.Serialize(loginDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var loginResponse = await this._client.PostAsync("/api/login", content);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnForbidden_WhenUserTriesToDeleteAdmin()
    {
        var userClient = await GetAuthenticatedClientAsync("user@example.com", "User123!");
        var adminId = new Guid("906a1671-2648-4fa2-85e6-2bd521928b8b");
        
        var response = await userClient.DeleteAsync($"/api/users/{adminId}");
        
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}