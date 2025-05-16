using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User, string>
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
            return await _context.Users.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"User with id {id} not found");
        }

        public async Task<User> Create(User order)
        {
            _context.Users.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<User> Update(string id, User newUser)
        {
            User existingUser = await _context.Users.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"User with id {id} not found");
            existingUser.Email = newUser.Email;
            existingUser.UserName = newUser.UserName;
            existingUser.PhoneNumber = newUser.PhoneNumber;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> Delete(string id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"User with id {id} not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
