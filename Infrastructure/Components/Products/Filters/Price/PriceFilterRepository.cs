using System.Data.Entity.Core;
using Domain.Component.Engravings.Support;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Currencies;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products.Filters.Price;

public class PriceFilterRepository : IPriceFilterRepository
{
    private readonly DBContext _context;
    private readonly IRepository<EngravingPrice> _engravingPriceRepository;

    public PriceFilterRepository(
        DBContext context,
        IRepository<EngravingPrice> engravingPriceRepository
    )
    {
        this._context = context;
        this._engravingPriceRepository = engravingPriceRepository;
    }

    public async Task<(double, double)> GetPriceFilter(string currency)
    {
        Currency localCurrency = await _context.Currencies
            .FirstOrDefaultAsync(c => c.Name == currency) 
            ?? throw new ObjectNotFoundException("Currency");
        
        double engravingPrice = (await _engravingPriceRepository.GetAll())
            .FirstOrDefault()?.Price / localCurrency.ExchangeRate 
            ?? throw new ObjectNotFoundException("Price");

        var attachments = await _context.Attachments
            .Select(a => a.Price / localCurrency.ExchangeRate)
            .ToListAsync();

        var completedSheaths = await _context.CompletedSheaths
            .Include(cs => cs.SheathColor)
                .ThenInclude(sc => sc.Prices)
            .Include(cs => cs.Sheath)
                .ThenInclude(s => s.Type)
            .Include(cs => cs.Engravings)
            .Include(cs => cs.Attachments)
            .ToListAsync();

        var knives = await _context.Knives
            .Include(k => k.Blade)
                .ThenInclude(b => b.Type)
            .Include(k => k.SheathColor)
                .ThenInclude(sc => sc.Prices)
            .Include(k => k.Handle)
            .Include(k => k.Color)
            .Include(k => k.Sheath)
            .Include(k => k.Engravings)
            .Include(k => k.Attachments)
            .ToListAsync();

        var attachmentPrices = attachments;
        
        var sheathPrices = completedSheaths.Select(sheath => CalculateSheathPrice(sheath, localCurrency, engravingPrice));
        
        var knifePrices = knives.Select(knife => CalculateKnifePrice(knife, localCurrency, engravingPrice));

        var allPrices = attachmentPrices.Concat(sheathPrices).Concat(knifePrices);

        var enumerable = allPrices.ToList();
        return (enumerable.Min(), enumerable.Max());
    }

    private double CalculateSheathPrice(CompletedSheath sheath, Currency localCurrency, double engravingPrice)
    {
        var basePrice = sheath.SheathColor.Prices
            .Where(p => p.TypeId == sheath.Sheath.Type.Id)
            .Select(p => p.Price)
            .First() / localCurrency.ExchangeRate;

        var sheathPrice = sheath.Sheath.Price / localCurrency.ExchangeRate;

        var engravingCount = sheath.Engravings?.Select(e => e.Side).Distinct().Count() ?? 0;

        var attachmentSum = sheath.Attachments?.Sum(a => a.Price / localCurrency.ExchangeRate) ?? 0;

        return basePrice + sheathPrice + (engravingCount * engravingPrice) + attachmentSum;
    }

    private double CalculateKnifePrice(Knife knife, Currency localCurrency, double engravingPrice)
    {
        var bladePrice = knife.Blade.Price / localCurrency.ExchangeRate;
        
        var sheathColorPrice = knife.SheathColor?.Prices
            .Where(price => price.TypeId == knife.Blade.Type.Id)
            .Select(price => price.Price)
            .FirstOrDefault() / localCurrency.ExchangeRate ?? 0;
        
        var handlePrice = knife.Handle?.Price / localCurrency.ExchangeRate ?? 0;
        
        var coatingPrice = knife.Color.Price / localCurrency.ExchangeRate;
        
        var sheathPrice = knife.Sheath?.Price / localCurrency.ExchangeRate ?? 0;
        
        var engravingCount = knife.Engravings?.Select(e => e.Side).Distinct().Count() ?? 0;
        
        var attachmentsPricesSum = knife.Attachments?.Sum(a => a.Price / localCurrency.ExchangeRate) ?? 0;

        return bladePrice + handlePrice + coatingPrice + sheathPrice + sheathColorPrice + 
               (engravingCount * engravingPrice) + attachmentsPricesSum;
    }
}