using Domain;
using Domain.Component.Textures;

namespace Application.Components.TexturedComponents.Data;

public interface ITexturedComponentDto<T> where T : class, IEntity, ITextured
{
    public Guid? TextureId { get; set; }
}