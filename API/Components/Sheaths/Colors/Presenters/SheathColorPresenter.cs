using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Files;

namespace API.Components.Sheaths.Colors.Presenters;

public class SheathColorPresenter
{
    private readonly SheathColorPriceByTypePresenter _sheathColorPriceByTypePresenter;

    public SheathColorPresenter(SheathColorPriceByTypePresenter priceByTypePresenter)
    {
        this._sheathColorPriceByTypePresenter = priceByTypePresenter;
    }
    
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

    public async Task<SheathColorPresenter> Present(SheathColor color, string locale, string currency)
    {
        this.Id = color.Id;
        this.Color = color.Color.GetTranslation(locale);
        this.ColorCode = color.ColorCode;
        this.Material = color.Material.GetTranslation(locale);
        this.EngravingColorCode = color.EngravingColorCode;
        this.Texture = color.Texture;
        this.ColorMap = color.ColorMap;
        this.Material = color.Material.GetTranslation(locale);
        this.IsActive = color.IsActive;
        this.Prices = await this._sheathColorPriceByTypePresenter.PresentList(color.Prices, locale, currency);
        
        return this;
    }

    public async Task<SheathColorPresenter> PresentWithTranslations(SheathColor color, string locale, string currency)
    {
        await this.Present(color, locale, currency);
        this.Colors = color.Color.TranslationDictionary;
        this.Materials = color.Material.TranslationDictionary;
        return this;
    }

    public async Task<List<SheathColorPresenter>> PresentList(List<SheathColor> colors, string locale, string currency)
    {
        List<SheathColorPresenter> presenters = new List<SheathColorPresenter>();
        foreach (SheathColor sheathColor in colors)
        {
            SheathColorPresenter presenter = new SheathColorPresenter(this._sheathColorPriceByTypePresenter);
            await Present(sheathColor, locale, currency);
            presenters.Add(presenter);
        }
        
        return presenters;
    }
}