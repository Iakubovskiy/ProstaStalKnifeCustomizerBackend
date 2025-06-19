using Domain.Users;

namespace Infrastructure.Users;

public interface IGetUserWithOrder
{
    public Task<User> GetUserWithOrder(Guid id);
}