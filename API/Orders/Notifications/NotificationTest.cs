using System.Net;
using System.Text;
using System.Text.Json;
using Application;
using FluentAssertions;
using Xunit;

namespace API.Orders.Notifications;

public class NotificationTest : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public NotificationTest(CustomWebAppFactory factory)
    {
        this._client = factory.CreateClient();
        this._jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task SendEmail_ShouldReturnOk_WhenDataIsValid()
    {
        var emailDto = new EmailDTO
        {
            EmailTo = "diman.150106@gmail.com",
            EmailSubject = "Test Subject",
            EmailBody = "This is a test email body."
        };
        
        var json = JsonSerializer.Serialize(emailDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await this._client.PostAsync("/api/notifications", content);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        responseContent.Should().ContainKey("message").WhoseValue.Should().Be("Email успішно відправлено");
    }

    [Theory]
    [InlineData(" ", "Test Subject")]
    [InlineData("test@example.com", " ")]
    public async Task SendEmail_ShouldReturnBadRequest_WhenDataIsWhitespace(string emailTo, string subject)
    {
        var emailDto = new EmailDTO
        {
            EmailTo = emailTo,
            EmailSubject = subject,
            EmailBody = "Test Body"
        };

        var json = JsonSerializer.Serialize(emailDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await this._client.PostAsync("/api/notifications", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        responseContent.Should().ContainKey("message").WhoseValue.Should().Be("Некоректні вхідні дані");
    }

    [Theory]
    [InlineData(null, "Test Subject")]
    [InlineData("test@example.com", null)]
    public async Task SendEmail_ShouldReturnBadRequest_WhenRequiredDataIsMissing(string emailTo, string subject)
    {
        var emailDto = new EmailDTO
        {
            EmailTo = emailTo,
            EmailSubject = subject,
            EmailBody = "Test Body"
        };

        var json = JsonSerializer.Serialize(emailDto, this._jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await this._client.PostAsync("/api/notifications", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}