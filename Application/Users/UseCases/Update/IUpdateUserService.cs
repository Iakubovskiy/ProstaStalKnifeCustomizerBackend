using Domain.Users;

namespace Application.Users.UseCases.Update;

public interface IUpdateUserService
{
    Task<User> UpdateAsync(Guid id, UpdateUserDto dto);
}