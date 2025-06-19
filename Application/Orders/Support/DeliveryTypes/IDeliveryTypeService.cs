using Application.Orders.Support.DeliveryTypes.Data;
using Domain.Orders.Support;

namespace Application.Orders.Support.DeliveryTypes;

public interface IDeliveryTypeService
{
    public Task ActivateDeliveryType(Guid deliveryTypeId);
    public Task DeactivateDeliveryType(Guid deliveryTypeId);
    public Task<DeliveryType> Create(DeliveryTypeDto dto);
    public Task<DeliveryType> Update(Guid id, DeliveryTypeDto dto);
}