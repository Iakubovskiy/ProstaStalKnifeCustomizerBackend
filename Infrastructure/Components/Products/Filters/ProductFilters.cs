namespace Infrastructure.Components.Products.Filters;

public class ProductFilters
{
    public string? ProductType { get; set; }
    public List<string>? Styles { get; set; }
    public double? MinBladeLength { get; set; }
    public double? MaxBladeLength { get; set; }
    public double? MinTotalLength { get; set; }
    public double? MaxTotalLength { get; set; }
    public double? MinBladeWidth { get; set; }
    public double? MaxBladeWidth { get; set; }
    public double? MinBladeWeight { get; set; }
    public double? MaxBladeWeight { get; set; }
    public List<string>? Colors { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
}