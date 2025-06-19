using Domain.Translation;
using Domain.Orders.Support;

namespace Application.Orders.Support.DeliveryTypes.Data;

public class DeliveryTypeDtoMapper : IDeliveryTypeDtoMapper
{
    public async Task<DeliveryType> Map(DeliveryTypeDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Names);
        Translations? comment = null;
        
        if(dto.Comments != null)
        {
            comment = new Translations(dto.Comments);
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