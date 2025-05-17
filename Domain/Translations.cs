using Microsoft.EntityFrameworkCore;

namespace Domain.Component.Translation;

[Owned]
public class Translations : ITranslatable
{
    public Dictionary<string, string> TranslationDictionary { get; }
    
    public Translations(Dictionary<string, string> translations)
    {
        this.TranslationDictionary = translations;    
    }

    public string GetTranslation(string language)
    {
        return this.TranslationDictionary[language];
    }
}