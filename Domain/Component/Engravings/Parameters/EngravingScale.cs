using Microsoft.EntityFrameworkCore;

namespace Domain.Component.Engravings.Parameters;

[Owned]
public class EngravingScale
{
    private const double MinScale = 0.0001;
    private EngravingScale()
    {
        
    }

    public EngravingScale(
        double scaleX, 
        double scaleY, 
        double scaleZ
    )
    {
        if (scaleX < MinScale || scaleY < MinScale || scaleZ < MinScale)
        {
            throw new ArgumentException("Scale must be greater than 0");
        }
        this.ScaleX = scaleX;
        this.ScaleY = scaleY;
        this.ScaleZ = scaleZ;
    }
    
    public double ScaleX { get; private set; }
    public double ScaleY { get; private set; }
    public double ScaleZ { get; private set; }
}