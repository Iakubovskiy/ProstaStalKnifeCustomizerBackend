using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class KnifeService
    {
        public Repository<Knife, int> _knifeRepository;
        public EngravingPriceService _engravingPriceService;

        public KnifeService(Repository<Knife, int> knifeRepository, EngravingPriceService engravingPriceService)
        {
            _knifeRepository = knifeRepository;
            _engravingPriceService = engravingPriceService;
        }

        public async Task<List<Knife>> GetAllKnives()
        {
            return await _knifeRepository.GetAll();
        }

        public async Task<Knife> GetKnifeById(int id)
        {
            return await _knifeRepository.GetById(id);
        }

        public async Task<Knife> CreateKnife(Knife knife)
        {
            return await _knifeRepository.Create(knife);
        }

        public async Task<Knife> UpdateKnife(int id, Knife newKnife)
        {
            return await _knifeRepository.Update(id, newKnife);
        }

        public async Task<bool> DeleteKnife(int id)
        {
            return await _knifeRepository.Delete(id);
        }

        public async Task<double> KnifePrice(int id)
        {
            Knife knife = await _knifeRepository.GetById(id);
            double fasteningPrice = 0;
            if(knife.Fastening != null)
            {
                foreach (Fastening fastening in knife.Fastening)
                {
                    fasteningPrice += fastening.price;
                }
            }
            
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
            double price = knife.Shape.Price + knife.BladeCoating.Price + knife.SheathColor.Price + fasteningPrice + engravingPrice;
            return price;
        }
    }
}
