using Microsoft.EntityFrameworkCore.Infrastructure;
using WorkshopBackend.DTO;
using WorkshopBackend.Interfaces;
using WorkshopBackend.Models;

namespace WorkshopBackend.Services
{
    public class OrderService
    {
        private readonly Repository<Order, int> _orderRepository;
        private readonly Repository<OrderStatuses, int> _orderStatusesRepository;
        private readonly ICustomEmailSender _customEmailSender;

        public OrderService(Repository<Order, int> orderRepository, 
            Repository<OrderStatuses, int> orderStatusesRepository, ICustomEmailSender customEmailSender)
        {
            _orderRepository = orderRepository;
            _orderStatusesRepository = orderStatusesRepository;
            _customEmailSender = customEmailSender;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            return await _orderRepository.Create(order);
        }

        public async Task<Order> ChangeStatus(int id, string newStatus)
        {
            bool isValid = false;
            OrderStatuses newOrderStatus = new OrderStatuses();
            foreach (OrderStatuses status in await _orderStatusesRepository.GetAll())
            {
                if (status.Status.ToLower() == newStatus.ToLower())
                {
                    isValid = true;
                    newOrderStatus = status;
                    break;
                }
            }
            if (!isValid)
            {
                throw new Exception("Not valid status");
            }
            Order order = await _orderRepository.GetById(id);
            order.Status = newOrderStatus;
            return await _orderRepository.Update(id, order);
        }

        public async Task<Order> ChangeDeliveryType(int id, DeliveryType deliveryType)
        {
            Order order = await _orderRepository.GetById(id);
            order.DeliveryType = deliveryType;
            return await _orderRepository.Update(id, order);
        }

        public async Task<Order> ChangeDeliveryData(int id, DeliveryDataDTO deliveryData)
        {
            Order order = await _orderRepository.GetById(id);
            order.ClientFullName = deliveryData.ClientFullName;
            order.ClientPhoneNumber = deliveryData.ClientPhoneNumber;
            order.CountryForDelivery = deliveryData.CountryForDelivery;
            order.City = deliveryData.City;
            order.Email = deliveryData.Email;
            return await _orderRepository.Update(id, order);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _orderRepository.Delete(id);
        }

    }
}
