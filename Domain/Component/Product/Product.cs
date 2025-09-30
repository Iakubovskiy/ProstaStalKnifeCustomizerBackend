using Domain.Component.Product.Reviews;
using Domain.Files;
using Domain.Translation;

namespace Domain.Component.Product;

public abstract class Product : IEntity, IComponent, IUpdatable<Product>
{
    protected Product()
    {
        
    }

    protected Product(
        Guid id, 
        bool isActive, 
        FileEntity image, 
        Translations name, 
        Translations title,
        Translations description, 
        Translations metaTitle, 
        Translations metaDescription,
        List<ProductTag> tags,
        DateTime createdAt,
        List<Review>? reviews = null
    )
    {
        this.Id = id;
        this.IsActive = isActive;
        this.Image = image;
        this.Name = name;
        this.Title = title;
        this.Description = description;
        this.MetaTitle = metaTitle;
        this.MetaDescription = metaDescription;
        this.Tags = tags;
        this.Reviews = reviews;
        this.CreatedAt = createdAt;
    }
    
    public Guid Id { get; private set;  }
    public bool IsActive { get; private set; }
    public FileEntity Image { get; private set; }
    public Translations Name { get; private set; }
    public Translations Title { get; private set; }
    public Translations Description { get; private set; }
    public Translations MetaTitle { get; private set; }
    public Translations MetaDescription { get; private set; }
    public List<ProductTag> Tags { get; private set; }
    public List<Review>? Reviews { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public abstract double GetPrice(double exchangerRate);

    public void Update(Product product)
    {
        this.IsActive = product.IsActive;
        this.Image = product.Image;
        this.Name = product.Name;
        this.Title = product.Title;
        this.Description = product.Description;
        this.MetaTitle = product.MetaTitle;
        this.MetaDescription = product.MetaDescription;
        this.Tags = product.Tags;
    }

    public void AddReview(Review review)
    {
        if (this.Reviews == null)
        {
            this.Reviews = new List<Review>();
        }
        this.Reviews.Add(review);
    }

    public void Activate()
    {
        this.IsActive = true;
    }

    public void Deactivate()
    {
        this.IsActive = false;
    }
}