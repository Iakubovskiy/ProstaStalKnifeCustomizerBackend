﻿using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class EngravingPriceService
    {
        private readonly Repository<EngravingPrice, Guid> _engravingPriceRepository;

        public EngravingPriceService(Repository<EngravingPrice, Guid> engravingPriceRepository)
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
