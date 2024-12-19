using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;
using WorkshopBackend.Repositories;

namespace WorkshopBackend.Services
{
    public class FasteningService
    {
        private readonly Repository<Fastening, int> _fasteningRepository;
        private readonly IFileService _fileService;

        public FasteningService(Repository<Fastening, int> fasteningRepository, IFileService fileService)
        {
            _fasteningRepository = fasteningRepository;
            _fileService = fileService;
        }

        public async Task<List<Fastening>> GetAllFastenings()
        {
            return await _fasteningRepository.GetAll();
        }

        public async Task<Fastening> GetFasteningById(int id)
        {
            return await _fasteningRepository.GetById(id);
        }

        public async Task<Fastening> CreateFastening(Fastening fastening, IFormFile model)
        {
            fastening.ModelUrl = await _fileService.SaveFile(model);
            return await _fasteningRepository.Create(fastening);
        }

        public async Task<Fastening> UpdateFastening(int id, Fastening fastening, IFormFile? model)
        {
            if (model != null) 
            {
                fastening.ModelUrl = await _fileService.SaveFile(model);
            }
            return await _fasteningRepository.Update(id, fastening);
        }

        public async Task<bool> DeleteFastening(int id)
        {
            Fastening existingFastening = await _fasteningRepository.GetById(id);
            string publicFileId = _fileService.GetIdFromUrl(existingFastening.ModelUrl);
            await _fileService.DeleteFile(publicFileId);
            return await _fasteningRepository.Delete(id);
        }
    }
}
