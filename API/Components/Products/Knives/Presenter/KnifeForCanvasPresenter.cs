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

    public static Task<KnifeForCanvasPresenter> Present(Knife knife, string locale)
    {
        var presenter = new KnifeForCanvasPresenter
        {
            BladeCoatingColor = BladeCoatingColorPresenterForCanvas.Present(knife.Color),
            BladeShape = BladeShapePresenterForCanvas.Present(knife.Blade, locale)
        };

        if (knife.Handle != null)
        {
            presenter.HandleColor = HandleColorPresenterForCanvas.Present(knife.Handle);
        }
        
        if (knife.SheathColor != null)
        {
            presenter.SheathColor = SheathColorPresenterForCanvas.Present(knife.SheathColor);
        }

        if (knife.Attachments != null)
        {
            presenter.Attachments = new List<AttachmentPresenterForCanvas>();
            foreach (Attachment attachment in knife.Attachments)
            {
                presenter.Attachments.Add(AttachmentPresenterForCanvas.Present(attachment));
            }
        }
        
        if (knife.Engravings != null)
        {
            presenter.Engravings = new List<EngravingPresenterForCanvas>();
            foreach (Engraving engraving in knife.Engravings)
            {
                presenter.Engravings.Add(EngravingPresenterForCanvas.Present(engraving));
            }
        }
        
        return Task.FromResult(presenter);
    }
}