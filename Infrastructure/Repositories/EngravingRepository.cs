using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using DbContext = Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class EngravingRepository : IRepository<Engraving, Guid>
    {
        private readonly DbContext _context;
        public EngravingRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<Engraving>> GetAll()
        {
            return await _context.Engravings.ToListAsync();
        }

        public async Task<Engraving> GetById(Guid id)
        {
            return await _context.Engravings.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Engraving not found");
        }

        public async Task<Engraving> Create(Engraving order)
        {
            _context.Engravings.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Engraving> Update(Guid id, Engraving newEngraving)
        {
            Engraving existingEngraving = await _context.Engravings.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Engraving not found");

            existingEngraving.Name = newEngraving.Name;
            existingEngraving.Side = newEngraving.Side;
            existingEngraving.Text = newEngraving.Text;
            existingEngraving.Font = newEngraving.Font;
            if(newEngraving.pictureUrl != null)
            {
                existingEngraving.pictureUrl = newEngraving.pictureUrl;
            }
            existingEngraving.rotationX = newEngraving.rotationX;
            existingEngraving.rotationY = newEngraving.rotationY;
            existingEngraving.rotationZ = newEngraving.rotationZ;
            existingEngraving.locationX = newEngraving.locationX;
            existingEngraving.locationY = newEngraving.locationY;
            existingEngraving.locationZ = newEngraving.locationZ;
            existingEngraving.scaleX = newEngraving.scaleX;
            existingEngraving.scaleY = newEngraving.scaleY;
            existingEngraving.scaleZ = newEngraving.scaleZ;

            await _context.SaveChangesAsync();
            return existingEngraving;
        }

        public async Task<bool> Delete(Guid id)
        {
            Engraving engraving = await _context.Engravings.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Engraving not found");
            _context.Engravings.Remove(engraving);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
