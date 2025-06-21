using Application.Components.Prices;
using Domain.Component.BladeCoatingColors;
using Domain.Component.Textures;
using Domain.Files;

namespace API.Components.BladeCoatingColors.Presenters;

public class BladeCoatingColorPresenter
{
    public Guid Id { get; set; }
    public double Price { get; set; }
    public string Color { get; set; }
    public Dictionary<string, string> Colors { get; set; }
    public string Type { get; set; }
    public Dictionary<string, string> Types { get; set; }
    public string? ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public bool IsActive { get; set; }
    public Texture? Texture { get; set; }
    public FileEntity? ColorMap { get; set; }

    public static async Task<BladeCoatingColorPresenter> Present(
        BladeCoatingColor color, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        return new BladeCoatingColorPresenter
        {
            Id = color.Id,
            Price = await getComponentPrice.GetPrice(color, currency),
            Color = color.Color.GetTranslation(locale),
            ColorCode = color.ColorCode,
            EngravingColorCode = color.EngravingColorCode,
            IsActive = color.IsActive,
            Texture = color.Texture,
            ColorMap = color.ColorMap,
            Type = color.Type.GetTranslation(locale),
        };
    }

    public static async Task<BladeCoatingColorPresenter> PresentWithTranslations(
        BladeCoatingColor color, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        BladeCoatingColorPresenter presenter = await Present(color, locale, currency, getComponentPrice);
        presenter.Colors = color.Color.TranslationDictionary;
        presenter.Types = color.Type.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<BladeCoatingColorPresenter>> PresentList(
        List<BladeCoatingColor> colors, 
        string locale,
        string currency,
        IGetComponentPrice getComponentPrice)
    {
        var colorsPresenters = new List<BladeCoatingColorPresenter>();
        foreach (BladeCoatingColor bladeCoatingColor in colors)
        {
            BladeCoatingColorPresenter colorsPresenter = await Present(bladeCoatingColor, locale, currency, getComponentPrice);
            colorsPresenters.Add(colorsPresenter);
        }
        return colorsPresenters;
    }
}