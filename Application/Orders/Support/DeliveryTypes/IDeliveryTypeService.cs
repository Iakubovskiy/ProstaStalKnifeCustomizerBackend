using Application.Orders.Support.DeliveryTypes.Data;
using Domain.Order.Support;

namespace Application.Orders.Support.DeliveryTypes;

public interface IDeliveryTypeService
{
    public void ActivateDeliveryType(Guid deliveryTypeId);
    public void DeactivateDeliveryType(Guid deliveryTypeId);
    public Task<DeliveryType> Create(DeliveryTypeDto dto);
    public Task<DeliveryType> Update(Guid id, DeliveryTypeDto dto);
}