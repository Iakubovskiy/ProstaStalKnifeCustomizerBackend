using Domain.Files;

namespace Domain.Component.BladeShapes.Validators;

public class BladeShapeValidator
{
    private const int UrlMaxLength = 255;
    
    public static void Validate(
        FileEntity? bladeShapePhoto,
        double price,
        FileEntity bladeShapeModel
    )
    {
        if (bladeShapePhoto != null)
        {
            if (string.IsNullOrWhiteSpace(bladeShapePhoto.FileUrl))
            {
                throw new ArgumentException("BladeShapePhotoUrl cannot be empty or whitespace if provided.", nameof(bladeShapePhoto.FileUrl));
            }
            if (bladeShapePhoto.FileUrl.Length > UrlMaxLength)
            {
                throw new ArgumentException($"BladeShapePhotoUrl cannot exceed {UrlMaxLength} characters.", nameof(bladeShapePhoto.FileUrl));
            }
            if (!Uri.IsWellFormedUriString(bladeShapePhoto.FileUrl, UriKind.Absolute))
            {
                throw new ArgumentException("BladeShapePhotoUrl is not a valid absolute URL.", nameof(bladeShapePhoto.FileUrl));
            }
        }
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(bladeShapeModel.FileUrl))
        {
            throw new ArgumentException("BladeShapeModelUrl cannot be null or whitespace.", nameof(bladeShapeModel.FileUrl));
        }
        if (bladeShapeModel.FileUrl.Length > UrlMaxLength)
        {
            throw new ArgumentException($"BladeShapeModelUrl cannot exceed {UrlMaxLength} characters.", nameof(bladeShapeModel.FileUrl));
        }
        if (!Uri.IsWellFormedUriString(bladeShapeModel.FileUrl, UriKind.Absolute))
        {
            throw new ArgumentException("BladeShapeModelUrl is not a valid absolute URL.", nameof(bladeShapeModel.FileUrl));
        }
    }
}