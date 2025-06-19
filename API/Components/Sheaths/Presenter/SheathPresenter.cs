using Application.Components.Prices;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;

namespace API.Components.Sheaths.Presenter;

public class SheathPresenter
{
    private readonly IGetComponentPrice _getComponentPrice;

    public SheathPresenter(IGetComponentPrice getComponentPrice)
    {
        this._getComponentPrice = getComponentPrice;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public FileEntity? Model { get; set; }
    public BladeShapeType ShapeType { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }

    public async Task<SheathPresenter> Present(Sheath sheath, string locale, string currency)
    {
        this.Id = sheath.Id;
        this.Name = sheath.Name.GetTranslation(locale);
        this.Model = sheath.Model;
        this.Price = await this._getComponentPrice.GetPrice(sheath,currency);
        this.IsActive = sheath.IsActive;
        
        return this;
    }

    public async Task<List<SheathPresenter>> PresentList(List<Sheath> sheaths, string locale, string currency)
    {
        List<SheathPresenter> sheathPresenters = new List<SheathPresenter>();
        foreach (Sheath sheath in sheaths)
        {
            SheathPresenter sheathPresenter = new SheathPresenter(this._getComponentPrice); 
            await Present(sheath, locale, currency);
            sheathPresenters.Add(sheathPresenter);
        }
        return sheathPresenters;
    }
}