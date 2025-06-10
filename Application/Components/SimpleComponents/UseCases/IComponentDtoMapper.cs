using Domain;
using Domain.Component;
using Domain.Component.Textures;

namespace Application.Components.SimpleComponents.UseCases;

public interface IComponentDtoMapper<T, TDto> 
    where T : class, IEntity, IUpdatable<T>
{
    public Task<T> Map(TDto dto);
}