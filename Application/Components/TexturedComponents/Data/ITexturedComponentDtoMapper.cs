using Domain;
using Domain.Component;
using Domain.Component.Textures;

namespace Application.Components.TexturedComponents.Data;

public interface ITexturedComponentDtoMapper<T, TDto> 
    where T : class, IEntity, IUpdatable<T>, ITextured
    where TDto : ITexturedComponentDto<T>
{
    public Task<T> Map(TDto dto, Texture? texture);
}