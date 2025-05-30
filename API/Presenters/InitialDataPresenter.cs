using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Handles;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;

namespace API.Presenters;

public class InitialDataPresenter
{
    public InitialDataPresenter(
        BladeShape? bladeShape, 
        BladeCoatingColor? bladeCoatingColor, 
        Handle? handleColor, 
        Sheath? sheath, 
        SheathColor? sheathColor
    )
    {
        this.BladeShape = bladeShape;
        this.BladeCoatingColor = bladeCoatingColor;
        this.HandleColor = handleColor;
        this.Sheath = sheath;
        this.SheathColor = sheathColor;
    }

    public BladeShape? BladeShape { get; private set; }
    
    public BladeCoatingColor? BladeCoatingColor { get; private set; }
    
    public Handle? HandleColor { get; private set; }
    
    public Sheath? Sheath { get; private set; }
    public SheathColor? SheathColor { get; private set; }
}