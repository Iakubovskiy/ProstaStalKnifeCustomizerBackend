using Domain.Translation;
using Domain.Order.Support;
using Newtonsoft.Json;

namespace Application.Orders.Support.DeliveryTypes.Data;

public class DeliveryTypeDtoMapper
{
    public async Task<DeliveryType> Map(DeliveryTypeDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names;
        Translations? comment = null;
        try
        {
            names = JsonConvert.DeserializeObject<Translations>(dto.NamesJson) 
                ?? throw new Exception("Names can't be empty");
        }
        catch (Exception e)
        {
            throw new Exception("Delivery type name can't be empty", e);
        }
        
        if(dto.CommentJson != null)
        {
            comment = JsonConvert.DeserializeObject<Translations>(dto.CommentJson);
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