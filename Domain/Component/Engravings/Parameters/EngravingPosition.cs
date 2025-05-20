using Microsoft.EntityFrameworkCore;

namespace Domain.Component.Engravings.Parameters;

[Owned]
public class EngravingPosition
{
    private const double MinLocation = -10;
    private EngravingPosition()
    {
        
    }

    public EngravingPosition(
        double locationX, 
        double locationY, 
        double locationZ
    )
    {
        if (locationX < MinLocation || locationY < MinLocation || locationZ < MinLocation)
        {
            throw new ArgumentException("Not valid location");
        }
        this.LocationX = locationX;
        this.LocationY = locationY;
        this.LocationZ = locationZ;
    }
    
    public double LocationX { get; private set; }
    public double LocationY { get; private set; }
    public double LocationZ { get; private set; }
}