using Domain.Orders.Support;

namespace Infrastructure.Orders.Support.PaymentMethods;

public interface IPaymentMethodRepository : IRepository<PaymentMethod>
{
    public Task<List<PaymentMethod>> GetAllActive();
}