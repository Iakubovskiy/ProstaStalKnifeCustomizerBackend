using Domain.Files;

namespace Domain.Component.Handles.Validators;

public static class HandleValidator
{
    public static void Validate(
        string? colorCode,
        FileEntity? colorMap
    )
    {
        if (colorMap != null && !string.IsNullOrWhiteSpace(colorMap.FileUrl) && !Uri.IsWellFormedUriString(colorMap.FileUrl, UriKind.Absolute))
            throw new ArgumentException("ColorMapUrl має бути валідною URL-адресою.", nameof(colorMap.FileUrl));

        if (colorCode != null && string.IsNullOrWhiteSpace(colorCode))
        {
            throw new AggregateException("color code can't be empty");
        }
    }
}