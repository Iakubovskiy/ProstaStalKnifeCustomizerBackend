using Domain.Translation;
using Domain.Order.Support;
using Newtonsoft.Json;

namespace Application.Orders.Support.PaymentMethods.Data;

public class PaymentMethodDtoMapper
{
    public async Task<PaymentMethod> Map(PaymentMethodDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names;
        Translations? description = null;
        try
        {
            names = JsonConvert.DeserializeObject<Translations>(dto.NamesJson) 
                ?? throw new Exception("Names can't be empty");
        }
        catch (Exception e)
        {
            throw new Exception("Name can't be empty", e);
        }
        try
        {
            description = JsonConvert.DeserializeObject<Translations>(dto.DescriptionJson) 
                    ?? throw new Exception("Description can't be empty");
        }
        catch (Exception e)
        {
            throw new Exception("Description can't be empty", e);
        }
        

        return new PaymentMethod(
            id,
            names,
            description,
            dto.IsActive
        );
    }
}