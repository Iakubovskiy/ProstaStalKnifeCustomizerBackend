using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Data.DbContext;

namespace Infrastructure.Repositories
{
    public class KnifeRepository: IRepository<Knife, Guid>
    {
        private readonly DbContext _context;
        
        public KnifeRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Knife>> GetAll()
        {
            return await _context.Knives.ToListAsync();
        }
        public async Task<Knife> GetById(Guid id)
        {
            return await _context.Knives
                .Include(k => k.Shape)
                .Include(k => k.BladeCoatingColor)
                .Include(k => k.HandleColor)
                .Include(k => k.SheathColor)
                .Include(k => k.Fastening)
                .Include(k => k.Engravings)
                .FirstOrDefaultAsync(k => k.Id == id) ?? throw new Exception($"Knife not found by id: {id}");
        }
        public async Task<Knife> Create (Knife order)
        {
            if (_context.Entry(order.Shape).State == EntityState.Detached)
                _context.Attach(order.Shape);
            if (_context.Entry(order.BladeCoatingColor).State == EntityState.Detached)
                _context.Attach(order.BladeCoatingColor);
            if (_context.Entry(order.HandleColor).State == EntityState.Detached)
                _context.Attach(order.HandleColor);
            if (_context.Entry(order.SheathColor).State == EntityState.Detached)
                _context.Attach(order.SheathColor);
            if (order.Fastening != null && _context.Entry(order.Fastening).State == EntityState.Detached)
                _context.Attach(order.Fastening);
            if (order.Engravings != null)
            {
                foreach (var engraving in order.Engravings)
                {
                    if (_context.Entry(engraving).State == EntityState.Detached)
                        _context.Attach(engraving);
                }
            }
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Knife> Update (Guid id, Knife newKnife)
        {
            Knife existingKnife = await _context.Knives
                .Include(k => k.Shape)
                .Include(k => k.BladeCoatingColor)
                .Include(k => k.HandleColor)
                .Include(k => k.SheathColor)
                .Include(k => k.Fastening)
                .Include(k => k.Engravings)
                .FirstOrDefaultAsync(k => k.Id == id) 
                                  ?? throw new KeyNotFoundException($"Knife with ID {id} not found.");

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
                if(existingKnife.Engravings != null)
                    existingKnife.Engravings.Clear();
                else
                    existingKnife.Engravings = new List<Engraving>();
                foreach (var engraving in newKnife.Engravings)
                {
                    if (_context.Entry(engraving).State == EntityState.Detached)
                        _context.Attach(engraving);
                    existingKnife.Engravings.Add(engraving);
                }
            }

            existingKnife.IsActive = newKnife.IsActive;
            await _context.SaveChangesAsync();
            
            return newKnife;
        }
        public async Task<bool> Delete(Guid id)
        {
            Knife knife = await _context.Knives.FirstOrDefaultAsync(k => k.Id == id)
                ?? throw new Exception($"knife not found by id: {id}");
            _context.Knives.Remove(knife);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
