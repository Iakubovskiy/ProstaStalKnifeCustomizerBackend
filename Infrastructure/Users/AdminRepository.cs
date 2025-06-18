using System.Data.Entity.Core;
using Domain.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Users;

public class AdminRepository : BaseRepository<User>, IAdminRepository
{
    public AdminRepository(DBContext context) : base(context)
    {
    }

    public async Task<List<Admin>> GetAdmins()
    {
        return await this.Context.Admins.ToListAsync();
    }

    public async Task<Admin> GetAdminById(Guid id)
    {
        return await this.Context.Admins.FindAsync(id) ?? throw new ObjectNotFoundException("Admin not found");
    }
}