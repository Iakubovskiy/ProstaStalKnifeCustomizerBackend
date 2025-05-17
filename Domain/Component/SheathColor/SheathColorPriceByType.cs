namespace Domain.Component.SheathColor;

public class SheathColorPriceByType
{
    public BladeShapeType.BladeShapeType Type { get; set; }
    public SheathColor SheathColor { get; set; }
    public double Price { get; set; }

    private SheathColorPriceByType()
    {
        
    }
    
    public SheathColorPriceByType(BladeShapeType.BladeShapeType type, SheathColor sheathColor, double price)
    {
        Type = type;
        SheathColor = sheathColor;
        Price = price;
    }
}