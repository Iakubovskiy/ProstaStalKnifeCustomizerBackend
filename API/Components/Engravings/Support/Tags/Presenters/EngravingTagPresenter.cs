using Domain.Component.Engravings.Support;

namespace API.Components.Engravings.Support.Tags.Presenters;

public class EngravingTagPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }

    public async Task<EngravingTagPresenter> Present(EngravingTag engravingTag, string locale)
    {
        this.Id = engravingTag.Id;
        this.Name = engravingTag.Name.GetTranslation(locale);
        return this;
    }

    public async Task<EngravingTagPresenter> PresentWithTranslations(EngravingTag engravingTag, string locale)
    {
        await this.Present(engravingTag, locale);
        this.Names = engravingTag.Name.TranslationDictionary;
        return this;
    }

    public async Task<List<EngravingTagPresenter>> PresentList(List<EngravingTag> engravingTags, string locale)
    {
        List<EngravingTagPresenter> presenters = new List<EngravingTagPresenter>();

        foreach (EngravingTag tag in engravingTags)
        {
            EngravingTagPresenter presenter = new EngravingTagPresenter();
            await presenter.Present(tag, locale);
            presenters.Add(presenter);
        }
        return presenters;
    }
}