using Microsoft.EntityFrameworkCore;

namespace Domain.Component.Translation;

[Owned]
public class Translations : ITranslatable
{
    private Translations()
    {
        
    }
    
    public Translations(Dictionary<string, string> translations)
    {
        this.TranslationDictionary = translations;    
    }

    public Dictionary<string, string> TranslationDictionary { get; private set;  }
    public string GetTranslation(string language)
    {
        return this.TranslationDictionary[language];
    }
    
}