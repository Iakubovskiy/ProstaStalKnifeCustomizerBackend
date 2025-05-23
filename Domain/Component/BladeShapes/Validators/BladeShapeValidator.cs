

using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Translation;

namespace Domain.Component.BladeShapes.Validators;

public class BladeShapeValidator
{
    private const int UrlMaxLength = 255;
    
    public static void Validate(
        string? bladeShapePhotoUrl,
        double price,
        string bladeShapeModelUrl
    )
    {
        if (bladeShapePhotoUrl != null)
        {
            if (string.IsNullOrWhiteSpace(bladeShapePhotoUrl))
            {
                throw new ArgumentException("BladeShapePhotoUrl cannot be empty or whitespace if provided.", nameof(bladeShapePhotoUrl));
            }
            if (bladeShapePhotoUrl.Length > UrlMaxLength)
            {
                throw new ArgumentException($"BladeShapePhotoUrl cannot exceed {UrlMaxLength} characters.", nameof(bladeShapePhotoUrl));
            }
            if (!Uri.IsWellFormedUriString(bladeShapePhotoUrl, UriKind.Absolute))
            {
                throw new ArgumentException("BladeShapePhotoUrl is not a valid absolute URL.", nameof(bladeShapePhotoUrl));
            }
        }
        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(bladeShapeModelUrl))
        {
            throw new ArgumentException("BladeShapeModelUrl cannot be null or whitespace.", nameof(bladeShapeModelUrl));
        }
        if (bladeShapeModelUrl.Length > UrlMaxLength)
        {
            throw new ArgumentException($"BladeShapeModelUrl cannot exceed {UrlMaxLength} characters.", nameof(bladeShapeModelUrl));
        }
        if (!Uri.IsWellFormedUriString(bladeShapeModelUrl, UriKind.Absolute))
        {
            throw new ArgumentException("BladeShapeModelUrl is not a valid absolute URL.", nameof(bladeShapeModelUrl));
        }
    }
}