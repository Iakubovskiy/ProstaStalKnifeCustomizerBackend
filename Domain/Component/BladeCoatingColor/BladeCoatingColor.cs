using Domain.Component.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Domain.Component.BladeCoatingColor;

public class BladeCoatingColor : IEntity, IComponent
{
    private BladeCoatingColor()
    {
        
    }
    
    
    public BladeCoatingColor(Guid id, Translations type, double price, Translations color, string? colorCode, string engravingColorCode, bool isActive, Texture.Texture? texture, string? colorMapUrl)
    {
        Id = id;
        Type = type;
        Price = price;
        Color = color;
        ColorCode = colorCode;
        EngravingColorCode = engravingColorCode;
        IsActive = isActive;
        Texture = Texture;
        ColorMapUrl = colorMapUrl;
    }
    [BindNever]
    public Guid Id { get; }
    public Translations Type { get; private set; }
    public double Price { get; private set; }
    public Translations Color { get; private set; }
    public string? ColorCode { get; private set; }
    public string EngravingColorCode { get; private set; }
    public bool IsActive { get; set; }
    public Texture.Texture? Texture { get; private set; }
    public string? ColorMapUrl { get; private set; }

    public double GetPrice()
    {
        return this.Price;
    }
    
    public void Update(BladeCoatingColor bladeCoatingColor)
    {
            
    }
}