using Domain.Users;

namespace Infrastructure.Users;

public interface IAdminRepository
{
    public Task<List<Admin>> GetAdmins();
    public Task<Admin> GetAdminById(Guid id);
}