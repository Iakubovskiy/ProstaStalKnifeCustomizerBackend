using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class KnifeService
    {
        private readonly IRepository<Knife, Guid> _knifeRepository;
        private readonly EngravingPriceService _engravingPriceService;

        public KnifeService(IRepository<Knife, Guid> knifeRepository, EngravingPriceService engravingPriceService)
        {
            _knifeRepository = knifeRepository;
            _engravingPriceService = engravingPriceService;
        }
        public async Task<List<Knife>> GetAllActiveKnives()
        {
            List<Knife> knives = await _knifeRepository.GetAll();
            return knives.Where(c => c.IsActive).ToList();
        }
        public async Task<List<Knife>> GetAllKnives()
        {
            return await _knifeRepository.GetAll();
        }

        public async Task<Knife> GetKnifeById(Guid id)
        {
            return await _knifeRepository.GetById(id);
        }

        public async Task<Knife> CreateKnife(Knife knife)
        {
            return await _knifeRepository.Create(knife);
        }

        public async Task<Knife> UpdateKnife(Guid id, Knife newKnife)
        {
            return await _knifeRepository.Update(id, newKnife);
        }

        public async Task<bool> DeleteKnife(Guid id)
        {
            return await _knifeRepository.Delete(id);
        }

        public async Task<double> KnifePrice(Guid id)
        {
            Knife knife = await _knifeRepository.GetById(id);
                        
            List<int>uniqueSides = new List<int>();
            if (knife.Engravings != null)
            {
                foreach (Engraving engraving in knife.Engravings)
                {
                    if(!uniqueSides.Contains(engraving.Side))
                    {
                        uniqueSides.Add(engraving.Side);
                    }
                }
            }
            List<EngravingPrice> prices = await _engravingPriceService.GetAllEngravingPrices();
            double oneSidePrice = prices[0].Price;
            double engravingPrice = uniqueSides.Count * oneSidePrice;
            double price = knife.Shape.Price + knife.BladeCoatingColor.Price + knife.SheathColor.Price + (knife.Fastening?.Price ?? 0) + engravingPrice;
            return price;
        }

        public async Task<Knife> ChangeActive(Guid id, bool active)
        {
            Knife knife = await _knifeRepository.GetById(id);
            knife.IsActive = active;
            return await _knifeRepository.Update(id, knife);
        }
    }
}
