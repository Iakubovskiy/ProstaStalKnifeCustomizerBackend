using System.ComponentModel.DataAnnotations;

namespace Domain.Component.Textures;

public class Texture: IEntity, IUpdatable<Texture>
{
    private Texture()
    {
        
    }
    
    public Texture(
        Guid id, 
        string name,
        string normalMapUrl, 
        string roughnessMapUrl
    )
    {
        if (!Uri.IsWellFormedUriString(normalMapUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Normal map url is not valid");
        }

        if (!Uri.IsWellFormedUriString(roughnessMapUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Roughness map url is not valid");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required");
        }
        this.Id = id;
        this.Name = name;
        this.NormalMapUrl = normalMapUrl;
        this.RoughnessMapUrl = roughnessMapUrl;
    }
    
    public Guid Id { get; private set; }
    [MaxLength(255)]
    public string Name { get; private set; }
    [MaxLength(255)]
    public string NormalMapUrl { get; private set; }
    [MaxLength(255)]
    public string RoughnessMapUrl { get; private set; }

    public void Update(Texture texture)
    {
        if (!Uri.IsWellFormedUriString(texture.NormalMapUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Normal map url is not valid");
        }
        if (!Uri.IsWellFormedUriString(texture.RoughnessMapUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Roughness map url is not valid");
        }

        if (string.IsNullOrWhiteSpace(texture.Name))
        {
            throw new ArgumentException("Name is required");
        }
        this.Name = texture.Name;
        this.NormalMapUrl = texture.NormalMapUrl;
        this.RoughnessMapUrl = texture.RoughnessMapUrl;
    }
}