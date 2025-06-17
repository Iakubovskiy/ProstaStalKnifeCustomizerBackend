using API.Components.Products.Knives.Presenter.ComponentsForCanvas;
using Domain.Component.Engravings;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Knife;

namespace API.Components.Products.Knives.Presenter;

public class KnifeForCanvasPresenter
{
    public List<AttachmentPresenterForCanvas>? Attachments { get; set; }
    public BladeCoatingColorPresenterForCanvas BladeCoatingColor { get; set; }
    public BladeShapePresenterForCanvas BladeShape { get; set; }
    public List<EngravingPresenterForCanvas>? Engravings { get; set; }
    public HandleColorPresenterForCanvas? HandleColor { get; set; }
    public SheathColorPresenterForCanvas? SheathColor { get; set; }
    public async Task<KnifeForCanvasPresenter> Present(Knife knife, string locale)
    {
        this.BladeCoatingColor = new BladeCoatingColorPresenterForCanvas().Present(knife.Color);
        this.BladeShape = new BladeShapePresenterForCanvas().Present(knife.Blade, locale);
        if (knife.Handle != null)
        {
            this.HandleColor = new HandleColorPresenterForCanvas().Present(knife.Handle);
        }
        if (knife.SheathColor != null)
        {
            this.SheathColor = new SheathColorPresenterForCanvas().Present(knife.SheathColor);
        }

        if (knife.Attachments != null)
        {
            this.Attachments = new List<AttachmentPresenterForCanvas>();
            foreach (Attachment attachment in knife.Attachments)
            {
                this.Attachments.Add(new AttachmentPresenterForCanvas().Present(attachment));
            }
        }
        
        if (knife.Engravings != null)
        {
            this.Engravings = new List<EngravingPresenterForCanvas>();
            foreach (Engraving engraving in knife.Engravings)
            {
                this.Engravings.Add(new EngravingPresenterForCanvas().Present(engraving));
            }
        }
        
        return this;
    }
}