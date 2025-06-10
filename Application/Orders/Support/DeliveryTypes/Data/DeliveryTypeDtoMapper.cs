using Domain.Translation;
using Domain.Order.Support;
using Newtonsoft.Json;

namespace Application.Orders.Support.DeliveryTypes.Data;

public class DeliveryTypeDtoMapper : IDeliveryTypeDtoMapper
{
    public async Task<DeliveryType> Map(DeliveryTypeDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Names);
        Translations? comment = null;
        
        if(dto.Comment != null)
        {
            comment = new Translations(dto.Comment);
        }

        return new DeliveryType(
            id,
            names,
            dto.Price,
            comment,
            dto.IsActive
        );
    }
}