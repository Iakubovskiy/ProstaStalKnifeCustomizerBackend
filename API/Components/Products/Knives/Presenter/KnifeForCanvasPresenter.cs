using API.Components.Products.Knives.Presenter.ComponentsForCanvas;
using Application.Currencies;
using Domain.Component.Engravings;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Knife;

namespace API.Components.Products.Knives.Presenter;

public class KnifeForCanvasPresenter
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public List<AttachmentPresenterForCanvas>? Attachments { get; set; }
    public BladeCoatingColorPresenterForCanvas BladeCoatingColor { get; set; }
    public BladeShapePresenterForCanvas BladeShape { get; set; }
    public List<EngravingPresenterForCanvas>? Engravings { get; set; }
    public HandleColorPresenterForCanvas? HandleColor { get; set; }
    public SheathColorPresenterForCanvas? SheathColor { get; set; }

    public static async Task<KnifeForCanvasPresenter> Present(
        Knife knife, 
        string locale, 
        string currency, 
        IPriceService priceService
    )
    {
        var presenter = new KnifeForCanvasPresenter
        {
            Id = knife.Id,
            IsActive = knife.IsActive,
            BladeCoatingColor = await BladeCoatingColorPresenterForCanvas
                .Present(knife.Color, locale, currency, priceService),
            BladeShape = await BladeShapePresenterForCanvas
                .Present(knife.Blade, locale, currency, priceService),
        };

        if (knife.Handle != null)
        {
            presenter.HandleColor = await HandleColorPresenterForCanvas
                .Present(knife.Handle, locale, currency, priceService);
        }
        
        if (knife.SheathColor != null)
        {
            presenter.SheathColor = await SheathColorPresenterForCanvas
                .Present(knife.SheathColor, locale, currency, priceService);
        }

        if (knife.Attachments != null)
        {
            presenter.Attachments = new List<AttachmentPresenterForCanvas>();
            foreach (Attachment attachment in knife.Attachments)
            {
                presenter.Attachments.Add(await AttachmentPresenterForCanvas
                    .Present(attachment, locale, currency, priceService));
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
        
        return presenter;
    }
}