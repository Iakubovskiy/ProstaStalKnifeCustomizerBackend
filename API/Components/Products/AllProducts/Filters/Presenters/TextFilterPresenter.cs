namespace API.Components.Products.AllProducts.Filters.Presenters;

public class TextFilterPresenter
{
    public string Name { get; set; }
    public List<string> Data { get; set; }

    public static TextFilterPresenter Present(string name, List<string> data)
    {
        TextFilterPresenter presenter = new TextFilterPresenter();
        presenter.Name = name;
        presenter.Data = data;
        return presenter;
    }
}