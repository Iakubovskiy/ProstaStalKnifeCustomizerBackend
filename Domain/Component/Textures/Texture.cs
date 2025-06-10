using System.ComponentModel.DataAnnotations;
using Domain.Files;

namespace Domain.Component.Textures;

public class Texture: IEntity, IUpdatable<Texture>
{
    private Texture()
    {
        
    }
    
    public Texture(
        Guid id, 
        string name,
        FileEntity normalMap, 
        FileEntity roughnessMap
    )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required");
        }
        this.Id = id;
        this.Name = name;
        this.NormalMap = normalMap;
        this.RoughnessMap = roughnessMap;
    }
    
    public Guid Id { get; private set; }
    [MaxLength(255)]
    public string Name { get; private set; }
    [MaxLength(255)]
    public FileEntity NormalMap { get; private set; }
    [MaxLength(255)]
    public FileEntity RoughnessMap { get; private set; }

    public void Update(Texture texture)
    {
        this.Name = texture.Name;
        this.NormalMap = texture.NormalMap;
        this.RoughnessMap = texture.RoughnessMap;
    }
}