using Domain.Component.Translation;

namespace Domain.Component.Product;

public abstract class Product : IEntity, IComponent
{
    protected Product()
    {
        
    }

    protected Product(
        Guid id, 
        bool isActive, 
        string imageUrl, 
        Translations name, 
        Translations title,
        Translations description, 
        Translations metaTitle, 
        Translations metaDescription
    )
    {
        if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid product image URL");
        }
        this.Id = id;
        this.IsActive = isActive;
        this.ImageUrl = imageUrl;
        this.Name = name;
        this.Title = title;
        this.Description = description;
        this.MetaTitle = metaTitle;
        this.MetaDescription = metaDescription;
    }
    
    public Guid Id { get; private set;  }
    public bool IsActive { get; private set; }
    public string ImageUrl { get; private set; }
    public Translations Name { get; private set; }
    public Translations Title { get; private set; }
    public Translations Description { get; private set; }
    public Translations MetaTitle { get; private set; }
    public Translations MetaDescription { get; private set; }

    public abstract double GetPrice();

    public void Update(Product product)
    {
        if (!Uri.IsWellFormedUriString(product.ImageUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid product image URL");
        }
        this.IsActive = product.IsActive;
        this.ImageUrl = product.ImageUrl;
        this.Name = product.Name;
        this.Title = product.Title;
        this.Description = product.Description;
        this.MetaTitle = product.MetaTitle;
        this.MetaDescription = product.MetaDescription;
    }
}