using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class DeliveryTypeService
    {
        private readonly IRepository<DeliveryType, Guid> _deliveryTypeRepository;

        public DeliveryTypeService(IRepository<DeliveryType, Guid> deliveryTypeRepository)
        {
            _deliveryTypeRepository = deliveryTypeRepository;
        }

        public async Task<List<DeliveryType>> GetAllDeliveryTypes()
        {
            return await _deliveryTypeRepository.GetAll();
        }

        public async Task<List<DeliveryType>> GetAllActiveDeliveryTypes()
        {
            List<DeliveryType> deliveryTypes = await _deliveryTypeRepository.GetAll();
            return deliveryTypes.Where(c => c.IsActive).ToList();
        }

        public async Task<DeliveryType> GetDeliveryTypeById(Guid id)
        {
            return await _deliveryTypeRepository.GetById(id);
        }

        public async Task<DeliveryType> CreateDeliveryType(DeliveryType deliveryType)
        {
            return await _deliveryTypeRepository.Create(deliveryType);
        }

        public async Task<DeliveryType> UpdateDeliveryType(Guid id, DeliveryType deliveryType)
        {
            return await _deliveryTypeRepository.Update(id, deliveryType);
        }

        public async Task<bool> DeleteDeliveryType(Guid id)
        {
            return await _deliveryTypeRepository.Delete(id);
        }

        public async Task<DeliveryType> ChangeActive(Guid id, bool active)
        {
            DeliveryType deliveryType = await _deliveryTypeRepository.GetById(id);
            deliveryType.IsActive = active;
            return await _deliveryTypeRepository.Update(id, deliveryType);
        }
    }
}
