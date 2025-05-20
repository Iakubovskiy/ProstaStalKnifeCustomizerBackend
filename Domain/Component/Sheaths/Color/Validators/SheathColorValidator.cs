using Domain.Component.Translation;

namespace Domain.Component.Sheaths.Color.Validators;

public static class SheathColorValidator
{
    public static void Validate(
        List<SheathColorPriceByType> prices,
        string material,
        string engravingColorCode,
        string? colorMapUrl
    )
    {

        if (string.IsNullOrWhiteSpace(material))
            throw new ArgumentException("Material can't be empty", nameof(material));

        if (string.IsNullOrWhiteSpace(engravingColorCode))
            throw new ArgumentException("EngravingColorCode can't be empty", nameof(engravingColorCode));

        if (prices.Count == 0)
            throw new ArgumentException("Prices should have at least one element", nameof(prices));

        if (!string.IsNullOrWhiteSpace(colorMapUrl) && !Uri.IsWellFormedUriString(colorMapUrl, UriKind.Absolute))
            throw new ArgumentException("ColorMapUrl is invalid URL.", nameof(colorMapUrl));
    }
}