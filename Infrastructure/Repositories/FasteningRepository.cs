using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class FasteningRepository : IRepository<Fastening, Guid>
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

        public async Task<Fastening> GetById(Guid id)
        {
            return await _context.Fastenings.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Fastening not found");
        }

        public async Task<Fastening> Create(Fastening order)
        {
            _context.Fastenings.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Fastening> Update(Guid id, Fastening newFastening)
        {
            Fastening existingFastening = await _context.Fastenings.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Fastening not found");
            existingFastening.Name = newFastening.Name;
            existingFastening.Price = newFastening.Price;
            existingFastening.Material = newFastening.Material;
            existingFastening.Color = newFastening.Color;
            existingFastening.ColorCode = newFastening.ColorCode;
            existingFastening.IsActive = newFastening.IsActive;
            existingFastening.ModelUrl = newFastening.ModelUrl;
            
            await _context.SaveChangesAsync();
            return existingFastening;
        }

        public async Task<bool> Delete(Guid id)
        {
            Fastening fastening = await _context.Fastenings.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception("Fastening not found");
            _context.Fastenings.Remove(fastening);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
