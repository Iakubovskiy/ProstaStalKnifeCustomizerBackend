namespace Application.Components.SimpleComponents.Textures;

public class TextureDto
{
    public TextureDto(
        string name,
        Guid normalMapId,
        Guid roughnessMapId,
        Guid? id = null
    )
    {
        this.Name = name;
        this.NormalMapId = normalMapId;
        this.RoughnessMapId = roughnessMapId;
        this.Id = id;
    }
    
    public Guid? Id { get; private set; }
    public string Name { get; private set; }
    public Guid NormalMapId { get; private set; }
    public Guid RoughnessMapId { get; private set; }
}