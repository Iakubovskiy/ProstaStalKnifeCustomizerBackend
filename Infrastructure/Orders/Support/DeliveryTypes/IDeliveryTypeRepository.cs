using Domain.Order.Support;

namespace Infrastructure.Orders.Support.DeliveryTypes;

public interface IDeliveryTypeRepository : IRepository<DeliveryType>
{
    public Task<List<DeliveryType>> GetAllActive();
}