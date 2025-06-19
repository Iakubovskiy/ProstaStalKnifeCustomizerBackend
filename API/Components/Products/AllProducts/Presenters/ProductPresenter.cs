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
    private readonly IGetComponentPrice _getComponentPriceService;

    public ProductPresenter(IGetComponentPrice getComponentPriceService)
    {
        _getComponentPriceService = getComponentPriceService;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public FileEntity Image { get; set; }
    public double Price { get; set; }
    public List<ReviewPresenter> Reviews { get; set; } = new List<ReviewPresenter>();
    
    public BladeCharacteristics? Characteristics { get; set; }

    public async Task<ProductPresenter> Present(Product product, string locale, string currency)
    {
        this.Id = product.Id;
        this.Name = product.Name.GetTranslation(locale);
        this.Image = product.Image;
        this.Price = await this._getComponentPriceService.GetPrice(product, currency);
        if (product is Knife knife)
        {
            this.Characteristics = knife.Blade.BladeCharacteristics;
        }

        if (product.Reviews != null)
        {
            ReviewPresenter presenter = new ReviewPresenter();
            this.Reviews = presenter.PresentList(product.Reviews);
        }
        return this;
    }
    
    public async Task<ProductPresenter> PresentWithTranslations(Product product, string locale, string currency)
    {
        await this.Present(product, locale, currency);
        this.Names = product.Name.TranslationDictionary;
        return this;
    }

    public async Task<PaginatedResult<ProductPresenter>> PresentPaginatedList(PaginatedResult<Product> products, string locale, string currency)
    {
        List<ProductPresenter> items = new List<ProductPresenter>();
        foreach (Product product in products.Items)
        {
            ProductPresenter productPresenter = new ProductPresenter(_getComponentPriceService);
            items.Add(await productPresenter.Present(product, locale, currency));
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