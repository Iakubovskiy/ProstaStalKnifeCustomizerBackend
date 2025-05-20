using Domain.Component.Textures;
using Domain.Component.Translation;

namespace Domain.Component.Handles.Validators;

public static class HandleValidator
{
    public static void Validate(
        string? colorCode,
        string? colorMapUrl
    )
    {
        if (!string.IsNullOrWhiteSpace(colorMapUrl) && !Uri.IsWellFormedUriString(colorMapUrl, UriKind.Absolute))
            throw new ArgumentException("ColorMapUrl має бути валідною URL-адресою.", nameof(colorMapUrl));

        if (colorCode != null && string.IsNullOrWhiteSpace(colorCode))
        {
            throw new AggregateException("color code can't be empty");
        }
    }
}