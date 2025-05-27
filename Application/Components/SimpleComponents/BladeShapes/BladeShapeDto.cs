using Microsoft.AspNetCore.Http;

namespace Application.Components.SimpleComponents.BladeShapes;

public class BladeShapeDto
{
    public BladeShapeDto(
        Guid typeId,
        Dictionary<string, string> name,
        double price,
        double totalLength,
        double bladeLength,
        double bladeWidth,
        double bladeWeight,
        double sharpeningAngle,
        double rockwellHardnessUnits,
        Guid bladeShapePhotoId,
        Guid bladeShapeModelId,
        bool isActive,
        Guid? sheathId
    )
    {
        this.TypeId = typeId;
        this.Name = name;
        this.Price = price;
        this.TotalLength = totalLength;
        this.BladeLength = bladeLength;
        this.BladeWidth = bladeWidth;
        this.BladeWeight = bladeWeight;
        this.SharpeningAngle = sharpeningAngle;
        this.RockwellHardnessUnits = rockwellHardnessUnits;
        this.BladeShapePhotoId = bladeShapePhotoId;
        this.BladeShapeModelId = bladeShapeModelId;
        this.IsActive = isActive;
        this.SheathId = sheathId;
    }
    
    public Guid TypeId { get; private set; }
    public Dictionary<string, string> Name { get; private set; }
    public double Price { get; private set; }
    public double TotalLength { get; private set; }
    public double BladeLength { get; private set; }
    public double BladeWidth { get; private set; }
    public double BladeWeight { get; private set; }
    public double SharpeningAngle { get; private set; }
    public double RockwellHardnessUnits  { get; private set; }
    public Guid BladeShapePhotoId { get; private set; }
    public Guid BladeShapeModelId { get; private set; }
    public bool IsActive { get; private set; }
    public Guid? SheathId { get; private set; }
}