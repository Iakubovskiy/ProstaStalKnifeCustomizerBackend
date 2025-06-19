using Application.Components.Prices;
using Domain.Component.Handles;
using Domain.Component.Textures;
using Domain.Files;

namespace API.Components.Handles.Presenters;

public class HandlePresenter
{
    private readonly IGetComponentPrice _getComponentPrice;

    public HandlePresenter(IGetComponentPrice getComponentPrice)
    {
        this._getComponentPrice = getComponentPrice;
    }
    
    public Guid Id { get; set; }
    public string Color { get; set; }
    public string ColorCode { get; set; }
    public bool IsActive { get; set; }
    public string Material { get; set; }
    public Texture? Texture { get; set; }
    public FileEntity? ColorMap { get; set; }
    public double Price { get; set; }

    public async Task<HandlePresenter> Present(Handle handle, string locale, string currency)
    {
        this.Id = handle.Id;
        this.Color = handle.Color.GetTranslation(locale);
        this.ColorCode = handle.ColorCode;
        this.Material = handle.Material.GetTranslation(locale);
        this.Texture = handle.Texture;
        this.ColorMap = handle.ColorMap;
        this.IsActive = handle.IsActive;
        this.Price = await this._getComponentPrice.GetPrice(handle, currency);
        
        return this;
    }

    public async Task<List<HandlePresenter>> PresentList(List<Handle> handles, string locale, string currency)
    {
        List<HandlePresenter> presenters = new List<HandlePresenter>();
        foreach (Handle handle in handles)
        {
            HandlePresenter presenter = new HandlePresenter(this._getComponentPrice);
            await presenter.Present(handle, locale, currency);
            presenters.Add(presenter);
        }
        return presenters;
    }
}