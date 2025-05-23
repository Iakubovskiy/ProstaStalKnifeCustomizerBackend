using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Handles;
using Domain.Component.Sheaths.Color;

namespace API.Presenters;

public class InitialDataPresenter
{
    public BladeShape? BladeShape { get; set; }
    
    public BladeCoatingColor? BladeCoatingColor { get; set; }
    
    public Handle? HandleColor { get; set; }
    
    public SheathColor? SheathColor { get; set; }
}