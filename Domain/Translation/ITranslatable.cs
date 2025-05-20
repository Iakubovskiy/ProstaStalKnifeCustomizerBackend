namespace Domain.Component.Translation;

public interface ITranslatable
{
    public string GetTranslation(string language);
}