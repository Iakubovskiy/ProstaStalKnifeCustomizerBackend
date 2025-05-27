using System.Text.RegularExpressions;
using Domain.Files;

namespace Domain.Component.BladeCoatingColors.Validators;

public class BladeCoatingColorValidator
{
    public static void Validate(
        double price,
        string? colorCode,
        string engravingColorCode,
        FileEntity? colorMap
    )
    {
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }

        
        if (string.IsNullOrWhiteSpace(engravingColorCode))
        {
            throw new ArgumentException("Engraving color code cannot be null or whitespace.", nameof(engravingColorCode));
        }
        if (!IsValidHexColor(engravingColorCode))
        {
            throw new ArgumentException("EngravingColorCode is not a valid HEX color format (e.g., #RRGGBB or #RGB).", nameof(engravingColorCode));
        }
        
        if (colorCode != null && !IsValidHexColor(colorCode))
        {
            throw new ArgumentException("ColorCode, if provided, is not a valid HEX color format (e.g., #RRGGBB or #RGB).", nameof(colorCode));
        }

        if (colorMap != null)
        {
            if (string.IsNullOrWhiteSpace(colorMap.FileUrl))
            {
                 throw new ArgumentException("ColorMapUrl cannot be empty or whitespace if provided.", nameof(colorMap.FileUrl));
            }
            if (!Uri.IsWellFormedUriString(colorMap.FileUrl, UriKind.Absolute))
            {
                throw new ArgumentException("ColorMapUrl is not a valid absolute URL.", nameof(colorMap.FileUrl));
            }
        }
        
    }
    
    private static bool IsValidHexColor(string? colorString)
    {
        if (string.IsNullOrWhiteSpace(colorString))
        {
            return false; 
        }
        return Regex.IsMatch(colorString, @"^#((?:[0-9a-fA-F]{3,4}){1,2})$");
    }
}