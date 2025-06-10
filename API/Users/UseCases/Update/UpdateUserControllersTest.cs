using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using Application.Users.UseCases.Registration;
using Application.Users.UseCases.Update;
using Domain.Order.Support;
using Domain.Users;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Update;

public class UpdateUserControllersTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebAppFactory _factory;
    private readonly JsonSerializerOptions _jsonOptions;

    public UpdateUserControllersTest(CustomWebAppFactory factory)
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
    public async Task UpdateUser_ShouldSucceed_WhenCalledByAdmin()
    {
        var adminClient = await GetAuthenticatedClientAsync("admin@example.com", "Admin123!");
        var userToUpdateId = await RegisterTestUser(adminClient, "user.to.update@example.com", "Password123!", "User");

        var updateDto = new UpdateUserDto { Email = "updated.by.admin@example.com" };
        var json = JsonSerializer.Serialize(updateDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await adminClient.PutAsync($"/api/users/{userToUpdateId}", content);
        response.EnsureSuccessStatusCode();

        var getResponse = await adminClient.GetAsync($"/api/users/{userToUpdateId}");
        var updatedUser = await getResponse.Content.ReadFromJsonAsync<User>();
        updatedUser.Email.Should().Be("updated.by.admin@example.com");

        await adminClient.DeleteAsync($"/api/users/{userToUpdateId}");
    }

    [Fact]
    public async Task UpdateCurrentUser_ShouldSucceed_WhenAuthenticated()
    {
        var adminClient = await GetAuthenticatedClientAsync("admin@example.com", "Admin123!");
        var userEmail = "user.to.update.self@example.com";
        var userPassword = "Password123!";
        var userId = await RegisterTestUser(adminClient, userEmail, userPassword, "User");
        
        var userClient = await GetAuthenticatedClientAsync(userEmail, userPassword);

        var updateDto = new UpdateUserDto
        {
            Email = "updated.by.self@example.com",
            ClientData = new ClientData("Updated By Self", "+380111111111", "USA", "New York", "updated.by.self@example.com", "New Street 1", "10001")
        };
        var json = JsonSerializer.Serialize(updateDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await userClient.PutAsync("/api/users/me", content);
        response.EnsureSuccessStatusCode();

        var getResponse = await userClient.GetAsync("/api/users/me");
        var updatedUser = await getResponse.Content.ReadFromJsonAsync<User>();
        updatedUser.Email.Should().Be("updated.by.self@example.com");
        updatedUser.UserData.ClientFullName.Should().Be("Updated By Self");

        await adminClient.DeleteAsync($"/api/users/{userId}");
    }
}