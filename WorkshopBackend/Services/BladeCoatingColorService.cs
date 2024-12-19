using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class BladeCoatingColorService
    {
        private readonly Repository<BladeCoatingColor, int> _bladeCoatingColorRepository;

        public BladeCoatingColorService(Repository<BladeCoatingColor, int> bladeCoatingRepository)
        {
            _bladeCoatingColorRepository = bladeCoatingRepository;
        }

        public async Task<List<BladeCoatingColor>> GetAllBladeCoatingColors()
        {
            return await _bladeCoatingColorRepository.GetAll();
        }

        public async Task<BladeCoatingColor> GetBladeCoatingColorById(int id)
        {
            return await _bladeCoatingColorRepository.GetById(id);
        }

        public async Task<BladeCoatingColor> CreateBladeCoatingColor(BladeCoatingColor bladeCoatingColor)
        {
            return await _bladeCoatingColorRepository.Create(bladeCoatingColor);
        }

        public async Task<BladeCoatingColor> UpdateBladeCoatingColor(int id, BladeCoatingColor bladeCoatingColor)
        {
            return await _bladeCoatingColorRepository.Update(id, bladeCoatingColor);
        }

        public async Task<bool> DeleteBladeCoatingColor(int id)
        {
            return await _bladeCoatingColorRepository.Delete(id);
        }
    }
}
