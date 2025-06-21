using System.Data.Entity.Core;
using Domain.Currencies;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Currencies;

public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
{
    public CurrencyRepository(DBContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<Currency> GetByName(string name)
    {
        return await this.Set.FirstOrDefaultAsync(currency => currency.Name == name) 
               ?? throw new ObjectNotFoundException($"Entity not found {nameof(Currency)}");
    }
}