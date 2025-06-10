namespace Application.Components.SimpleComponents.Textures;

public class TextureDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public Guid NormalMapId { get; set; }
    public Guid RoughnessMapId { get; set; }
}