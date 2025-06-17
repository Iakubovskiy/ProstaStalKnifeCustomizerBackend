using Application.Components.Prices;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Infrastructure;

namespace API.Components.Products.AllProducts.Presenters;

public class ProductPresenter
{
    private readonly IGetComponentPrice _getComponentPriceService;

    public ProductPresenter(IGetComponentPrice getComponentPriceService)
    {
        this._getComponentPriceService = getComponentPriceService;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    
    public string Type { get; set; }
    public BladeCharacteristics? Characteristics { get; set; }

    public async Task<ProductPresenter> Present(Product product, string locale, string currency)
    {
        this.Id = product.Id;
        this.Name = product.Name.GetTranslation(locale);
        this.ImageUrl = product.Image.FileUrl;
        this.Price = await this._getComponentPriceService.GetPrice(product, currency);
        if (product is Knife knife)
        {
            this.Characteristics = knife.Blade.BladeCharacteristics;
            this.Type = "knife";
        }
        else if (product is CompletedSheath)
        {
            this.Type = "completed_sheath";
        }
        else if (product is Attachment)
        {
            this.Type = "attachment";
        }
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