using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using WorkshopBackend.Repositories;

namespace WorkshopBackend.Services
{
    public class SheathColorService
    {
        private readonly Repository<SheathColor, int> _sheathColorRepository;
        private readonly IFileService _fileService;

        public SheathColorService(Repository<SheathColor, int> sheathColorRepository, IFileService fileService)
        {
            _sheathColorRepository = sheathColorRepository;
            _fileService = fileService;
        }

        public async Task<List<SheathColor>> GetAllSheathColors()
        {
            return await _sheathColorRepository.GetAll();
        }

        public async Task<SheathColor> GetSheathColorById(int id)
        {
            return await _sheathColorRepository.GetById(id);
        }

        public async Task<SheathColor> CreateSheathColor(SheathColor sheathColor, IFormFile material)
        {
            sheathColor.MaterialUrl = await _fileService.SaveFile(material);
            return await _sheathColorRepository.Create(sheathColor);
        }

        public async Task<SheathColor> UpdateSheathColor(int id, SheathColor sheathColor, IFormFile? material)
        {
            if(material != null)
            {
                sheathColor.MaterialUrl = await _fileService.SaveFile(material);
            }
            return await _sheathColorRepository.Update(id, sheathColor);
        }

        public async Task<bool> DeleteSheathColor(int id)
        {
            SheathColor color = await _sheathColorRepository.GetById(id);
            List<SheathColor> colors = await _sheathColorRepository.GetAll();
            int materialColorsQuantity = colors.Count(c => c.Material == color.Material);
            if (materialColorsQuantity == 1 && color.MaterialUrl != null)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.MaterialUrl));
            }
            return await _sheathColorRepository.Delete(id);
        }
    }
}
