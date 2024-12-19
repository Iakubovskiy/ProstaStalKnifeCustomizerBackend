using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class OrderStatusesService
    {
        private readonly Repository<OrderStatuses, int> _orderStatusesRepository;

        public OrderStatusesService(Repository<OrderStatuses, int> orderStatusesRepository)
        {
            _orderStatusesRepository = orderStatusesRepository;
        }

        public async Task<List<OrderStatuses>> GetAllOrderStatusess()
        {
            return await _orderStatusesRepository.GetAll();
        }

        public async Task<OrderStatuses> GetOrderStatusesById(int id)
        {
            return await _orderStatusesRepository.GetById(id);
        }

        public async Task<OrderStatuses> CreateOrderStatuses(OrderStatuses orderStatuses)
        {
            return await _orderStatusesRepository.Create(orderStatuses);
        }

        public async Task<OrderStatuses> UpdateOrderStatuses(int id, OrderStatuses orderStatuses)
        {
            return await _orderStatusesRepository.Update(id, orderStatuses);
        }

        public async Task<bool> DeleteOrderStatuses(int id)
        {
            return await _orderStatusesRepository.Delete(id);
        }
    }
}
