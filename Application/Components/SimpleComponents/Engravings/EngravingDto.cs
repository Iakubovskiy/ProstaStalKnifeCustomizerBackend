namespace Application.Components.SimpleComponents.Engravings;

public class EngravingDto
{
    public Guid? Id { get; set; }
    public Dictionary<string, string> Names { get; set; }
    public Dictionary<string, string> Descriptions { get; set; }
    public Guid? PictureId { get; set; }
    public Guid? PictureForLaserId { get; set; }
    public int Side { get; set; }
    public string? Text { get; set; }
    public string? Font { get; set; }
    public double LocationX { get; set; }
    public double LocationY { get; set; }
    public double LocationZ { get; set; }
    public double RotationX { get; set; }
    public double RotationY { get; set; }
    public double RotationZ { get; set; }
    public double ScaleX { get; set; }
    public double ScaleY { get; set; }
    public double ScaleZ { get; set; }
    public List<Guid> TagsIds { get; set; }
    public bool IsActive { get; set; }
}