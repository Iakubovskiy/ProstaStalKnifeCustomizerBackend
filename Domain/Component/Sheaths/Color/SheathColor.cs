using System.ComponentModel.DataAnnotations;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths.Color.Validators;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;

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
        string? colorCode,
        List<SheathColorPriceByType> prices,
        Translations material, 
        string engravingColorCode, 
        Texture? texture, 
        FileEntity? colorMap
    )
    {
        SheathColorValidator.Validate(prices, material, engravingColorCode);
        this.Id = id;
        this.Color = color;
        this.IsActive = isActive;
        this.Material = material;
        this.EngravingColorCode = engravingColorCode;
        this.Texture = texture;
        this.Prices = prices;
        this.ColorMap = colorMap;
        this.ColorCode = colorCode;
    }
    
    public Guid Id { get; private set;}
    public Translations Color { get; private set; }
    public bool IsActive { get; private set; }
    [MaxLength(10)]
    public string? ColorCode { get; private set; }
    public Translations Material { get; private set; }
    [MaxLength(10)]
    public string EngravingColorCode { get; private set; }
    public Texture? Texture { get; private set; }
    [MaxLength(255)]
    public FileEntity? ColorMap { get; private set; }
    public List<SheathColorPriceByType> Prices { get; private set; }

    public double GetPrice(BladeShapeType type, double exchangerRate)
    {
        return Math.Ceiling(this.Prices.First(p => p.TypeId == type.Id).Price / exchangerRate);
    }
    
    public void Update(SheathColor sheathColor)
    {
        SheathColorValidator.Validate(sheathColor.Prices, sheathColor.Material, sheathColor.EngravingColorCode);
        this.Color = sheathColor.Color;
        this.IsActive = sheathColor.IsActive;
        this.Material = sheathColor.Material;
        this.EngravingColorCode = sheathColor.EngravingColorCode;
        this.Texture = sheathColor.Texture;
        this.ColorMap = sheathColor.ColorMap;
        this.Prices = sheathColor.Prices;
        this.ColorCode = sheathColor.ColorCode;
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