using Microsoft.AspNetCore.Http.Metadata;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class BladeShapeService
    {
        private readonly Repository<BladeShape, int> _bladeShapeRepository;
        private readonly IFileService _fileService;

        public BladeShapeService(Repository<BladeShape, int> bladeShapeRepository, IFileService fileService)
        {
            _bladeShapeRepository = bladeShapeRepository;
            _fileService = fileService;
        }

        public async Task<List<BladeShape>> GetAllBladeShapes()
        {
            return await _bladeShapeRepository.GetAll();
        }

        public async Task<BladeShape> GetBladeShapeById(int id)
        {
            return await _bladeShapeRepository.GetById(id);
        }

        public async Task<BladeShape> CreateBladeShape(
            BladeShape bladeShape, 
            IFormFile bladeShapeModel, 
            IFormFile handleModel,
            IFormFile sheathModel
            )
        {
            bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(bladeShapeModel);
            bladeShape.handleShapeModelUrl = await _fileService.SaveFile(handleModel);
            bladeShape.sheathModelUrl = await _fileService.SaveFile(sheathModel);
            return await _bladeShapeRepository.Create(bladeShape);
        }

        public async Task<BladeShape> UpdateBladeShape(
            int id, 
            BladeShape bladeShape,
            IFormFile? bladeShapeModel,
            IFormFile? handleModel,
            IFormFile? sheathModel
            )
        {
            if (bladeShapeModel != null)
            {
                bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(bladeShapeModel);
            }
            if (handleModel != null)
            {
                bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(handleModel);
            }
            if (sheathModel != null)
            {
                bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(sheathModel);
            }
            return await _bladeShapeRepository.Update(id, bladeShape);
        }

        public async Task<bool> DeleteBladeShape(int id)
        {
            BladeShape bladeShape = await _bladeShapeRepository.GetById(id);
            string shapeFileId = _fileService.GetIdFromUrl(bladeShape.bladeShapeModelUrl);
            string handleFileId = _fileService.GetIdFromUrl(bladeShape.handleShapeModelUrl);
            string sheathFileId = _fileService.GetIdFromUrl(bladeShape.sheathModelUrl);
            await _fileService.DeleteFile(shapeFileId);
            await _fileService.DeleteFile(handleFileId);
            await _fileService.DeleteFile(sheathFileId);

            return await _bladeShapeRepository.Delete(id);
        }
    }
}
