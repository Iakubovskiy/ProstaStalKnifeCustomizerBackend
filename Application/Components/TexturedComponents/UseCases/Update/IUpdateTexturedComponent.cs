using Application.Components.TexturedComponents.Data;
using Domain;
using Domain.Component;
using Domain.Component.Textures;

namespace Application.Components.TexturedComponents.UseCases.Update;

public interface IUpdateTexturedComponent<T, TDto> 
    where T : class, ITextured, IComponent, IEntity, IUpdatable<T>
    where TDto : ITexturedComponentDto<T>
{
    public Task<T> Update(Guid id, TDto component);
}