using API.Components.Engravings.Support.Tags.Presenters;
using Application.Components.Prices.Engravings;
using Domain.Component.Engravings;
using Domain.Component.Engravings.Parameters;
using Domain.Component.Engravings.Support;
using Domain.Files;

namespace API.Components.Engravings.Presenters;

public class EngravingPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public int Side { get; set; }
    public string? Text { get; set; }
    public string? Font { get; set; }
    public FileEntity? Picture { get; set; }
    public FileEntity? PictureForLaser { get; set; }
    public EngravingPosition Position { get; set; }
    public EngravingRotation Rotation { get; set; }
    public EngravingScale Scale { get; set; }
    public List<EngravingTagPresenter> Tags { get; set; }
    public string Description {get; set;}
    public Dictionary<string, string> Descriptions {get; set;}
    public bool IsActive {get; set;}

    public static async Task<EngravingPresenter> Present(Engraving engraving, string locale)
    {
        var presenter = new EngravingPresenter
        {
            Id = engraving.Id,
            Name = engraving.Name.GetTranslation(locale),
            Side = engraving.Side,
            Text = engraving.Text,
            Font = engraving.Font,
            Picture = engraving.Picture,
            PictureForLaser = engraving.PictureForLaser,
            Position = engraving.EngravingPosition,
            Rotation = engraving.EngravingRotation,
            Scale = engraving.EngravingScale,
            Description = engraving.Description.GetTranslation(locale),
            IsActive = engraving.IsActive,
            Tags = await EngravingTagPresenter.PresentList(engraving.Tags, locale)
        };

        return presenter;
    }
    
    public static async Task<EngravingPresenter> PresentWithTranslations(Engraving engraving, string locale)
    {
        EngravingPresenter presenter = await Present(engraving, locale);
        presenter.Names = engraving.Name.TranslationDictionary;
        presenter.Descriptions = engraving.Description.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<EngravingPresenter>> PresentList(List<Engraving> engravings, string locale)
    {
        var presenters = new List<EngravingPresenter>();
        foreach (Engraving engraving in engravings)
        {
            EngravingPresenter presenter = await Present(engraving, locale);
            presenters.Add(presenter);
        }
        
        return presenters;
    }
}