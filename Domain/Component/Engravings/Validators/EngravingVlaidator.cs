using Domain.Component.Engravings.Support;
using Domain.Files;

namespace Domain.Component.Engravings.Validators;

public class EngravingVlaidator
{
    public static void Validate(
        int side,
        string? text,
        string? font,
        FileEntity? picture
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
        
        if (picture != null && !Uri.IsWellFormedUriString(picture.FileUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid picture url.", nameof(picture.FileUrl));
        }
    }
}