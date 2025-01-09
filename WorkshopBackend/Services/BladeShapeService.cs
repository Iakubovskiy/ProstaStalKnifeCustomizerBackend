﻿using Microsoft.AspNetCore.Http.Metadata;
using System.Drawing;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using WorkshopBackend.Repositories;

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

        public async Task<BladeShape> ChangeActive(int id, bool active)
        {
            BladeShape bladeShape = await _bladeShapeRepository.GetById(id);
            bladeShape.IsActive = active;
            return await _bladeShapeRepository.Update(id, bladeShape);
        }

        public async Task<BladeShape> CreateBladeShape(
            BladeShape bladeShape, 
            IFormFile bladeShapeModel, 
            IFormFile sheathModel
            )
        {
            bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(bladeShapeModel);
            bladeShape.sheathModelUrl = await _fileService.SaveFile(sheathModel);
            return await _bladeShapeRepository.Create(bladeShape);
        }

        public async Task<BladeShape> UpdateBladeShape(
            int id, 
            BladeShape bladeShape,
            IFormFile? bladeShapeModel,
            IFormFile? sheathModel
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
                bladeShape.bladeShapeModelUrl = await _fileService.SaveFile(bladeShapeModel);
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
                bladeShape.sheathModelUrl = await _fileService.SaveFile(sheathModel);
            }
            return await _bladeShapeRepository.Update(id, bladeShape);
        }

        public async Task<bool> DeleteBladeShape(int id)
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
