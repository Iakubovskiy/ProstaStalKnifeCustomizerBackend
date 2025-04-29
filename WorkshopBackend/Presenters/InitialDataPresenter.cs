using WorkshopBackend.Models;

namespace WorkshopBackend.Presenters;

public class InitialDataPresenter
{
    public BladeShape? BladeShape { get; set; }
    
    public BladeCoatingColor? BladeCoatingColor { get; set; }
    
    public HandleColor? HandleColor { get; set; }
    
    public SheathColor? SheathColor { get; set; }
}