using Application.Orders.Support.PaymentMethods.Data;
using Domain.Orders.Support;

namespace Application.Orders.Support.PaymentMethods;

public interface IPaymentMethodService
{
    public void ActivatePaymentMethod(Guid deliveryTypeId);
    public void DeactivatePaymentMethod(Guid deliveryTypeId);
    public Task<PaymentMethod> Create(PaymentMethodDto dto);
    public Task<PaymentMethod> Update(Guid id, PaymentMethodDto dto);
}