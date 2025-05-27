using Domain.Order.Support;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Orders.Support.PaymentMethods;

public class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
{
    public PaymentMethodRepository(DBContext context) 
        : base(context)
    {
        
    }

    public async Task<List<PaymentMethod>> GetAllActive()
    {
        return await this.Set.Where(method => method.IsActive).ToListAsync();
    }
}