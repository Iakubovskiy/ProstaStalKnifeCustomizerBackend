using Application.Orders.Support.PaymentMethods.Data;
using Domain.Order.Support;
using Infrastructure.Orders.Support.PaymentMethods;

namespace Application.Orders.Support.PaymentMethods;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly PaymentMethodDtoMapper _paymentMethodDtoMapper;
    
    public PaymentMethodService(
        IPaymentMethodRepository paymentMethodRepository,
        PaymentMethodDtoMapper paymentMethodDtoMapper
    )
    {
        this._paymentMethodRepository = paymentMethodRepository;
        this._paymentMethodDtoMapper = paymentMethodDtoMapper;
    }
    
    public async void ActivatePaymentMethod(Guid paymentMethodId)
    {
        try
        {
            PaymentMethod paymentMethod = await _paymentMethodRepository.GetById(paymentMethodId);
            paymentMethod.Activate();
            await this._paymentMethodRepository.Update(paymentMethodId, paymentMethod);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public async void DeactivatePaymentMethod(Guid paymentMethodId)
    {
        try
        {
            var paymentMethod = await _paymentMethodRepository.GetById(paymentMethodId);
            paymentMethod.Deactivate();
            await this._paymentMethodRepository.Update(paymentMethodId, paymentMethod);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public Task<PaymentMethod> Create(PaymentMethodDto dto)
    {
        PaymentMethod paymentMethod = this._paymentMethodDtoMapper.Map(dto).Result;
        return this._paymentMethodRepository.Create(paymentMethod);
    }

    public Task<PaymentMethod> Update(Guid id, PaymentMethodDto dto)
    {
        PaymentMethod paymentMethod = this._paymentMethodDtoMapper.Map(dto).Result;
        return this._paymentMethodRepository.Update(id, paymentMethod);   
    }
}