using Microsoft.EntityFrameworkCore;

namespace Domain.Translation;

[Owned]
public class Translations : ITranslatable
{
    private Translations()
    {
        
    }
    
    public Translations(Dictionary<string, string> translationDictionary)
    {
        this.TranslationDictionary = translationDictionary;    
    }

    public Dictionary<string, string> TranslationDictionary { get; private set;  }
    public string GetTranslation(string language)
    {
        return this.TranslationDictionary[language];
    }
    
}