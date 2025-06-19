using Microsoft.AspNetCore.Http;

namespace Application.Components.SimpleComponents.BladeShapes;

public class BladeShapeDto
{
    public Guid? Id { get; set; }
    public Guid TypeId { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public double Price { get; set; }
    public double TotalLength { get; set; }
    public double BladeLength { get; set; }
    public double BladeWidth { get; set; }
    public double BladeWeight { get; set; }
    public double SharpeningAngle { get; set; }
    public double RockwellHardnessUnits  { get; set; }
    public Guid BladeShapePhotoId { get; set; }
    public Guid BladeShapeModelId { get; set; }
    public bool IsActive { get; set; }
    public Guid? SheathId { get; set; }
}