using Domain.Component.Engravings.Support;

namespace API.Components.Engravings.Support.Tags.Presenters;

public class EngravingTagPresenter
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Names { get; set; }

    public static Task<EngravingTagPresenter> Present(EngravingTag engravingTag, string locale)
    {
        var presenter = new EngravingTagPresenter
        {
            Id = engravingTag.Id,
            Name = engravingTag.Name.GetTranslation(locale)
        };
        return Task.FromResult(presenter);
    }

    public static async Task<EngravingTagPresenter> PresentWithTranslations(EngravingTag engravingTag, string locale)
    {
        EngravingTagPresenter presenter = await Present(engravingTag, locale);
        presenter.Names = engravingTag.Name.TranslationDictionary;
        return presenter;
    }

    public static async Task<List<EngravingTagPresenter>> PresentList(List<EngravingTag> engravingTags, string locale)
    {
        var presenters = new List<EngravingTagPresenter>();
        foreach (EngravingTag tag in engravingTags)
        {
            EngravingTagPresenter presenter = await Present(tag, locale);
            presenters.Add(presenter);
        }
        return presenters;
    }
}