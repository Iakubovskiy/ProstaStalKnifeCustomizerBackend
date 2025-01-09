using WorkshopBackend.Data;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkshopBackend.Repositories
{
    public class KnifeRepository: Repository<Knife, int>
    {
        private readonly DBContext _context;
        
        public KnifeRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Knife>> GetAll()
        {
            return await _context.Knives.ToListAsync();
        }
        public async Task<Knife> GetById(int id)
        {
            return await _context.Knives
                .Include(k => k.Shape)
                .Include(k => k.BladeCoatingColor)
                .Include(k => k.HandleColor)
                .Include(k => k.SheathColor)
                .Include(k => k.Fastening)
                .Include(k => k.Engravings)
                .FirstOrDefaultAsync(k => k.Id == id);
        }
        public async Task<Knife> Create (Knife knife)
        {
            if (_context.Entry(knife.Shape).State == EntityState.Detached)
                _context.Attach(knife.Shape);
            if (_context.Entry(knife.BladeCoatingColor).State == EntityState.Detached)
                _context.Attach(knife.BladeCoatingColor);
            if (_context.Entry(knife.HandleColor).State == EntityState.Detached)
                _context.Attach(knife.HandleColor);
            if (_context.Entry(knife.SheathColor).State == EntityState.Detached)
                _context.Attach(knife.SheathColor);
            if (_context.Entry(knife.Fastening).State == EntityState.Detached)
                _context.Attach(knife.Fastening);
            if (knife.Engravings != null)
            {
                foreach (var engraving in knife.Engravings)
                {
                    if (_context.Entry(engraving).State == EntityState.Detached)
                        _context.Attach(engraving);
                }
            }
            _context.Add(knife);
            await _context.SaveChangesAsync();
            return knife;
        }
        public async Task<Knife> Update (int id, Knife newKnife)
        {
            var existingKnife = await _context.Knives
                .Include(k => k.Shape)
                .Include(k => k.BladeCoatingColor)
                .Include(k => k.HandleColor)
                .Include(k => k.SheathColor)
                .Include(k => k.Fastening)
                .Include(k => k.Engravings)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (existingKnife == null)
                throw new KeyNotFoundException($"Knife with ID {id} not found.");

            if (_context.Entry(newKnife.Shape).State == EntityState.Detached)
                _context.Attach(newKnife.Shape);
            if (_context.Entry(newKnife.BladeCoatingColor).State == EntityState.Detached)
                _context.Attach(newKnife.BladeCoatingColor);
            if (_context.Entry(newKnife.HandleColor).State == EntityState.Detached)
                _context.Attach(newKnife.HandleColor);
            if (_context.Entry(newKnife.SheathColor).State == EntityState.Detached)
                _context.Attach(newKnife.SheathColor);
            if (newKnife.Engravings != null)
            {
                existingKnife.Engravings.Clear();
                foreach (var engraving in newKnife.Engravings)
                {
                    if (_context.Entry(engraving).State == EntityState.Detached)
                        _context.Attach(engraving);
                    existingKnife.Engravings.Add(engraving);
                }
            }

            _context.Entry(existingKnife).CurrentValues.SetValues(newKnife);
            await _context.SaveChangesAsync();
            
            return newKnife;
        }
        public async Task<bool> Delete(int id)
        {
            var knife = await _context.Knives.FirstOrDefaultAsync(k => k.Id == id);
            _context.Knives.Remove(knife);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
