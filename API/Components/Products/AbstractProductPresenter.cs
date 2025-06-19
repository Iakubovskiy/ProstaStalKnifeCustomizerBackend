using API.Components.Products.AllProducts.Presenters;
using Domain.Component.Product;
using Domain.Files;

namespace API.Components.Products;

public class AbstractProductPresenter
{
    public Guid Id { get; set; }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public string Name {get; set;}
    public string Description {get; set;}
    public double Price {get; set;}
    public FileEntity ImageUrl {get; set;}
    public List<ReviewPresenter>? Reviews {get; set;}
    public double? AverageRating { get; set; }
}