using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class EngravingService
    {
        private readonly Repository<Engraving, int> _engravingRepository;
        private readonly IFileService _fileService;

        public EngravingService(Repository<Engraving, int> engravingRepository, IFileService fileService)
        {
            _engravingRepository = engravingRepository;
            _fileService = fileService;
        }

        public async Task<List<Engraving>> GetAllEngravings()
        {
            return await _engravingRepository.GetAll();
        }

        public async Task<Engraving> GetEngravingById(int id)
        {
            return await _engravingRepository.GetById(id);
        }

        public async Task<Engraving> CreateEngraving(Engraving engraving, IFormFile? file)
        {
            if (file != null)
            {
                engraving.pictureUrl = await _fileService.SaveFile(file);
            }
            return await _engravingRepository.Create(engraving);
        }

        public async Task<Engraving> UpdateEngraving(int id, Engraving engraving, IFormFile? file)
        {
            if (file != null)
            {
                engraving.pictureUrl = await _fileService.SaveFile(file);
            }
            return await _engravingRepository.Update(id, engraving);
        }

        public async Task<bool> DeleteEngraving(int id)
        {
            Engraving engraving = await _engravingRepository.GetById(id);
            if (!string.IsNullOrEmpty(engraving.pictureUrl))
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(engraving.pictureUrl));
            }
            return await _engravingRepository.Delete(id);
        }
    }
}
