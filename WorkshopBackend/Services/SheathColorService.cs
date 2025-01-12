using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using WorkshopBackend.Repositories;

namespace WorkshopBackend.Services
{
    public class SheathColorService
    {
        private readonly Repository<SheathColor, Guid> _sheathColorRepository;
        private readonly IFileService _fileService;

        public SheathColorService(Repository<SheathColor, Guid> sheathColorRepository, IFileService fileService)
        {
            _sheathColorRepository = sheathColorRepository;
            _fileService = fileService;
        }

        public async Task<List<SheathColor>> GetAllSheathColors()
        {
            return await _sheathColorRepository.GetAll();
        }

        public async Task<List<SheathColor>> GetAllActiveSheathColors()
        {
            List<SheathColor> sheathColors = await _sheathColorRepository.GetAll();
            return sheathColors.Where(c => c.IsActive).ToList();
        }

        public async Task<SheathColor> GetSheathColorById(Guid id)
        {
            return await _sheathColorRepository.GetById(id);
        }

        public async Task<SheathColor> CreateSheathColor(
            SheathColor sheathColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            if (colorMap != null)
            {
                sheathColor.ColorMapUrl = await _fileService.SaveFile(colorMap);
            }
            if (normalMap != null)
            {
                sheathColor.NormalMapUrl = await _fileService.SaveFile(normalMap);
            }
            if (roughnesMap != null)
            {
                sheathColor.RoughnessMapUrl = await _fileService.SaveFile(roughnesMap);
            }
            return await _sheathColorRepository.Create(sheathColor);
        }

        public async Task<SheathColor> UpdateSheathColor(
            Guid id, 
            SheathColor sheathColor, 
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            if (colorMap != null)
            {
                if (!string.IsNullOrEmpty(sheathColor.ColorMapUrl))
                {
                    List<SheathColor> colors = await _sheathColorRepository.GetAll();
                    int quantity = colors.Count(c => c.ColorMapUrl == sheathColor.ColorMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(sheathColor.ColorMapUrl));
                    }
                }
                sheathColor.ColorMapUrl = await _fileService.SaveFile(colorMap);
            }
            if (normalMap != null)
            {
                if (!string.IsNullOrEmpty(sheathColor.NormalMapUrl))
                {
                    List<SheathColor> colors = await _sheathColorRepository.GetAll();
                    int quantity = colors.Count(c => c.NormalMapUrl == sheathColor.NormalMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(sheathColor.NormalMapUrl));
                    }
                }
                sheathColor.NormalMapUrl = await _fileService.SaveFile(normalMap);
            }
            if (roughnesMap != null)
            {
                if (!string.IsNullOrEmpty(sheathColor.RoughnessMapUrl))
                {
                    List<SheathColor> colors = await _sheathColorRepository.GetAll();
                    int quantity = colors.Count(c => c.RoughnessMapUrl == sheathColor.RoughnessMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(sheathColor.RoughnessMapUrl));
                    }
                }
                sheathColor.RoughnessMapUrl = await _fileService.SaveFile(roughnesMap);
            }
            return await _sheathColorRepository.Update(id, sheathColor);
        }

        public async Task<bool> DeleteSheathColor(Guid id)
        {
            SheathColor color = await _sheathColorRepository.GetById(id);
            List<SheathColor> colors = await _sheathColorRepository.GetAll();
            int quantity = colors.Count(c => c.ColorMapUrl == color.ColorMapUrl);
            if (quantity <= 1)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.ColorMapUrl));
            }
            quantity = colors.Count(c => c.RoughnessMapUrl == color.RoughnessMapUrl);
            if (quantity <= 1)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.RoughnessMapUrl));
            }
            quantity = colors.Count(c => c.NormalMapUrl == color.NormalMapUrl);
            if (quantity <= 1)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.NormalMapUrl));
            }
            return await _sheathColorRepository.Delete(id);
        }
        public async Task<SheathColor> ChangeActive(Guid id, bool active)
        {
            SheathColor sheathColor = await _sheathColorRepository.GetById(id);
            sheathColor.IsActive = active;
            return await _sheathColorRepository.Update(id, sheathColor);
        }
    }
}
