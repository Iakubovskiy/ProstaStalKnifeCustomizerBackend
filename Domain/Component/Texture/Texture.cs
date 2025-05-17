using Microsoft.EntityFrameworkCore;

namespace Domain.Component.Texture;

[Owned]
public class Texture
{
    public Guid Id { get; private set; }
    public string NormalMapUrl { get; private set; }
    public string RoughnessMapUrl { get; private set; }

    private Texture()
    {
        
    }
    
    public Texture(Guid id, string normalMapUrl, string roughnessMapUrl)
    {
        Id = id;
        NormalMapUrl = normalMapUrl;
        RoughnessMapUrl = roughnessMapUrl;
    }
}