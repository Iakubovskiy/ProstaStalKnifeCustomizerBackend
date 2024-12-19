using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class BladeCoatingService
    {
        private readonly Repository<BladeCoating, int> _bladeCoatingRepository;
        private readonly IFileService _fileService;
        
        public BladeCoatingService(Repository<BladeCoating, int> bladeCoatingRepository, IFileService fileService)
        {
            _bladeCoatingRepository = bladeCoatingRepository;
            _fileService = fileService;
        }

        public async Task<List<BladeCoating>> GetAllBladeCoatings()
        {
            return await _bladeCoatingRepository.GetAll();
        }

        public async Task<BladeCoating> GetBladeCoatingById(int id)
        {
            return await _bladeCoatingRepository.GetById(id);
        }

        public async Task<BladeCoating> CreateBladeCoating(BladeCoating bladeCoating, IFormFile file)
        {
            bladeCoating.MaterialUrl = await _fileService.SaveFile(file);
            return await _bladeCoatingRepository.Create(bladeCoating);
        }

        public async Task<BladeCoating> UpdateBladeCoating(int id, BladeCoating bladeCoating, IFormFile? file)
        {
            if (file != null)
            {
                bladeCoating.MaterialUrl = await _fileService.SaveFile(file);
            }
            return await _bladeCoatingRepository.Update(id, bladeCoating);
        }

        public async Task<bool> DeleteBladeCoating(int id)
        {
            BladeCoating existingBladeCoating = await _bladeCoatingRepository.GetById(id);
            string publicFileId = _fileService.GetIdFromUrl(existingBladeCoating.MaterialUrl);
            await _fileService.DeleteFile(publicFileId);
            return await _bladeCoatingRepository.Delete(id);
        }
    }
}
