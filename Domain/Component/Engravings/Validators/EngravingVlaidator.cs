using Domain.Component.Engravings.Support;

namespace Domain.Component.Engravings.Validators;

public class EngravingVlaidator
{
    public static void Validate(
        int side,
        string? text,
        string? font,
        string? pictureUrl
    )
    {
        if (!Enum.IsDefined(typeof(Side), side))
        {
            throw new ArgumentException("Invalid side value.", nameof(side));
        }

        if (text != null && font == null)
        {
            throw new ArgumentException("Font is required when text is provided.", nameof(font));
        }
        
        if (pictureUrl != null && !Uri.IsWellFormedUriString(pictureUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid picture url.", nameof(pictureUrl));
        }

        if (text != null && string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text can't be empty.", nameof(text));
        }
    }
}