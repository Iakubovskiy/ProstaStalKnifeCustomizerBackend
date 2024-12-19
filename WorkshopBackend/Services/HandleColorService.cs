using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

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

        public async Task<HandleColor> CreateHandleColor(HandleColor handleColor, IFormFile material)
        {
            handleColor.MaterialUrl = await _fileService.SaveFile(material);
            return await _handleColorRepository.Create(handleColor);
        }

        public async Task<HandleColor> UpdateHandleColor(int id, HandleColor handleColor, IFormFile? material)
        {
            if(material != null)
            {
                handleColor.MaterialUrl = await _fileService.SaveFile(material);
            }
            return await _handleColorRepository.Update(id, handleColor);
        }

        public async Task<bool> DeleteHandleColor(int id)
        {
            HandleColor color = await _handleColorRepository.GetById(id);
            List<HandleColor> colors = await _handleColorRepository.GetAll();            
            int materialColorsQuantity = colors.Count(c => c.Material == color.Material);
            if (materialColorsQuantity == 1)
            {                
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.MaterialUrl));
            }
            return await _handleColorRepository.Delete(id);
        }
    }
}
