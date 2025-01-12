using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class EngravingRepository : Repository<Engraving, Guid>
    {
        private readonly DBContext _context;
        public EngravingRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Engraving>> GetAll()
        {
            return await _context.Engravings.ToListAsync();
        }

        public async Task<Engraving> GetById(Guid id)
        {
            return await _context.Engravings.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Engraving> Create(Engraving Engraving)
        {
            _context.Engravings.Add(Engraving);
            await _context.SaveChangesAsync();
            return Engraving;
        }

        public async Task<Engraving> Update(Guid id, Engraving newEngraving)
        {
            var existingEngraving = await _context.Engravings.FirstOrDefaultAsync(a => a.Id == id);

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
            var Engraving = await _context.Engravings.FirstOrDefaultAsync(a => a.Id == id);
            _context.Engravings.Remove(Engraving);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
