using Domain.Component.Product;

namespace API.Components.Products.ProductTags.Presenters;

public class ProductTagPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }

    public static Task<ProductTagPresenter> Present(ProductTag engravingTag, string locale)
    {
        var presenter = new ProductTagPresenter
        {
            Id = engravingTag.Id,
            Name = engravingTag.Tag.GetTranslation(locale)
        };
        return Task.FromResult(presenter);
    }

    public static async Task<ProductTagPresenter> PresentWithTranslations(ProductTag engravingTag, string locale)
    {
        ProductTagPresenter presenter = await Present(engravingTag, locale);
        presenter.Names = engravingTag.Tag.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<ProductTagPresenter>> PresentList(List<ProductTag> engravingTags, string locale)
    {
        var presenters = new List<ProductTagPresenter>();
        foreach (ProductTag tag in engravingTags)
        {
            ProductTagPresenter presenter = await Present(tag, locale);
            presenters.Add(presenter);
        }
        return presenters;
    }
}