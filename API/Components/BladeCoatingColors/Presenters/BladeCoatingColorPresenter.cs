using Application.Components.Prices;
using Domain.Component.BladeCoatingColors;
using Domain.Component.Textures;
using Domain.Files;

namespace API.Components.BladeCoatingColors.Presenters;

public class BladeCoatingColorPresenter
{
    private readonly IGetComponentPrice _getComponentPrice;
    public BladeCoatingColorPresenter(
        IGetComponentPrice getComponentPrice
    )
    {
        this._getComponentPrice = getComponentPrice;    
    }
    
    public Guid Id { get; set; }
    public double Price { get; set; }
    public string Color { get; set; }
    public string? ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public bool IsActive { get; set; }
    public Texture? Texture { get; set; }
    public FileEntity? ColorMap { get; set; }

    public async Task<BladeCoatingColorPresenter> Present(BladeCoatingColor color, string locale, string currency)
    {
        this.Id = color.Id;
        this.Price = await this._getComponentPrice.GetPrice(color, currency);
        this.Color = color.Color.TranslationDictionary[locale];
        this.ColorCode = color.ColorCode;
        this.EngravingColorCode = color.EngravingColorCode;
        this.IsActive = color.IsActive;
        this.Texture = color.Texture;
        this.ColorMap = color.ColorMap;
        
        return this;
    }

    public async Task<List<BladeCoatingColorPresenter>> PresentList(
        List<BladeCoatingColor> colors, 
        string locale,
        string currency
    )
    {
        List<BladeCoatingColorPresenter> colorsPresenters = new List<BladeCoatingColorPresenter>();
        foreach (BladeCoatingColor bladeCoatingColor in colors)
        {
            BladeCoatingColorPresenter colorsPresenter = new BladeCoatingColorPresenter(this._getComponentPrice);
            await colorsPresenter.Present(bladeCoatingColor, locale, currency);
            colorsPresenters.Add(colorsPresenter);
        }
        return colorsPresenters;
    }
}