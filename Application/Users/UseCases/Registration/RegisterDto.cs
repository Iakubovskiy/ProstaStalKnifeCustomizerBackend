using Domain.Order.Support;

namespace Application.Users.UseCases.Registration;

public class RegisterDto
{
    public Guid? Id { get; set; }
    public ClientData? ClientData { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public string Role { get; set; }
}