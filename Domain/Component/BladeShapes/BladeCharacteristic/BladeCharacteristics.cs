using Microsoft.EntityFrameworkCore;

namespace Domain.Component.BladeShapes.BladeCharacteristic;

[Owned]
public class BladeCharacteristics
{
    private const double MinSharpeningAngle = 10.0;
    private const double MaxSharpeningAngle = 60.0;
    private const double MinRockwellHardness = 20.0;
    private const double MaxRockwellHardness = 70.0;
    
    private BladeCharacteristics()
    {
        
    }

    public BladeCharacteristics(
        double totalLength,
        double bladeLength,
        double bladeWidth,
        double bladeWeight,
        double sharpeningAngle,
        double rockwellHardnessUnits
    )
    {
        if (totalLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalLength), "Total length must be a positive value.");
        if (bladeLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(bladeLength), "Blade length must be a positive value.");
        if (bladeWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(bladeWidth), "Blade width must be a positive value.");
        if (bladeWeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(bladeWeight), "Blade weight must be a positive value.");

        if (sharpeningAngle is < MinSharpeningAngle or > MaxSharpeningAngle)
            throw new ArgumentOutOfRangeException(nameof(sharpeningAngle), $"Sharpening angle must be between {MinSharpeningAngle} and {MaxSharpeningAngle} degrees.");
        
        if (rockwellHardnessUnits is < MinRockwellHardness or > MaxRockwellHardness)
            throw new ArgumentOutOfRangeException(nameof(rockwellHardnessUnits), $"Rockwell hardness units must be between {MinRockwellHardness} and {MaxRockwellHardness} HRC.");

        if (totalLength < bladeLength)
            throw new ArgumentException("Total length cannot be less than blade length.", nameof(totalLength));

        this.TotalLength = totalLength;
        this.BladeLength = bladeLength;
        this.BladeWidth = bladeWidth;
        this.BladeWeight = bladeWeight;
        this.SharpeningAngle = sharpeningAngle;
        this.RockwellHardnessUnits = rockwellHardnessUnits;
    }

    
    public double TotalLength { get; private set; }
    public double BladeLength { get; private set; }
    public double BladeWidth { get; private set; }
    public double BladeWeight { get; private set; }
    public double SharpeningAngle { get; private set; }
    public double RockwellHardnessUnits  { get; private set; }
}