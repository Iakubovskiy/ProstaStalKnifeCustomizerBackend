using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class UserRepository : Repository<User, string>
    {
        private readonly DBContext _context;
        public UserRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User> Create(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> Update(string id, User newUser)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
            existingUser.Email = newUser.Email;
            existingUser.UserName = newUser.UserName;
            existingUser.PhoneNumber = newUser.PhoneNumber;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> Delete(string id)
        {
            var User = await _context.Users.FirstOrDefaultAsync(a => a.Id == id);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
