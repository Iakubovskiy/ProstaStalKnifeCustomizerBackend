using System.Net;
using System.Text;
using System.Text.Json;
using Application.Users.UseCases.Authentication;
using Application.Users.UseCases.Registration;
using FluentAssertions;
using Xunit;

namespace API.Users.UseCases.Register;

public class RegistrationControllerTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly CustomWebAppFactory _factory;

    public RegistrationControllerTest(CustomWebAppFactory factory)
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
    public async Task Register_ShouldCreateUser_WithValidData()
    {
        var newUserId = new Guid("11111111-1111-1111-1111-111111111111");
        var registrationDto = new RegisterDto
        {
            Id = newUserId,
            Email = "newuser.test@example.com",
            Password = "Password123!",
            PasswordConfirmation = "Password123!",
            Role = "User"
        };
        var json = JsonSerializer.Serialize(registrationDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/register", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var adminClient = await GetAuthenticatedClientAsync("Admin");
        await adminClient.DeleteAsync($"/api/users/{newUserId}");
    }
    
    [Fact]
    public async Task Register_ShouldForbid_WhenRoleIsNotUser()
    {
        var registrationDto = new RegisterDto
        {
            Email = "newadmin.test@example.com",
            Password = "Password123!",
            PasswordConfirmation = "Password123!",
            Role = "Admin"
        };
        var json = JsonSerializer.Serialize(registrationDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/register", content);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
    
    [Fact]
    public async Task RegisterByAdmin_ShouldSucceed_WhenCalledByAdmin()
    {
        var adminClient = await GetAuthenticatedClientAsync("Admin");
        var newUserId = new Guid("22222222-2222-2222-2222-222222222222");
        var registrationDto = new RegisterDto
        {
            Id = newUserId,
            Email = "newadmin.test@example.com",
            Password = "Password123!",
            PasswordConfirmation = "Password123!",
            Role = "Admin"
        };
        var json = JsonSerializer.Serialize(registrationDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await adminClient.PostAsync("/api/admin/register", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        await adminClient.DeleteAsync($"/api/users/{newUserId}");
    }

    [Fact]
    public async Task RegisterByAdmin_ShouldReturnForbidden_WhenCalledByUser()
    {
        var userClient = await GetAuthenticatedClientAsync("User");
        var registrationDto = new RegisterDto { Role = "Admin", Email = "any@mail.com", Password = "p", PasswordConfirmation = "p" };
        var json = JsonSerializer.Serialize(registrationDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await userClient.PostAsync("/api/admin/register", content);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}