using Domain.Orders.Support;
using Domain.Translation;

namespace Infrastructure.Data.Orders.Support;

public class DeliveryTypeSeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<DeliveryType> _deliveryTypeRepository;
    
    public DeliveryTypeSeeder(IRepository<DeliveryType> deliveryTypeRepository)
    {
        this._deliveryTypeRepository = deliveryTypeRepository;
    }
    
    public async Task SeedAsync()
    {
        int count = (await this._deliveryTypeRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        
        DeliveryType deliveryType1 = new DeliveryType(
            new Guid("b6d15e84-3d69-4b74-b270-cb5e9fd0d2d3"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Nova Poshta" },
                { "ua", "Нова Пошта" },
            }),
            50.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Delivery to post office" },
                { "ua", "Доставка у відділення" },
            }),
            true
        );
        
        DeliveryType deliveryType2 = new DeliveryType(
            new Guid("79beccad-8373-4f94-935d-43dd8d97975c"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Nova Poshta Courier" },
                { "ua", "Нова Пошта Кур'єр" },
            }),
            80.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Courier delivery to your door" },
                { "ua", "Кур'єрська доставка до дверей" },
            }),
            true
        );
        
        DeliveryType deliveryType3 = new DeliveryType(
            new Guid("7ac885c8-e468-4180-bdf0-c73ac7afdead"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Ukrposhta" },
                { "ua", "Укрпошта" },
            }),
            35.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Standard postal delivery" },
                { "ua", "Стандартна поштова доставка" },
            }),
            true
        );
        
        DeliveryType deliveryType4 = new DeliveryType(
            new Guid("3fc6562b-4e9b-4530-9895-d01de959bafc"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Self Pickup" },
                { "ua", "Самовивіз" },
            }),
            0.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Pickup from our store" },
                { "ua", "Забрати з нашого магазину" },
            }),
            true
        );
        
        DeliveryType deliveryType5 = new DeliveryType(
            new Guid("8eaa6dbf-1778-49be-9329-111b6d765140"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Express Delivery" },
                { "ua", "Експрес доставка" },
            }),
            150.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Same day delivery" },
                { "ua", "Доставка в день замовлення" },
            }),
            true
        );
        
        DeliveryType deliveryType6 = new DeliveryType(
            new Guid("4ac9a2c0-9543-40ec-9f20-8c521ad34711"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "International DHL" },
                { "ua", "Міжнародна DHL" },
            }),
            500.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "International express delivery" },
                { "ua", "Міжнародна експрес доставка" },
            }),
            true
        );
        
        DeliveryType deliveryType7 = new DeliveryType(
            new Guid("cbd2d78c-945a-4a79-a7b3-323a25c76850"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Meest Express" },
                { "ua", "Міст Експрес" },
            }),
            45.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Delivery to parcel locker" },
                { "ua", "Доставка до поштомату" },
            }),
            true
        );
        
        DeliveryType deliveryType8 = new DeliveryType(
            new Guid("d3bc7807-b59d-4bc8-bdbf-92dc421cacad"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Justin" },
                { "ua", "Джастін" },
            }),
            40.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Delivery to post office" },
                { "ua", "Доставка у відділення" },
            }),
            false
        );
        
        DeliveryType deliveryType9 = new DeliveryType(
            new Guid("0e58ea41-4264-44d7-ab00-e377d8a926dd"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "InTime" },
                { "ua", "ІнТайм" },
            }),
            55.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Fast delivery service" },
                { "ua", "Швидка служба доставки" },
            }),
            true
        );
        
        DeliveryType deliveryType10 = new DeliveryType(
            new Guid("e8b91597-2f10-4585-bbe1-d62c6ae0147e"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Delivery" },
                { "ua", "Делівері" },
            }),
            60.0,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Regional delivery service" },
                { "ua", "Регіональна служба доставки" },
            }),
            true
        );
        
        await this._deliveryTypeRepository.Create(deliveryType1);
        await this._deliveryTypeRepository.Create(deliveryType2);
        await this._deliveryTypeRepository.Create(deliveryType3);
        await this._deliveryTypeRepository.Create(deliveryType4);
        await this._deliveryTypeRepository.Create(deliveryType5);
        await this._deliveryTypeRepository.Create(deliveryType6);
        await this._deliveryTypeRepository.Create(deliveryType7);
        await this._deliveryTypeRepository.Create(deliveryType8);
        await this._deliveryTypeRepository.Create(deliveryType9);
        await this._deliveryTypeRepository.Create(deliveryType10);
    }
}