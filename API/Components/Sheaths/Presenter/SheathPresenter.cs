using Application.Components.Prices;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;

namespace API.Components.Sheaths.Presenter;

public class SheathPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public FileEntity? Model { get; set; }
    public BladeShapeType Type { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }

    public static async Task<SheathPresenter> Present(
        Sheath sheath, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        return new SheathPresenter
        {
            Id = sheath.Id,
            Name = sheath.Name.GetTranslation(locale),
            Model = sheath.Model,
            Price = await getComponentPrice.GetPrice(sheath, currency),
            IsActive = sheath.IsActive,
            Type = sheath.Type,
        };
    }
    
    public static async Task<SheathPresenter> PresentWithTranslations(
        Sheath sheath, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        SheathPresenter presenter = await Present(sheath, locale, currency, getComponentPrice);
        presenter.Names = sheath.Name.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<SheathPresenter>> PresentList(
        List<Sheath> sheaths, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        var sheathPresenters = new List<SheathPresenter>();
        foreach (Sheath sheath in sheaths)
        {
            SheathPresenter sheathPresenter = await Present(sheath, locale, currency, getComponentPrice);
            sheathPresenters.Add(sheathPresenter);
        }
        return sheathPresenters;
    }
}