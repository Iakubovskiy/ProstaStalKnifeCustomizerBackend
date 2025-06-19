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
    public EngravingPosition Position { get; set; }
    public EngravingRotation Rotation { get; set; }
    public EngravingScale Scale { get; set; }
    public List<EngravingTagPresenter> Tags { get; set; }
    public string Description {get; set;}
    public Dictionary<string, string> Descriptions {get; set;}
    public bool IsActive {get; set;}

    public async Task<EngravingPresenter> Present(Engraving engraving, string locale)
    {
        this.Id = engraving.Id;
        this.Name = engraving.Name.GetTranslation(locale);
        this.Side = engraving.Side;
        this.Text = engraving.Text;
        this.Font = engraving.Font;
        this.Picture = engraving.Picture;
        this.Position = engraving.EngravingPosition;
        this.Rotation = engraving.EngravingRotation;
        this.Scale = engraving.EngravingScale;
        this.Description = engraving.Description.GetTranslation(locale);
        this.IsActive = engraving.IsActive;
        
        EngravingTagPresenter presenter = new EngravingTagPresenter();
        this.Tags = await presenter.PresentList(engraving.Tags, locale);

        return this;
    }
    
    public async Task<EngravingPresenter> PresentWithTranslations(Engraving engraving, string locale)
    {
        await this.Present(engraving, locale);
        this.Names = engraving.Name.TranslationDictionary;
        this.Descriptions = engraving.Description.TranslationDictionary;
        return this;
    }

    public async Task<List<EngravingPresenter>> PresentList(List<Engraving> engravings, string locale)
    {
        List<EngravingPresenter> presenters = new List<EngravingPresenter>();
        foreach (Engraving engraving in engravings)
        {
            EngravingPresenter presenter = new EngravingPresenter();
            await presenter.Present(engraving, locale);
            presenters.Add(presenter);
        }
        
        return presenters;
    }
}