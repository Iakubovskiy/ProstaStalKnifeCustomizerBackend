using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class DeliveryTypeService
    {
        private readonly Repository<DeliveryType, int> _deliveryTypeRepository;

        public DeliveryTypeService(Repository<DeliveryType, int> deliveryTypeRepository)
        {
            _deliveryTypeRepository = deliveryTypeRepository;
        }

        public async Task<List<DeliveryType>> GetAllDeliveryTypes()
        {
            return await _deliveryTypeRepository.GetAll();
        }

        public async Task<DeliveryType> GetDeliveryTypeById(int id)
        {
            return await _deliveryTypeRepository.GetById(id);
        }

        public async Task<DeliveryType> CreateDeliveryType(DeliveryType deliveryType)
        {
            return await _deliveryTypeRepository.Create(deliveryType);
        }

        public async Task<DeliveryType> UpdateDeliveryType(int id, DeliveryType deliveryType)
        {
            return await _deliveryTypeRepository.Update(id, deliveryType);
        }

        public async Task<bool> DeleteDeliveryType(int id)
        {
            return await _deliveryTypeRepository.Delete(id);
        }
    }
}
