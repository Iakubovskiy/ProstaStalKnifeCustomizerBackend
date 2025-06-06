using Domain.Component.Product;
using Domain.Translation;

namespace Infrastructure.Data.Component.Products;

public class ProductTagSeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<ProductTag> _productTagRepository;
    
    public ProductTagSeeder(IRepository<ProductTag> productTagRepository)
    {
        this._productTagRepository = productTagRepository;
    }
    
    public async Task SeedAsync()
    {
        int count = (await this._productTagRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        
        ProductTag productTag1 = new ProductTag(
            new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Handmade" },
                { "ua", "Ручна робота" },
            })
        );
        
        ProductTag productTag2 = new ProductTag(
            new Guid("b2c3d4e5-f6a7-8901-2345-67890abcdef1"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Premium" },
                { "ua", "Преміум" },
            })
        );
        
        ProductTag productTag3 = new ProductTag(
            new Guid("c3d4e5f6-a7b8-9012-3456-7890abcdef12"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Limited Edition" },
                { "ua", "Обмежена серія" },
            })
        );
        
        ProductTag productTag4 = new ProductTag(
            new Guid("d4e5f6a7-b8c9-0123-4567-890abcdef123"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Custom" },
                { "ua", "На замовлення" },
            })
        );
        
        ProductTag productTag5 = new ProductTag(
            new Guid("e5f6a7b8-c9d0-1234-5678-90abcdef1234"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Professional" },
                { "ua", "Професійний" },
            })
        );
        
        ProductTag productTag6 = new ProductTag(
            new Guid("f6a7b8c9-d0e1-2345-6789-0abcdef12345"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Collectible" },
                { "ua", "Колекційний" },
            })
        );
        
        ProductTag productTag7 = new ProductTag(
            new Guid("a7b8c9d0-e1f2-3456-789a-bcdef1234567"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Tactical" },
                { "ua", "Тактичний" },
            })
        );
        
        ProductTag productTag8 = new ProductTag(
            new Guid("b8c9d0e1-f2a3-4567-89ab-cdef12345678"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Outdoor" },
                { "ua", "Для активного відпочинку" },
            })
        );
        
        ProductTag productTag9 = new ProductTag(
            new Guid("c9d0e1f2-a3b4-5678-9abc-def123456789"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Gift" },
                { "ua", "Подарунок" },
            })
        );
        
        ProductTag productTag10 = new ProductTag(
            new Guid("d0e1f2a3-b4c5-6789-abcd-ef123456789a"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Bestseller" },
                { "ua", "Бестселер" },
            })
        );
        
        await this._productTagRepository.Create(productTag1);
        await this._productTagRepository.Create(productTag2);
        await this._productTagRepository.Create(productTag3);
        await this._productTagRepository.Create(productTag4);
        await this._productTagRepository.Create(productTag5);
        await this._productTagRepository.Create(productTag6);
        await this._productTagRepository.Create(productTag7);
        await this._productTagRepository.Create(productTag8);
        await this._productTagRepository.Create(productTag9);
        await this._productTagRepository.Create(productTag10);
    }
}