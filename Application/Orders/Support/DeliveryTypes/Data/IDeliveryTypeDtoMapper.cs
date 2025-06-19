using Domain.Orders.Support;

namespace Application.Orders.Support.DeliveryTypes.Data;

public interface IDeliveryTypeDtoMapper
{
    public Task<DeliveryType> Map(DeliveryTypeDto dto);
}