using Microsoft.EntityFrameworkCore;
using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Repositories
{
    public class BladeCoatingRepository : Repository<BladeCoating, int>
    {
        private readonly DBContext _context;
        private readonly Repository<BladeCoatingColor, int> _colorRepository;
        public BladeCoatingRepository(DBContext context, Repository<BladeCoatingColor, int> repository)
        {
            _context = context;
            _colorRepository = repository;
        }
        public async Task<List<BladeCoating>> GetAll()
        {
            return await _context.BladeCoatings.ToListAsync();
        }

        public async Task<BladeCoating> GetById(int id)
        {
            return await _context.BladeCoatings.Include(bc=>bc.Colors).FirstOrDefaultAsync(bc => bc.Id == id);
        }

        public async Task<BladeCoating> Create(BladeCoating BladeCoating)
        {
            foreach (BladeCoatingColor color in BladeCoating.Colors)
            {
                    await _colorRepository.Create(color);
            }
            _context.BladeCoatings.Add(BladeCoating);
            await _context.SaveChangesAsync();
            return BladeCoating;
        }

        public async Task<BladeCoating> Update(int id, BladeCoating newBladeCoating)
        {
            var existingBladeCoating = await _context.BladeCoatings.Include(b => b.Colors).FirstOrDefaultAsync(a => a.Id == id);            
            List<BladeCoatingColor> bladeCoatingColors = new List<BladeCoatingColor>(existingBladeCoating.Colors);
            List<BladeCoatingColor> newBladeCoatingColors = new List<BladeCoatingColor>(newBladeCoating.Colors);

            foreach (BladeCoatingColor color in newBladeCoatingColors)
            {
                if (bladeCoatingColors.Any(c => c.ColorCode == color.ColorCode))
                {
                    bladeCoatingColors.Remove(bladeCoatingColors.First(c => c.ColorCode == color.ColorCode));
                }
                else
                {
                    await _colorRepository.Create(color);
                    existingBladeCoating.Colors.Add(color);
                }
            }
            foreach (BladeCoatingColor color in bladeCoatingColors)
            {
                existingBladeCoating.Colors.Remove(color);
                await _colorRepository.Delete(color.Id);
            }
            if(newBladeCoating.MaterialUrl != null)
            {
                existingBladeCoating.MaterialUrl = newBladeCoating.MaterialUrl;
            }
            existingBladeCoating.Name = newBladeCoating.Name;
            existingBladeCoating.Price = newBladeCoating.Price;
            await _context.SaveChangesAsync();
            return existingBladeCoating;
        }

        public async Task<bool> Delete(int id)
        {
            var BladeCoating = await _context.BladeCoatings.FirstOrDefaultAsync(a => a.Id == id);
            _context.BladeCoatings.Remove(BladeCoating);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
