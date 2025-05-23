using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths.Color.Validators;
using Domain.Component.Textures;
using Domain.Component.Translation;

namespace Domain.Component.Sheaths.Color;

public class SheathColor : IEntity, IComponentWithTypeDependency, IUpdatable<SheathColor>, ITextured
{
    private SheathColor()
    {
        
    }

    public SheathColor(
        Guid id, 
        Translations color, 
        bool isActive,
        List<SheathColorPriceByType> prices,
        string material, 
        string engravingColorCode, 
        Texture? texture, 
        string? colorMapUrl
    )
    {
        SheathColorValidator.Validate(prices, material, engravingColorCode, colorMapUrl);
        this.Id = id;
        this.Color = color;
        this.IsActive = isActive;
        this.Material = material;
        this.EngravingColorCode = engravingColorCode;
        this.Texture = texture;
        this.Prices = prices;
        this.ColorMapUrl = colorMapUrl;
    }
    
    public Guid Id { get; private set;}
    public Translations Color { get; private set; }
    public bool IsActive { get; private set; }
    public string Material { get; private set; }
    public string EngravingColorCode { get; private set; }
    public Texture? Texture { get; private set; }
    public string? ColorMapUrl { get; private set; }
    public List<SheathColorPriceByType> Prices { get; private set; }

    public double GetPrice(BladeShapeType type, double exchangerRate)
    {
        return this.Prices.First(p => p.Type == type).Price / exchangerRate;
    }
    
    public void Update(SheathColor sheathColor)
    {
        SheathColorValidator.Validate(sheathColor.Prices, sheathColor.Material, sheathColor.EngravingColorCode, sheathColor.ColorMapUrl);
        this.Color = sheathColor.Color;
        this.IsActive = sheathColor.IsActive;
        this.Material = sheathColor.Material;
        this.EngravingColorCode = sheathColor.EngravingColorCode;
        this.Texture = sheathColor.Texture;
        this.ColorMapUrl = sheathColor.ColorMapUrl;
        this.Prices = sheathColor.Prices;
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