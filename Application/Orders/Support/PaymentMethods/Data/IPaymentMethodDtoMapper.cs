using Domain.Order.Support;

namespace Application.Orders.Support.PaymentMethods.Data;

public interface IPaymentMethodDtoMapper
{
    public Task<PaymentMethod> Map(PaymentMethodDto dto);
}