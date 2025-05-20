using Domain.Component.Engravings;
using Domain.Component.Product.Attachments;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Component.Translation;

namespace Domain.Component.Product.CompletedSheath;

public class CompletedSheath : Product
{
    private CompletedSheath() : base()
    {
        
    }
    
    public CompletedSheath(
        Guid id,
        bool isActive,
        string imageUrl,
        Translations name,
        Translations title,
        Translations description,
        Translations metaTitle,
        Translations metaDescription,
        Sheath sheath, 
        SheathColor sheathColor, 
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
        metaDescription
    )
    {
        this.Sheath = sheath;
        this.SheathColor = sheathColor;
        this.Engravings = engravings;
        this.Attachments = attachments;
    }
    
    public Sheath Sheath { get; private set; }
    public SheathColor SheathColor { get; private set; }
    public List<Engraving>? Engravings { get; private set; }
    public List<Attachment>? Attachments { get; private set; }

    public override double GetPrice()
    {
        double price = 0;
        price += this.Sheath.GetPrice();
        price += this.SheathColor.GetPrice(this.Sheath.Type);
        price += this.Engravings?.Sum(x => x.GetPrice()) ?? 0;
        price += this.Attachments?.Sum(x => x.GetPrice()) ?? 0;
        
        return price;
    }
    
    public void Update(CompletedSheath completedSheath)
    {
        base.Update(completedSheath);
        this.Sheath = completedSheath.Sheath;
        this.SheathColor = completedSheath.SheathColor;
        this.Engravings = completedSheath.Engravings;
        this.Attachments = completedSheath.Attachments;
    }
}