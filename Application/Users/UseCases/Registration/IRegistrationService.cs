using Domain.Users;

namespace Application.Users.UseCases.Registration;

public interface IRegistrationService
{
    public Task<User> Register(RegisterDto registerDto);
}