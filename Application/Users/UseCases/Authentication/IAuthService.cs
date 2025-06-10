namespace Application.Users.UseCases.Authentication;

public interface IAuthService
{
    public Task<string> Login(LoginDto model);
}