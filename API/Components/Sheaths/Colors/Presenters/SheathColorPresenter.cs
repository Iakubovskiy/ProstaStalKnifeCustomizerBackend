using Application.Currencies;
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Files;

namespace API.Components.Sheaths.Colors.Presenters;

public class SheathColorPresenter
{
    public Guid Id { get; set; }
    public string Color { get; set; }
    public Dictionary<string, string> Colors { get; set; }
    public string ColorCode { get; set; }
    public bool IsActive { get; set; }
    public string Material { get; set; }
    public Dictionary<string, string> Materials { get; set; }
    public string EngravingColorCode { get; set; }
    public Texture? Texture { get; set; }
    public FileEntity? ColorMap { get; set; }
    public List<SheathColorPriceByTypePresenter> Prices { get; set; }

    public static async Task<SheathColorPresenter> Present(
        SheathColor color, 
        string locale, 
        string currency, 
        IPriceService priceService)
    {
        return new SheathColorPresenter
        {
            Id = color.Id,
            Color = color.Color.GetTranslation(locale),
            ColorCode = color.ColorCode,
            Material = color.Material.GetTranslation(locale),
            EngravingColorCode = color.EngravingColorCode,
            Texture = color.Texture,
            ColorMap = color.ColorMap,
            IsActive = color.IsActive,
            Prices = await SheathColorPriceByTypePresenter.PresentList(color.Prices, locale, currency, priceService)
        };
    }

    public static async Task<SheathColorPresenter> PresentWithTranslations(
        SheathColor color, 
        string locale, 
        string currency, 
        IPriceService priceService)
    {
        SheathColorPresenter presenter = await Present(color, locale, currency, priceService);
        presenter.Colors = color.Color.TranslationDictionary;
        presenter.Materials = color.Material.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<SheathColorPresenter>> PresentList(
        List<SheathColor> colors, 
        string locale, 
        string currency, 
        IPriceService priceService)
    {
        var presenters = new List<SheathColorPresenter>();
        foreach (SheathColor sheathColor in colors)
        {
            SheathColorPresenter presenter = await Present(sheathColor, locale, currency, priceService);
            presenters.Add(presenter);
        }
        
        return presenters;
    }
}