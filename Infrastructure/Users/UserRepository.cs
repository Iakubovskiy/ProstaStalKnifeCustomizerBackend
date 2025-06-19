using System.Data.Entity.Core;
using Domain.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users;

public class UserRepository : BaseRepository<User>, IGetUserWithOrder
{
    public UserRepository(DBContext context) : base(context)
    {
    }

    public async Task<User> GetUserWithOrder(Guid id)
    {
        return await this.Set
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderItems)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new ObjectNotFoundException("User not found");
    }
}