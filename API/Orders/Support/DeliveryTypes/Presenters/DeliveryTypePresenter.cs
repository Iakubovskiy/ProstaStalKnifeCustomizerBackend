using Application.Currencies;
using Domain.Orders.Support;

namespace API.Orders.Support.DeliveryTypes.Presenters;

public class DeliveryTypePresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public double Price { get; set; }
    public string? Comment { get; set; }
    public Dictionary<string, string>? Comments { get; set; }
    public bool IsActive { get; set; }

    public static async Task<DeliveryTypePresenter> Present(
        DeliveryType deliveryType, 
        string locale, 
        string currency, 
        IPriceService priceService)
    {
        return new DeliveryTypePresenter
        {
            Id = deliveryType.Id,
            Name = deliveryType.Name.GetTranslation(locale),
            Price = await priceService.GetPrice(deliveryType.Price, currency),
            Comment = deliveryType.Comment?.GetTranslation(locale),
            IsActive = deliveryType.IsActive
        };
    }

    public static async Task<DeliveryTypePresenter> PresentWithTranslations(
        DeliveryType deliveryType, 
        string locale, 
        string currency, 
        IPriceService priceService)
    {
        DeliveryTypePresenter presenter = await Present(deliveryType, locale, currency, priceService);
        presenter.Names = deliveryType.Name.TranslationDictionary;
        presenter.Comments = deliveryType.Comment?.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<DeliveryTypePresenter>> PresentList(
        List<DeliveryType> deliveryTypes, 
        string locale, 
        string currency, 
        IPriceService priceService)
    {
        List<DeliveryTypePresenter> deliveryTypesPresenters = new List<DeliveryTypePresenter>();
        foreach (DeliveryType deliveryType in deliveryTypes)
        {
            DeliveryTypePresenter deliveryTypePresenter = await Present(deliveryType, locale, currency, priceService);
            deliveryTypesPresenters.Add(deliveryTypePresenter);
        }
        
        return deliveryTypesPresenters;
    }
}