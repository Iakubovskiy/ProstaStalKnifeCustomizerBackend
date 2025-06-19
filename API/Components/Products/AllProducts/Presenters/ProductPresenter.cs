using Application.Components.Prices;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.Product;
using Domain.Component.Product.Knife;
using Domain.Files;
using Infrastructure;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Components.Products.AllProducts.Presenters;

public class ProductPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public FileEntity Image { get; set; }
    public double Price { get; set; }
    public List<ReviewPresenter> Reviews { get; set; } = new List<ReviewPresenter>();
    public BladeCharacteristics? Characteristics { get; set; }

    public static async Task<ProductPresenter> Present(
        Product product, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var presenter = new ProductPresenter
        {
            Id = product.Id,
            Name = product.Name.GetTranslation(locale),
            Image = product.Image,
            Price = await getComponentPriceService.GetPrice(product, currency)
        };

        if (product is Knife knife)
        {
            presenter.Characteristics = knife.Blade.BladeCharacteristics;
        }

        if (product.Reviews != null)
        {
            presenter.Reviews = ReviewPresenter.PresentList(product.Reviews);
        }
        return presenter;
    }
    
    public static async Task<ProductPresenter> PresentWithTranslations(
        Product product, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        ProductPresenter presenter = await Present(product, locale, currency, getComponentPriceService);
        presenter.Names = product.Name.TranslationDictionary;
        return presenter;
    }

    public static async Task<PaginatedResult<ProductPresenter>> PresentPaginatedList(
        PaginatedResult<Product> products, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPriceService)
    {
        var items = new List<ProductPresenter>();
        foreach (Product product in products.Items)
        {
            items.Add(await Present(product, locale, currency, getComponentPriceService));
        }
        
        return new PaginatedResult<ProductPresenter>
        {
            Items = items,
            Page = products.Page,
            PageSize = products.PageSize,
            TotalItems = products.TotalItems,
        };
    }
}