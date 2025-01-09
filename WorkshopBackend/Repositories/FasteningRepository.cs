using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class FasteningRepository : Repository<Fastening, int>
    {
        private readonly DBContext _context;
        public FasteningRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Fastening>> GetAll()
        {
            return await _context.Fastenings.ToListAsync();
        }

        public async Task<Fastening> GetById(int id)
        {
            return await _context.Fastenings.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Fastening> Create(Fastening Fastening)
        {
            _context.Fastenings.Add(Fastening);
            await _context.SaveChangesAsync();
            return Fastening;
        }

        public async Task<Fastening> Update(int id, Fastening newFastening)
        {
            var existingFastening = await _context.Fastenings.FirstOrDefaultAsync(a => a.Id == id);
            existingFastening.Name = newFastening.Name;
            existingFastening.Price = newFastening.Price;
            existingFastening.Material = newFastening.Material;
            existingFastening.Color = newFastening.Color;
            existingFastening.ColorCode = newFastening.ColorCode;
            existingFastening.IsActive = newFastening.IsActive;
            if(newFastening.ModelUrl != null)
            {
                existingFastening.ModelUrl = newFastening.ModelUrl;
            }
            await _context.SaveChangesAsync();
            return existingFastening;
        }

        public async Task<bool> Delete(int id)
        {
            var Fastening = await _context.Fastenings.FirstOrDefaultAsync(a => a.Id == id);
            _context.Fastenings.Remove(Fastening);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
