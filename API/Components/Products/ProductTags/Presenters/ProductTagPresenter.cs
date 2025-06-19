using Domain.Component.Product;

namespace API.Components.Products.ProductTags.Presenters;

public class ProductTagPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public async Task<ProductTagPresenter> Present(ProductTag engravingTag, string locale)
    {
        this.Id = engravingTag.Id;
        this.Name = engravingTag.Tag.GetTranslation(locale);
        return this;
    }

    public async Task<List<ProductTagPresenter>> PresentList(List<ProductTag> engravingTags, string locale)
    {
        List<ProductTagPresenter> presenters = new List<ProductTagPresenter>();

        foreach (ProductTag tag in engravingTags)
        {
            ProductTagPresenter presenter = new ProductTagPresenter();
            await presenter.Present(tag, locale);
            presenters.Add(presenter);
        }
        return presenters;
    }
}