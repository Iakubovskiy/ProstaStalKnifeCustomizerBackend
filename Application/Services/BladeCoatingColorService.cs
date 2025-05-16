using Application.Interfaces;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class BladeCoatingColorService
    {
        private readonly IRepository<BladeCoatingColor, Guid> _bladeCoatingColorRepository;
        private readonly IFileService _fileService;

        public BladeCoatingColorService(IRepository<BladeCoatingColor, Guid> bladeCoatingRepository, IFileService fileService)
        {
            _bladeCoatingColorRepository = bladeCoatingRepository;
            _fileService = fileService;
        }

        public async Task<List<BladeCoatingColor>> GetAllBladeCoatingColors()
        {
            return await _bladeCoatingColorRepository.GetAll();
        }

        public async Task<List<BladeCoatingColor>> GetAllActiveBladeCoatingColors()
        {
            List<BladeCoatingColor> bladeCoatingColors = await _bladeCoatingColorRepository.GetAll();
            return bladeCoatingColors.Where(c => c.IsActive).ToList();
        }

        public async Task<BladeCoatingColor> GetBladeCoatingColorById(Guid id)
        {
            return await _bladeCoatingColorRepository.GetById(id);
        }

        public async Task<BladeCoatingColor> CreateBladeCoatingColor(
            BladeCoatingColor bladeCoatingColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnessMap
            )
        {
            if (colorMap != null)
            {
                bladeCoatingColor.ColorMapUrl = await _fileService.SaveFile(colorMap, colorMap.FileName);
            }
            if (normalMap != null)
            {
                bladeCoatingColor.NormalMapUrl = await _fileService.SaveFile(normalMap, normalMap.FileName);
            }
            if (roughnessMap != null)
            {
                bladeCoatingColor.RoughnessMapUrl = await _fileService.SaveFile(roughnessMap, roughnessMap.FileName);
            }
            return await _bladeCoatingColorRepository.Create(bladeCoatingColor);
        }

        public async Task<BladeCoatingColor> UpdateBladeCoatingColor(
            Guid id, 
            BladeCoatingColor bladeCoatingColor,
            IFormFile? colorMap,
            IFormFile? normalMap,
            IFormFile? roughnessMap
            )
        {
            if (colorMap != null)
            {
                if (!string.IsNullOrEmpty(bladeCoatingColor.ColorMapUrl))
                {
                    List<BladeCoatingColor> colors = await _bladeCoatingColorRepository.GetAll();
                    int quantity = colors.Count(c => c.ColorMapUrl == bladeCoatingColor.ColorMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(bladeCoatingColor.ColorMapUrl));
                    }
                }
                bladeCoatingColor.ColorMapUrl = await _fileService.SaveFile(colorMap, colorMap.FileName);
            }
            if (normalMap != null)
            {
                if (!string.IsNullOrEmpty(bladeCoatingColor.NormalMapUrl))
                {
                    List<BladeCoatingColor> colors = await _bladeCoatingColorRepository.GetAll();
                    int quantity = colors.Count(c => c.NormalMapUrl == bladeCoatingColor.NormalMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(bladeCoatingColor.NormalMapUrl));
                    }
                }
                bladeCoatingColor.NormalMapUrl = await _fileService.SaveFile(normalMap, normalMap.FileName);
            }
            if (roughnessMap != null)
            {
                if (!string.IsNullOrEmpty(bladeCoatingColor.RoughnessMapUrl))
                {
                    List<BladeCoatingColor> colors = await _bladeCoatingColorRepository.GetAll();
                    int quantity = colors.Count(c => c.RoughnessMapUrl == bladeCoatingColor.RoughnessMapUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(bladeCoatingColor.RoughnessMapUrl));
                    }
                }
                bladeCoatingColor.RoughnessMapUrl = await _fileService.SaveFile(roughnessMap, roughnessMap.FileName);
            }
            return await _bladeCoatingColorRepository.Update(id, bladeCoatingColor);
        }

        public async Task<bool> DeleteBladeCoatingColor(Guid id)
        {
            BladeCoatingColor color = await _bladeCoatingColorRepository.GetById(id);
            List<BladeCoatingColor> colors = await _bladeCoatingColorRepository.GetAll();
            int quantity = colors.Count(c => c.ColorMapUrl == color.ColorMapUrl);
            if (quantity <= 1 && color.ColorMapUrl != null)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.ColorMapUrl));
            }
            quantity = colors.Count(c => c.RoughnessMapUrl == color.RoughnessMapUrl);
            if (quantity <= 1 && color.RoughnessMapUrl != null)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.RoughnessMapUrl));
            }
            quantity = colors.Count(c => c.NormalMapUrl == color.NormalMapUrl);
            if (quantity <= 1 && color.NormalMapUrl != null)
            {
                await _fileService.DeleteFile(_fileService.GetIdFromUrl(color.NormalMapUrl));
            }
            return await _bladeCoatingColorRepository.Delete(id);
        }

        public async Task<BladeCoatingColor> ChangeActive(Guid id, bool active)
        {
            BladeCoatingColor bladeCoatingColor = await _bladeCoatingColorRepository.GetById(id);
            bladeCoatingColor.IsActive = active;
            return await _bladeCoatingColorRepository.Update(id, bladeCoatingColor);
        }
    }
}
