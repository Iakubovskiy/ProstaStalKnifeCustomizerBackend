using Application.Components.Prices;
using Domain.Component.Handles;
using Domain.Component.Textures;
using Domain.Files;

namespace API.Components.Handles.Presenters;

public class HandlePresenter
{
    public Guid Id { get; set; }
    public string Color { get; set; }
    public Dictionary<string, string> Colors { get; set; }
    public string ColorCode { get; set; }
    public bool IsActive { get; set; }
    public string Material { get; set; }
    public Dictionary<string, string> Materials { get; set; }
    public Texture? Texture { get; set; }
    public FileEntity? ColorMap { get; set; }
    public double Price { get; set; }

    public static async Task<HandlePresenter> Present(
        Handle handle, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        return new HandlePresenter
        {
            Id = handle.Id,
            Color = handle.Color.GetTranslation(locale),
            ColorCode = handle.ColorCode,
            Material = handle.Material.GetTranslation(locale),
            Texture = handle.Texture,
            ColorMap = handle.ColorMap,
            IsActive = handle.IsActive,
            Price = await getComponentPrice.GetPrice(handle, currency)
        };
    }

    public static async Task<HandlePresenter> PresentWithTranslations(
        Handle handle, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        HandlePresenter presenter = await Present(handle, locale, currency, getComponentPrice);
        presenter.Colors = handle.Color.TranslationDictionary;
        presenter.Materials = handle.Material.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<HandlePresenter>> PresentList(
        List<Handle> handles, 
        string locale, 
        string currency, 
        IGetComponentPrice getComponentPrice)
    {
        var presenters = new List<HandlePresenter>();
        foreach (Handle handle in handles)
        {
            HandlePresenter presenter = await Present(handle, locale, currency, getComponentPrice);
            presenters.Add(presenter);
        }
        return presenters;
    }
}