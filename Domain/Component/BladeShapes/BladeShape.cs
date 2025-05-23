using System.ComponentModel.DataAnnotations;
using Domain.Component.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding; 
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.Validators;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths;

namespace Domain.Component.BladeShapes; 

public class BladeShape : IEntity, IComponent, IUpdatable<BladeShape>
{
    private const int UrlMaxLength = 255;

    private BladeShape()
    {
        
    }

    public BladeShape(
        Guid id,
        BladeShapeType type,
        Translations name,
        string? bladeShapePhotoUrl,
        double price,
        BladeCharacteristics bladeCharacteristics,
        Sheath? sheath,
        string bladeShapeModelUrl,
        bool isActive
    )
    {
        this.Id = id;
        BladeShapeValidator.Validate(
            bladeShapePhotoUrl, 
            price,
            bladeShapeModelUrl
        );
        this.Type = type;
        this.Name = name;
        this.BladeShapePhotoUrl = bladeShapePhotoUrl;
        this.Price = price;
        this.BladeCharacteristics = bladeCharacteristics;
        this.BladeShapeModelUrl = bladeShapeModelUrl;
        this.IsActive = isActive;
        this.Sheath = sheath;
    }

    [BindNever]
    public Guid Id { get; private set; }

    public BladeShapeType Type { get; private set; }
    public Translations Name { get; private set; }

    [MaxLength(UrlMaxLength)]
    public string? BladeShapePhotoUrl { get; private set; }
    public double Price { get; private set; }
    public BladeCharacteristics BladeCharacteristics { get; private set; }

    [MaxLength(UrlMaxLength)]
    public string BladeShapeModelUrl { get; private set; }
    public bool IsActive { get; private set; } 
    public Sheath? Sheath { get; private set; } 

    public double GetPrice(double exchangerRate)
    {
        return this.Price / exchangerRate;
    }

    public void Update(BladeShape bladeShape)
    {
        BladeShapeValidator.Validate(
            bladeShape.BladeShapePhotoUrl, 
            bladeShape.Price, 
            bladeShape.BladeShapeModelUrl
        );
        this.Type = bladeShape.Type;
        this.Name = bladeShape.Name;
        this.BladeShapePhotoUrl = bladeShape.BladeShapePhotoUrl;
        this.Price = bladeShape.Price;
        this.BladeCharacteristics = bladeShape.BladeCharacteristics;
        this.BladeShapeModelUrl = bladeShape.BladeShapeModelUrl;
        this.IsActive = bladeShape.IsActive;
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