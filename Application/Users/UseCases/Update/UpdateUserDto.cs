using Domain.Order.Support;

namespace Application.Users.UseCases.Update;

public class UpdateUserDto
{
    public ClientData? ClientData { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirmation { get; set; }
}