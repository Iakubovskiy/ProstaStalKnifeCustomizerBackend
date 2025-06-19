using Application.Orders.Support.DeliveryTypes.Data;
using Domain.Orders.Support;
using Infrastructure.Orders.Support.DeliveryTypes;

namespace Application.Orders.Support.DeliveryTypes;

public class DeliveryTypeService : IDeliveryTypeService
{
    private readonly IDeliveryTypeRepository _deliveryTypeRepository;
    private readonly IDeliveryTypeDtoMapper _deliveryTypeDtoMapper;
    
    public DeliveryTypeService(
        IDeliveryTypeRepository deliveryTypeRepository,
        IDeliveryTypeDtoMapper deliveryTypeDtoMapper
    )
    {
        this._deliveryTypeRepository = deliveryTypeRepository;
        this._deliveryTypeDtoMapper = deliveryTypeDtoMapper;
    }
    
    public async Task ActivateDeliveryType(Guid deliveryTypeId)
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

    public async Task DeactivateDeliveryType(Guid deliveryTypeId)
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