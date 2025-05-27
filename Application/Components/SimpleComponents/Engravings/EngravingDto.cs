namespace Application.Components.SimpleComponents.Engravings;

public class EngravingDto
{
    public EngravingDto(
        Dictionary<string, string> name,
        Dictionary<string, string> description,
        Guid? pictureId,
        int side,
        string? text,
        string? font,
        double locationX,
        double locationY,
        double locationZ,
        double rotationX,
        double rotationY,
        double rotationZ,
        double scaleX,
        double scaleY,
        double scaleZ,
        List<Guid> tagsIds,
        bool isActive,
        Guid? id = null
    )
    {
        this.Name = name;
        this.Description = description;
        this.PictureId = pictureId;
        this.Side = side;
        this.Text = text;
        this.Font = font;
        this.LocationX = locationX;
        this.LocationY = locationY;
        this.LocationZ = locationZ;
        this.RotationX = rotationX;
        this.RotationY = rotationY;
        this.RotationZ = rotationZ;
        this.ScaleX = scaleX;
        this.ScaleY = scaleY;
        this.ScaleZ = scaleZ;
        this.TagsIds = tagsIds;
        this.IsActive = isActive;
        this.Id = id;
    }
    
    public Guid? Id { get; set; }
    public Dictionary<string, string> Name { get; set; }
    public Dictionary<string, string> Description { get; set; }
    public Guid? PictureId { get; private set; }
    public int Side { get; private set; }
    public string? Text { get; private set; }
    public string? Font { get; private set; }
    public double LocationX { get; private set; }
    public double LocationY { get; private set; }
    public double LocationZ { get; private set; }
    public double RotationX { get; private set; }
    public double RotationY { get; private set; }
    public double RotationZ { get; private set; }
    public double ScaleX { get; private set; }
    public double ScaleY { get; private set; }
    public double ScaleZ { get; private set; }
    public List<Guid> TagsIds { get; set; }
    public bool IsActive { get; private set; }
}