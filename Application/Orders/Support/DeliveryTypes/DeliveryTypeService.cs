using Application.Orders.Support.DeliveryTypes.Data;
using Domain.Order.Support;
using Infrastructure.Orders.Support.DeliveryTypes;

namespace Application.Orders.Support.DeliveryTypes;

public class DeliveryTypeService : IDeliveryTypeService
{
    private readonly IDeliveryTypeRepository _deliveryTypeRepository;
    private readonly DeliveryTypeDtoMapper _deliveryTypeDtoMapper;
    
    public DeliveryTypeService(
        IDeliveryTypeRepository deliveryTypeRepository,
        DeliveryTypeDtoMapper deliveryTypeDtoMapper
    )
    {
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._deliveryTypeDtoMapper = deliveryTypeDtoMapper;
    }
    
    public async void ActivateDeliveryType(Guid deliveryTypeId)
    {
        try
        {
            var deliveryType = await _deliveryTypeRepository.GetById(deliveryTypeId);
            deliveryType.Activate();
            await this._deliveryTypeRepository.Update(deliveryTypeId, deliveryType);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public async void DeactivateDeliveryType(Guid deliveryTypeId)
    {
        try
        {
            var deliveryType = await _deliveryTypeRepository.GetById(deliveryTypeId);
            deliveryType.Deactivate();
            await this._deliveryTypeRepository.Update(deliveryTypeId, deliveryType);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public Task<DeliveryType> Create(DeliveryTypeDto dto)
    {
        DeliveryType deliveryType = this._deliveryTypeDtoMapper.Map(dto).Result;
        return this._deliveryTypeRepository.Create(deliveryType);
    }

    public Task<DeliveryType> Update(Guid id, DeliveryTypeDto dto)
    {
        DeliveryType deliveryType = this._deliveryTypeDtoMapper.Map(dto).Result;
        return this._deliveryTypeRepository.Update(id, deliveryType);   
    }
}