using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class FasteningService
    {
        private readonly IRepository<Fastening, Guid> _fasteningRepository;
        private readonly IFileService _fileService;

        public FasteningService(IRepository<Fastening, Guid> fasteningRepository, IFileService fileService)
        {
            _fasteningRepository = fasteningRepository;
            _fileService = fileService;
        }

        public async Task<List<Fastening>> GetAllFastenings()
        {
            return await _fasteningRepository.GetAll();
        }

        public async Task<List<Fastening>> GetAllActiveFastenings()
        {
            List<Fastening> fastenings = await _fasteningRepository.GetAll();
            return fastenings.Where(c => c.IsActive).ToList();
        }

        public async Task<Fastening?> GetFasteningById(Guid id)
        {
            return await _fasteningRepository.GetById(id);
        }

        public async Task<Fastening> CreateFastening(Fastening fastening, IFormFile model)
        {
            fastening.ModelUrl = await _fileService.SaveFile(model, model.FileName);
            return await _fasteningRepository.Create(fastening);
        }

        public async Task<Fastening> UpdateFastening(Guid id, Fastening fastening, IFormFile? model)
        {
            if (model != null) 
            {
                if (!string.IsNullOrEmpty(fastening.ModelUrl))
                {
                    List<Fastening> fastenings = await _fasteningRepository.GetAll();
                    int quantity = fastenings.Count(c => c.ModelUrl == fastening.ModelUrl);
                    if (quantity <= 1)
                    {
                        await _fileService.DeleteFile(_fileService.GetIdFromUrl(fastening.ModelUrl));
                    }
                }
                fastening.ModelUrl = await _fileService.SaveFile(model, model.FileName);
            }
            return await _fasteningRepository.Update(id, fastening);
        }

        public async Task<bool> DeleteFastening(Guid id)
        {
            Fastening existingFastening = await _fasteningRepository.GetById(id);
            string publicFileId = _fileService.GetIdFromUrl(existingFastening.ModelUrl);
            await _fileService.DeleteFile(publicFileId);
            return await _fasteningRepository.Delete(id);
        }

        public async Task<Fastening> ChangeActive(Guid id, bool active)
        {
            Fastening fastening = await _fasteningRepository.GetById(id);
            fastening.IsActive = active;
            return await _fasteningRepository.Update(id, fastening);
        }
    }
}
