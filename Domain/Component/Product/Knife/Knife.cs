using Domain.Component.BladeCoatingColors;
using Domain.Component.Translation;
using Domain.Component.BladeShapes;
using Domain.Component.Engravings;
using Domain.Component.Handles;
using Domain.Component.Product.Attachments;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;

namespace Domain.Component.Product.Knife;

public class Knife : Product, IUpdatable<Knife>
{
    private Knife()
    {
        
    }

    public Knife(
        Guid id,
        bool isActive,
        string imageUrl,
        Translations name,
        Translations title,
        Translations description,
        Translations metaTitle,
        Translations metaDescription,
        List<ProductTag> tags,
        BladeShape blade,
        BladeCoatingColor color,
        Handle? handle, 
        Sheath? sheath,
        SheathColor? sheathColor,
        List<Engraving>? engravings,
        List<Attachment>? attachments
    ) : base(
        id, 
        isActive, 
        imageUrl, 
        name, 
        title, 
        description, 
        metaTitle, 
        metaDescription,
        tags
    )
    {
        if (sheath != null && sheathColor == null)
        {
            throw new ArgumentException("Sheath color is required when sheath is set");
        }

        if (sheathColor != null && sheath == null)
        {
            throw new ArgumentException("Sheath is required when sheath color is set");
        }
        
        if (sheath != null && blade.Type != sheath.Type)
        {
            throw new ArgumentException("Sheath type is not compatible with blade type");
        }
        
        this.Blade = blade;
        this.Color = color;
        this.Handle = handle;
        this.Sheath = sheath;
        this.SheathColor = sheathColor;
        this.Engravings = engravings;
        this.Attachments = attachments;
    }
    
    public BladeShape Blade { get; private set; }
    public BladeCoatingColor Color { get; private set; }
    public Handle? Handle { get; private set; }
    public Sheath? Sheath { get; private set; }
    public SheathColor? SheathColor { get; private set; }
    public List<Engraving>? Engravings { get; private set; }
    public List<Attachment>? Attachments { get; private set; }

    public override double GetPrice(double exchangerRate)
    {
        double price = 0;
        price += this.Blade.GetPrice(exchangerRate);
        price += this.Color.GetPrice(exchangerRate);
        price += this.Handle?.GetPrice(exchangerRate) ?? 0;
        price += this.Sheath?.GetPrice(exchangerRate) ?? 0;
        price += this.SheathColor?.GetPrice(this.Blade.Type , exchangerRate) ?? 0;
        price += this.Engravings?.Sum(x => x.GetPrice(exchangerRate)) ?? 0;
        price += this.Attachments?.Sum(x => x.GetPrice(exchangerRate)) ?? 0;
        
        return price;
    }

    public void Update(Knife knife)
    {
        if (knife is { Sheath: not null, SheathColor: null })
        {
            throw new ArgumentException("Sheath color is required when sheath is set");
        }

        if (knife is { SheathColor: not null, Sheath: null })
        {
            throw new ArgumentException("Sheath is required when sheath color is set");
        }
        
        if (knife.Sheath != null && knife.Blade.Type != knife.Sheath.Type)
        {
            throw new ArgumentException("Sheath type is not compatible with blade type");
        }
        
        base.Update(knife);
        this.Blade = knife.Blade;
        this.Color = knife.Color;
        this.Handle = knife.Handle;
        this.Sheath = knife.Sheath;
        this.SheathColor = knife.SheathColor;
        this.Engravings = knife.Engravings;
        this.Attachments = knife.Attachments;
    }
}