using Domain.Translation;

namespace Domain.Component.Sheaths.Color.Validators;

public static class SheathColorValidator
{
    public static void Validate(
        List<SheathColorPriceByType> prices,
        Translations material,
        string engravingColorCode
    )
    {
        if (string.IsNullOrWhiteSpace(engravingColorCode))
            throw new ArgumentException("EngravingColorCode can't be empty", nameof(engravingColorCode));

        if (prices.Count == 0)
            throw new ArgumentException("Prices should have at least one element", nameof(prices));
    }
}