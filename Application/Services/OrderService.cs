using Domain.Models;
using Infrastructure.Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Data.DbContext;

namespace Application.Services
{
    public class OrderService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<OrderStatuses, Guid> _orderStatusesRepository;
        private readonly ICustomEmailSender _customEmailSender;
        private readonly DbContext _context;

        public OrderService(IRepository<Order, Guid> orderRepository, 
            IRepository<OrderStatuses, Guid> orderStatusesRepository, ICustomEmailSender customEmailSender,
            DbContext context)
        {
            _orderRepository = orderRepository;
            _orderStatusesRepository = orderStatusesRepository;
            _customEmailSender = customEmailSender;
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<OrderReturnDTO> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetById(id);
            var orderItems = await EntityFrameworkQueryableExtensions
                .Include(_context.OrderItems, oi => oi.Product)
                .Where(oi => oi.Order.Id == id)
                .ToListAsync();
            List<OrderProductDto> orderProducts = orderItems.Select(oi => new OrderProductDto
            {
                ProductId = oi.Product.Id,
                ProductType = oi.Product.GetType().Name,
                Quantity = oi.Quantity,
            }).ToList();

            OrderReturnDTO orderReturn = new OrderReturnDTO
            {
                Id = order.Id,
                City = order.City,
                ClientFullName = order.ClientFullName,
                ClientPhoneNumber = order.ClientPhoneNumber,
                Comment = order.Comment,
                CountryForDelivery = order.CountryForDelivery,
                DeliveryType = order.DeliveryType,
                Email = order.Email,
                Number = order.Number,
                Status = order.Status,
                Total = order.Total,
                Products = orderProducts,
            };

            return orderReturn;
        }

        public async Task<Order> CreateOrder(Order order, List<(Product product, int quantity)> productsWithQuantities)
        {
            var createdOrder = await _orderRepository.Create(order);
            foreach (var (product, quantity) in productsWithQuantities)
            {
                OrderItem orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Order.Id == createdOrder.Id && oi.Product.Id == product.Id);
                if (orderItem == null)
                    throw new Exception($"Product with id {product.Id} does not exist");
                orderItem.Quantity = quantity;               
            }
            await _context.SaveChangesAsync();
            return createdOrder;
        }

        public async Task<Order> ChangeStatus(Guid id, string newStatus)
        {
            bool isValid = false;
            foreach (OrderStatuses status in await _orderStatusesRepository.GetAll())
            {
                if (status.Status.ToLower() == newStatus.ToLower())
                {
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                throw new Exception("Not valid status");
            }
            Order order = await _orderRepository.GetById(id);
            order.Status = newStatus.ToLower();
            return await _orderRepository.Update(id, order);
        }

        public async Task<Order> ChangeDeliveryType(Guid id, DeliveryType deliveryType)
        {
            Order order = await _orderRepository.GetById(id);
            order.DeliveryType = deliveryType;
            return await _orderRepository.Update(id, order);
        }

        public async Task<Order> ChangeDeliveryData(Guid id, DeliveryDataDTO deliveryData)
        {
            Order order = await _orderRepository.GetById(id);
            order.ClientFullName = deliveryData.ClientFullName;
            order.ClientPhoneNumber = deliveryData.ClientPhoneNumber;
            order.CountryForDelivery = deliveryData.CountryForDelivery;
            order.City = deliveryData.City;
            order.Email = deliveryData.Email;
            return await _orderRepository.Update(id, order);
        }

        public async Task<bool> DeleteOrder(Guid id)
        {
            return await _orderRepository.Delete(id);
        }

    }
}
