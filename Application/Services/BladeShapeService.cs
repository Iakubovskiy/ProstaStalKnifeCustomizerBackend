using Application.Interfaces;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class BladeShapeService
    {
        private readonly IRepository<BladeShape, Guid> _bladeShapeRepository;
        private readonly IFileService _fileService;

        public BladeShapeService(IRepository<BladeShape, Guid> bladeShapeRepository, IFileService fileService)
        {
            _bladeShapeRepository = bladeShapeRepository;
            _fileService = fileService;
        }

        public async Task<List<BladeShape>> GetAllBladeShapes()
        {
            return await _bladeShapeRepository.GetAll();
        }

        public async Task<List<BladeShape>> GetAllActiveBladeShapes()
        {
            List<BladeShape> bladeShapes = await _bladeShapeRepository.GetAll();
            return bladeShapes.Where(c => c.IsActive).ToList();
        }

        public async Task<BladeShape> GetBladeShapeById(Guid id)
        {
            return await _bladeShapeRepository.GetById(id);
        }

        public async Task<BladeShape> ChangeActive(Guid id, bool active)
        {
            BladeShape bladeShape = await _bladeShapeRepository.GetById(id);
            if (bladeShape is null)
                throw new KeyNotFoundException("Blade shape not found");
            bladeShape.IsActive = active;
            return await _bladeShapeRepository.Update(id, bladeShape);
        }

        public async Task<BladeShape> CreateBladeShape(
            BladeShape bladeShape, 
            IFormFile bladeShapeModel, 
            IFormFile sheathModel,
            IFormFile bladeShapePhoto
            )
        {
            bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(bladeShapeModel, bladeShapeModel.FileName);
            bladeShape.sheathModelUrl = await _fileService.SaveFile(sheathModel, sheathModel.FileName);
            bladeShape.BladeShapePhotoUrl = await _fileService.SaveFile(bladeShapePhoto, bladeShapePhoto.FileName);
            return await _bladeShapeRepository.Create(bladeShape);
        }

        public async Task<BladeShape> UpdateBladeShape(
            Guid id, 
            BladeShape bladeShape,
            IFormFile? bladeShapeModel,
            IFormFile? sheathModel,
            IFormFile? bladeShapePhoto
            )
        {
            if (bladeShapeModel != null)
            {
                if (!string.IsNullOrEmpty(bladeShape.bladeShapeModelUrl))
                {
                    List<BladeShape> shapes = await _bladeShapeRepository.GetAll();
                    int quantity = shapes.Count(c => c.bladeShapeModelUrl == bladeShape.bladeShapeModelUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(bladeShape.bladeShapeModelUrl));
                    }
                }
                bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(bladeShapeModel, bladeShapeModel.FileName);
            }
            if (sheathModel != null)
            {
                if (!string.IsNullOrEmpty(bladeShape.bladeShapeModelUrl))
                {
                    List<BladeShape> shapes = await _bladeShapeRepository.GetAll();
                    int quantity = shapes.Count(c => c.sheathModelUrl == bladeShape.sheathModelUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(bladeShape.sheathModelUrl));
                    }
                }
                bladeShape.sheathModelUrl = await _fileService.SaveFile(sheathModel, sheathModel.FileName);
            }
            if (bladeShapePhoto != null)
            {
                if (!string.IsNullOrEmpty(bladeShape.BladeShapePhotoUrl))
                {
                    List<BladeShape> shapes = await _bladeShapeRepository.GetAll();
                    int quantity = shapes.Count(c => c.BladeShapePhotoUrl == bladeShape.BladeShapePhotoUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(bladeShape.BladeShapePhotoUrl));
                    }
                }
                bladeShape.BladeShapePhotoUrl = await _fileService.SaveFile(bladeShapePhoto, bladeShapePhoto.FileName);
            }
            return await _bladeShapeRepository.Update(id, bladeShape);
        }

        public async Task<bool> DeleteBladeShape(Guid id)
        {
            BladeShape bladeShape = await _bladeShapeRepository.GetById(id);
            string shapeFileId = _fileService.GetIdFromUrl(bladeShape.bladeShapeModelUrl);
            string sheathFileId = _fileService.GetIdFromUrl(bladeShape.sheathModelUrl);
            await _fileService.DeleteFile(shapeFileId);
            await _fileService.DeleteFile(sheathFileId);

            return await _bladeShapeRepository.Delete(id);
        }
    }
}
