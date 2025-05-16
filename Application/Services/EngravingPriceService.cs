using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class EngravingPriceService
    {
        private readonly IRepository<EngravingPrice, Guid> _engravingPriceRepository;

        public EngravingPriceService(IRepository<EngravingPrice, Guid> engravingPriceRepository)
        {
            _engravingPriceRepository = engravingPriceRepository;
        }

        public async Task<List<EngravingPrice>> GetAllEngravingPrices()
        {
            return await _engravingPriceRepository.GetAll();
        }

        public async Task<EngravingPrice> GetEngravingPriceById(Guid id)
        {
            return await _engravingPriceRepository.GetById(id);
        }

        public async Task<EngravingPrice> CreateEngravingPrice(EngravingPrice engravingPrice)
        {
            return await _engravingPriceRepository.Create(engravingPrice);
        }

        public async Task<EngravingPrice> UpdateEngravingPrice(Guid id, EngravingPrice engravingPrice)
        {
            return await _engravingPriceRepository.Update(id, engravingPrice);
        }

        public async Task<bool> DeleteEngravingPrice(Guid id)
        {
            return await _engravingPriceRepository.Delete(id);
        }
    }
}
