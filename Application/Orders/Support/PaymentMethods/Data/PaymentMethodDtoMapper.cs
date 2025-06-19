using Domain.Translation;
using Domain.Orders.Support;
using Newtonsoft.Json;

namespace Application.Orders.Support.PaymentMethods.Data;

public class PaymentMethodDtoMapper : IPaymentMethodDtoMapper
{
    public async Task<PaymentMethod> Map(PaymentMethodDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Names);
        Translations? description = new Translations(dto.Description);

        return new PaymentMethod(
            id,
            names,
            description,
            dto.IsActive
        );
    }
}