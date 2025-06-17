using Domain.Component.Engravings;
using Domain.Component.Product.Attachments;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Files;
using Domain.Translation;

namespace Domain.Component.Product.CompletedSheath;

public class CompletedSheath : Product, IUpdatable<CompletedSheath>
{
    private CompletedSheath() : base()
    {
        
    }
    
    public CompletedSheath(
        Guid id,
        bool isActive,
        FileEntity image,
        Translations name,
        Translations title,
        Translations description,
        Translations metaTitle,
        Translations metaDescription,
        List<ProductTag> tags,
        Sheath sheath, 
        SheathColor sheathColor, 
        List<Engraving>? engravings, 
        List<Attachment>? attachments
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
        this.Sheath = sheath;
        this.SheathColor = sheathColor;
        this.Engravings = engravings;
        this.Attachments = attachments;
    }
    
    public Sheath Sheath { get; private set; }
    public SheathColor SheathColor { get; private set; }
    public List<Engraving>? Engravings { get; private set; }
    public List<Attachment>? Attachments { get; private set; }
    public double TotalPriceInUah { get; private set; }

    public override double GetPrice(double exchangerRate)
    {
        return Math.Ceiling(TotalPriceInUah / exchangerRate);
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