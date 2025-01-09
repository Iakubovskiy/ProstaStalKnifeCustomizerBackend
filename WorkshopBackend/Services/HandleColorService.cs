using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using WorkshopBackend.Repositories;

namespace WorkshopBackend.Services
{
    public class HandleColorService
    {
        private readonly Repository<HandleColor, int> _handleColorRepository;
        private readonly IFileService _fileService;

        public HandleColorService(Repository<HandleColor, int> handleColorRepository, IFileService fileService)
        {
            _handleColorRepository = handleColorRepository;
            _fileService = fileService;
        }

        public async Task<List<HandleColor>> GetAllHandleColors()
        {
            return await _handleColorRepository.GetAll();
        }

        public async Task<HandleColor> GetHandleColorById(int id)
        {
            return await _handleColorRepository.GetById(id);
        }

        public async Task<HandleColor> CreateHandleColor(
            HandleColor handleColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            if (colorMap != null)
            {
                if (!string.IsNullOrEmpty(handleColor.ColorMapUrl))
                {
                    List<HandleColor> colors = await _handleColorRepository.GetAll();
                    int quantity = colors.Count(c => c.ColorMapUrl == handleColor.ColorMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(handleColor.ColorMapUrl));
                    }
                }
                handleColor.ColorMapUrl = await _fileService.SaveFile(colorMap);
            }
            if (normalMap != null)
            {
                if (!string.IsNullOrEmpty(handleColor.NormalMapUrl))
                {
                    List<HandleColor> colors = await _handleColorRepository.GetAll();
                    int quantity = colors.Count(c => c.NormalMapUrl == handleColor.NormalMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(handleColor.NormalMapUrl));
                    }
                }
                handleColor.NormalMapUrl = await _fileService.SaveFile(normalMap);
            }
            if (roughnesMap != null)
            {
                if (!string.IsNullOrEmpty(handleColor.RoughnessMapUrl))
                {
                    List<HandleColor> colors = await _handleColorRepository.GetAll();
                    int quantity = colors.Count(c => c.RoughnessMapUrl == handleColor.RoughnessMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(handleColor.RoughnessMapUrl));
                    }
                }
                handleColor.RoughnessMapUrl = await _fileService.SaveFile(roughnesMap);
            }
            return await _handleColorRepository.Create(handleColor);
        }

        public async Task<HandleColor> UpdateHandleColor(
            int id, 
            HandleColor handleColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnesMap
            )
        {
            if (colorMap != null)
            {
                handleColor.ColorMapUrl = await _fileService.SaveFile(colorMap);
            }
            if (normalMap != null)
            {
                handleColor.NormalMapUrl = await _fileService.SaveFile(normalMap);
            }
            if (roughnesMap != null)
            {
                handleColor.RoughnessMapUrl = await _fileService.SaveFile(roughnesMap);
            }
            return await _handleColorRepository.Update(id, handleColor);
        }

        public async Task<bool> DeleteHandleColor(int id)
        {
            HandleColor color = await _handleColorRepository.GetById(id);
            List<HandleColor> colors = await _handleColorRepository.GetAll();            
            int quantity = colors.Count(c => c.ColorMapUrl == color.ColorMapUrl);
            if (quantity == 1)
            {                
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.ColorMapUrl));
            }
            quantity = colors.Count(c => c.RoughnessMapUrl == color.RoughnessMapUrl);
            if (quantity == 1)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.RoughnessMapUrl));
            }
            quantity = colors.Count(c => c.NormalMapUrl == color.NormalMapUrl);
            if (quantity == 1)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.NormalMapUrl));
            }
            return await _handleColorRepository.Delete(id);
        }

        public async Task<HandleColor> ChangeActive(int id, bool active)
        {
            HandleColor handleColor = await _handleColorRepository.GetById(id);
            handleColor.IsActive = active;
            return await _handleColorRepository.Update(id, handleColor);
        }
    }
}
