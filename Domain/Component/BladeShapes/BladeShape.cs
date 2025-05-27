using System.ComponentModel.DataAnnotations;
using Domain.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding; 
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.Validators;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;

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
        FileEntity? bladeShapePhoto,
        double price,
        BladeCharacteristics bladeCharacteristics,
        Sheath? sheath,
        FileEntity bladeShapeModel,
        bool isActive
    )
    {
        this.Id = id;
        BladeShapeValidator.Validate(
            bladeShapePhoto, 
            price,
            bladeShapeModel
        );
        this.Type = type;
        this.Name = name;
        this.BladeShapePhoto = bladeShapePhoto;
        this.Price = price;
        this.BladeCharacteristics = bladeCharacteristics;
        this.BladeShapeModel = bladeShapeModel;
        this.IsActive = isActive;
        this.Sheath = sheath;
    }

    [BindNever]
    public Guid Id { get; private set; }

    public BladeShapeType Type { get; private set; }
    public Translations Name { get; private set; }

    [MaxLength(UrlMaxLength)]
    public FileEntity? BladeShapePhoto { get; private set; }
    public double Price { get; private set; }
    public BladeCharacteristics BladeCharacteristics { get; private set; }

    [MaxLength(UrlMaxLength)]
    public FileEntity BladeShapeModel { get; private set; }
    public bool IsActive { get; private set; } 
    public Sheath? Sheath { get; private set; } 

    public double GetPrice(double exchangerRate)
    {
        return this.Price / exchangerRate;
    }

    public void Update(BladeShape bladeShape)
    {
        BladeShapeValidator.Validate(
            bladeShape.BladeShapePhoto, 
            bladeShape.Price, 
            bladeShape.BladeShapeModel
        );
        this.Type = bladeShape.Type;
        this.Name = bladeShape.Name;
        this.BladeShapePhoto = bladeShape.BladeShapePhoto;
        this.Price = bladeShape.Price;
        this.BladeCharacteristics = bladeShape.BladeCharacteristics;
        this.BladeShapeModel = bladeShape.BladeShapeModel;
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