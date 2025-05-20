using System.ComponentModel.DataAnnotations;
using Domain.Component.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding; 
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.Validators;
using Domain.Component.BladeShapeTypes;

namespace Domain.Component.BladeShapes; 

public class BladeShape : IEntity, IComponent
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
        string bladeShapeModelUrl,
        string sheathModelUrl,
        bool isActive
    )
    {
        this.Id = id;
        BladeShapeValidator.Validate(
            bladeShapePhotoUrl, 
            price,
            bladeShapeModelUrl, 
            sheathModelUrl
        );
        this.Type = type;
        this.Name = name;
        this.BladeShapePhotoUrl = bladeShapePhotoUrl;
        this.Price = price;
        this.BladeCharacteristics = bladeCharacteristics;
        this.BladeShapeModelUrl = bladeShapeModelUrl;
        this.SheathModelUrl = sheathModelUrl;
        this.IsActive = isActive;
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

    [MaxLength(UrlMaxLength)]
    public string SheathModelUrl { get; private set; }

    public bool IsActive { get; private set; } 

    public double GetPrice()
    {
        return this.Price;
    }

    public void Update(BladeShape bladeShape)
    {
        BladeShapeValidator.Validate(
            bladeShape.BladeShapePhotoUrl, 
            bladeShape.Price, 
            bladeShape.BladeShapeModelUrl, 
            bladeShape.SheathModelUrl
        );
        this.Type = bladeShape.Type;
        this.Name = bladeShape.Name;
        this.BladeShapePhotoUrl = bladeShape.BladeShapePhotoUrl;
        this.Price = bladeShape.Price;
        this.BladeCharacteristics = bladeShape.BladeCharacteristics;
        this.BladeShapeModelUrl = bladeShape.BladeShapeModelUrl;
        this.SheathModelUrl = bladeShape.SheathModelUrl;
        this.IsActive = bladeShape.IsActive;
    }

    public void ChangeActive(bool isActive)
    {
        this.IsActive = isActive;
    }
}