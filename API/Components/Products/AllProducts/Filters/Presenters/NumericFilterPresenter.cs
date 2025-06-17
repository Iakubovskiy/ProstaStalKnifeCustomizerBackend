namespace API.Components.Products.AllProducts.Filters.Presenters;

public class NumericFilterPresenter
{
    public string Name { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }

    public static NumericFilterPresenter Present(string name, double min, double max)
    {
        NumericFilterPresenter presenter = new NumericFilterPresenter();
        presenter.Name = name;
        presenter.Min = min;
        presenter.Max = max;
        return presenter;
    }
}