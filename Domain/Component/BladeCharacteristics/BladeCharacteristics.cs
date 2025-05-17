using Microsoft.EntityFrameworkCore;

namespace Domain.Component.BladeCharacteristics;

[Owned]
public class BladeCharacteristics
{
    public double TotalLength { get; private set; }
    public double BladeLength { get; private set; }
    public double BladeWidth { get; private set; }
    public double BladeWeight { get; private set; }
    public double SharpeningAngle { get; private set; }
    public double RockwellHardnessUnits  { get; private set; }
}