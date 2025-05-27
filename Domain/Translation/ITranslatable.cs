namespace Domain.Translation;

public interface ITranslatable
{
    public string GetTranslation(string language);
}