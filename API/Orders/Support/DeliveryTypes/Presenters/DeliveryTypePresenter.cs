using Application.Currencies;
using Domain.Orders.Support;

namespace API.Orders.Support.DeliveryTypes.Presenters;

public class DeliveryTypePresenter
{
    // id: number;
    // name: string;
    // price: number;
    // comment: string;
    private readonly IPriceService _priceService;

    public DeliveryTypePresenter(IPriceService priceService)
    {
        this._priceService = priceService;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Comment { get; set; }
    public bool IsActive { get; set; }

    public async Task<DeliveryTypePresenter> Present(DeliveryType deliveryType, string locale, string currency)
    {
        this.Id = deliveryType.Id;
        this.Name = deliveryType.Name.GetTranslation(locale);
        this.Price = await this._priceService.GetPrice(deliveryType.Price, currency);
        this.Comment = deliveryType.Comment?.GetTranslation(locale);
        this.IsActive = deliveryType.IsActive;
        
        return this;
    }

    public async Task<List<DeliveryTypePresenter>> PresentList(List<DeliveryType> deliveryTypes, string locale,
        string currency)
    {
        List<DeliveryTypePresenter> deliveryTypesPresenters = new List<DeliveryTypePresenter>();
        foreach (DeliveryType deliveryType in deliveryTypes)
        {
            DeliveryTypePresenter deliveryTypePresenter = new DeliveryTypePresenter(this._priceService);
            await deliveryTypePresenter.Present(deliveryType, locale, currency);
            deliveryTypesPresenters.Add(deliveryTypePresenter);
        }
        
        return deliveryTypesPresenters;
    }
}