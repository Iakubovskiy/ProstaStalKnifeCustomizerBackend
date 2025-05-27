using Domain.Order.Support;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Orders.Support.DeliveryTypes;

public class DeliveryTypeRepository : BaseRepository<DeliveryType>, IDeliveryTypeRepository
{
    public DeliveryTypeRepository(DBContext context) 
        : base(context)
    {
        
    }

    public async Task<List<DeliveryType>> GetAllActive()
    {
        return await this.Context.DeliveryTypes.Where(deliveryType => deliveryType.IsActive).ToListAsync();
    }
}