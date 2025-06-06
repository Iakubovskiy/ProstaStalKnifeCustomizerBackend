using Microsoft.EntityFrameworkCore;

namespace Domain.Component.Engravings.Parameters;

[Owned]
public class EngravingRotation
{
    private const double MinRotation = -360;
    private EngravingRotation()
    {
        
    }

    public EngravingRotation(
        double rotationX, 
        double rotationY, 
        double rotationZ
    )
    {
        if (rotationX < MinRotation || rotationY < MinRotation || rotationZ < MinRotation)
        {
            throw new ArgumentException("Rotation must be greater than -360");
        }
        this.RotationX = rotationX;
        this.RotationY = rotationY;
        this.RotationZ = rotationZ;
    }
    
    public double RotationX { get; private set; }
    public double RotationY { get; private set; }
    public double RotationZ { get; private set; }
}