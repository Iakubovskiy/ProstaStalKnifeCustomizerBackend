using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths;
using Microsoft.AspNetCore.Http;

namespace Application.Components.SimpleComponents.BladeShapes;

public class BladeShapeDto
{
    public BladeShapeDto(
        Guid typeId,
        string nameJson,
        double price,
        double totalLength,
        double bladeLength,
        double bladeWidth,
        double bladeWeight,
        double sharpeningAngle,
        double rockwellHardnessUnits,
        IFormFile bladeShapePhoto,
        IFormFile bladeShapeModel,
        bool isActive,
        Guid? sheathId
    )
    {
        this.TypeId = typeId;
        this.NameJson = nameJson;
        this.Price = price;
        this.TotalLength = totalLength;
        this.BladeLength = bladeLength;
        this.BladeWidth = bladeWidth;
        this.BladeWeight = bladeWeight;
        this.SharpeningAngle = sharpeningAngle;
        this.RockwellHardnessUnits = rockwellHardnessUnits;
        this.BladeShapePhoto = bladeShapePhoto;
        this.BladeShapeModel = bladeShapeModel;
        this.IsActive = isActive;
        this.SheathId = sheathId;
    }
    
    public Guid TypeId { get; private set; }
    public string NameJson { get; private set; }
    public double Price { get; private set; }
    public double TotalLength { get; private set; }
    public double BladeLength { get; private set; }
    public double BladeWidth { get; private set; }
    public double BladeWeight { get; private set; }
    public double SharpeningAngle { get; private set; }
    public double RockwellHardnessUnits  { get; private set; }
    public IFormFile BladeShapePhoto { get; private set; }
    public IFormFile BladeShapeModel { get; private set; }
    public bool IsActive { get; private set; }
    public Guid? SheathId { get; private set; }
}