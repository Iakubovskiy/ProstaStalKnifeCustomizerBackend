using Domain.Component.Engravings.Support;

namespace Infrastructure.Data.Component.Engravings;

public class EngravingPriceSeed : ISeeder
{
    private readonly IRepository<EngravingPrice> _engravingPriceRepository;

    public EngravingPriceSeed(IRepository<EngravingPrice> engravingPriceRepository)
    {
        this._engravingPriceRepository = engravingPriceRepository;
    }
    public int Priority => 0;
    public async Task SeedAsync()
    {
        if ((await this._engravingPriceRepository.GetAll()).Any())
        {
            return;
        }
        EngravingPrice price = new EngravingPrice(
            new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), 
            250
        );
        await this._engravingPriceRepository.Create(price);
    }
}