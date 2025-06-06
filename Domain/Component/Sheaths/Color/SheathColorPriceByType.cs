using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Component.BladeShapes.BladeShapeTypes;

namespace Domain.Component.Sheaths.Color;

public class SheathColorPriceByType
{
    private SheathColorPriceByType()
    {
        
    }
    
    public SheathColorPriceByType(
        BladeShapeType type, 
        SheathColor sheathColor, 
        double price
    )
    {
        this.Type = type;
        this.TypeId = type.Id;
        this.SheathColor = sheathColor;
        this.SheathColorId = sheathColor.Id;
        this.Price = price;
    }
    
    [ForeignKey(nameof(SheathColor))]
    public Guid SheathColorId { get; private set; }
    [ForeignKey(nameof(Type))]
    public Guid TypeId { get; private set; }
    public BladeShapeType Type { get; private set; }
    public SheathColor SheathColor { get; private set; }
    public double Price { get; private set; }

    public void UpdatePrice(double price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        this.Price = price;
    }
}