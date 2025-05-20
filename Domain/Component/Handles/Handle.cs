using Domain.Component.Handles.Validators;
using Domain.Component.Textures;
using Domain.Component.Translation;

namespace Domain.Component.Handles;

public class Handle : IComponent, IEntity
{
    private Handle()
    {
        
    }

    public Handle(
        Guid id, 
        Translations name, 
        string? colorCode, 
        bool isActive, 
        Translations material,
        Texture? texture,
        string? colorMapUrl,
        double price,
        string? handleModelUrl
    )
    {
        HandleValidator.Validate(colorCode, colorMapUrl);
        this.Id = id;
        this.Name = name;
        this.ColorCode = colorCode;
        this.IsActive = isActive;
        this.Material = material;
        this.Texture = texture;
        this.ColorMapUrl = colorMapUrl;
        this.Price = price;
        this.HandleModelUrl = handleModelUrl;
    }

    public Guid Id { get; private set; }
    public Translations Name { get; private set; }
    public string? ColorCode { get; private set; }
    public bool IsActive { get; set; }
    public Translations Material { get; private set; }
    public Texture? Texture { get; private set; }
    public string? ColorMapUrl { get; private set; }
    public double Price { get; private set; }
    
    public string? HandleModelUrl { get; private set; }
    
    public double GetPrice()
    {
        return this.Price;
    }

    public void Update(Handle handle)
    {
        HandleValidator.Validate(handle.ColorCode, handle.ColorMapUrl);
        this.Name = handle.Name;
        this.ColorCode = handle.ColorCode;
        this.IsActive = handle.IsActive;
        this.Material = handle.Material;
        this.Texture = handle.Texture;
        this.ColorMapUrl = handle.ColorMapUrl;
        this.Price = handle.Price;
    }
}