using Domain.Files;
using Domain.Translation;

namespace Domain.Component.Product.Attachments;

public class Attachment : Product, IUpdatable<Attachment>
{
    protected Attachment() : base()
    {
        
    }

    public Attachment(
        Guid id,
        bool isActive,
        FileEntity image,
        Translations name,
        Translations title,
        Translations description,
        Translations metaTitle,
        Translations metaDescription,
        List<ProductTag> tags,
        AttachmentType type, 
        Translations color, 
        double price, 
        Translations material,
        string modelUrl
    ) : base(
        id, 
        isActive, 
        image, 
        name, 
        title, 
        description, 
        metaTitle, 
        metaDescription,
        tags
    )
    {
        if (string.IsNullOrWhiteSpace(modelUrl) || !Uri.IsWellFormedUriString(modelUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid attachment model URL or it's empty");
        }

        if (price <= 0)
        {
            throw new ArgumentException("Attachment price is lower then 0");
        }
        this.Type = type;
        this.Color = color;
        this.Price = price;
        this.Material = material;
        this.ModelUrl = modelUrl;
    }
    
    public AttachmentType Type { get; private set; }
    public Translations Color { get; private set; }
    public double Price { get; private set; }
    public Translations Material { get; private set; }
    public string ModelUrl { get; private set; }
    
    public override double GetPrice(double exchangerRate)
    {
        return this.Price / exchangerRate;
    }

    public void Update(Attachment attachment)
    {
        if (string.IsNullOrWhiteSpace(attachment.ModelUrl) || !Uri.IsWellFormedUriString(attachment.ModelUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid attachment model URL or it's empty");
        }

        if (attachment.Price <= 0)
        {
            throw new ArgumentException("Attachment price is lower then 0");
        }
        base.Update(attachment);
        this.Type = attachment.Type;
        this.Color = attachment.Color;
        this.Price = attachment.Price;
        this.Material = attachment.Material;
        this.ModelUrl = attachment.ModelUrl;
    }
}
