using Domain.Component.BladeCoatingColors.Validators;
using Domain.Component.Textures;
using Domain.Component.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Domain.Component.BladeCoatingColors;

public class BladeCoatingColor : IEntity, IComponent
{
    private BladeCoatingColor()
    {
        
    }
    
    public BladeCoatingColor(
        Guid id, 
        Translations type, 
        double price, 
        Translations color, 
        string? colorCode, 
        string engravingColorCode, 
        bool isActive, 
        Texture? texture, 
        string? colorMapUrl
    )
    {
        this.Id = id;
        this.Type = type;
        this.Price = price;
        this.Color = color;
        this.ColorCode = colorCode;
        this.EngravingColorCode = engravingColorCode;
        this.Texture = texture;
        this.ColorMapUrl = colorMapUrl;
        this.IsActive = isActive;
    }
    [BindNever]
    public Guid Id { get; private set; }
    public Translations Type { get; private set; }
    public double Price { get; private set; }
    public Translations Color { get; private set; }
    public string? ColorCode { get; private set; }
    public string EngravingColorCode { get; private set; }
    public bool IsActive { get; private set; }
    public Texture? Texture { get; private set; }
    public string? ColorMapUrl { get; private set; }

    public double GetPrice()
    {
        return this.Price;
    }
    
    public void Update(BladeCoatingColor bladeCoatingColor)
    {
        if (bladeCoatingColor == null)
        {
            throw new ArgumentNullException(nameof(bladeCoatingColor));
        }
        BladeCoatingColorValidator.Validate(
            bladeCoatingColor.Price, 
            bladeCoatingColor.ColorCode, 
            bladeCoatingColor.EngravingColorCode, 
            bladeCoatingColor.ColorMapUrl
        );
        
        this.Type = bladeCoatingColor.Type;
        this.Price = bladeCoatingColor.Price;
        this.Color = bladeCoatingColor.Color;
        this.ColorCode = bladeCoatingColor.ColorCode;
        this.EngravingColorCode = bladeCoatingColor.EngravingColorCode;
        this.Texture = bladeCoatingColor.Texture;
        this.ColorMapUrl = bladeCoatingColor.ColorMapUrl;
        this.IsActive = bladeCoatingColor.IsActive;
    }
    
    public void ChangeActive(bool isActive)
    {
        this.IsActive = isActive;
    }

}