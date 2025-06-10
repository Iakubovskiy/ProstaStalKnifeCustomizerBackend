using Application.Components.TexturedComponents.Data;
using Domain;
using Domain.Component;
using Domain.Component.Textures;
using Infrastructure;
using Infrastructure.Components;

namespace Application.Components.TexturedComponents.UseCases.Create;

public class CreateTexturedComponent<T, TDto> : ICreateTexturedComponent<T, TDto> 
    where T : class, ITextured, IComponent, IEntity, IUpdatable<T>
    where TDto : class, ITexturedComponentDto<T>
{
    private readonly IComponentRepository<T> _componentRepository;
    private readonly IRepository<Texture> _textureRepository;
    private readonly ITexturedComponentDtoMapper<T, TDto> _texturedComponentDtoMapper;
    
    public CreateTexturedComponent(
        IComponentRepository<T> componentRepository,
        IRepository<Texture> textureRepository,
        ITexturedComponentDtoMapper<T, TDto> texturedComponentDtoMapper
    )
    {
        this._componentRepository = componentRepository;
        this._textureRepository = textureRepository;
        this._texturedComponentDtoMapper = texturedComponentDtoMapper;
    }
    
    public virtual async Task<T> Create(TDto component)
    {
        Texture? texture = null;
        if (component.TextureId is { } textureId && textureId != Guid.Empty)
        {
            texture = await this._textureRepository.GetById(textureId);
        }
        var entity = await this._texturedComponentDtoMapper.Map(component, texture);
        await this._componentRepository.Create(entity);
        
        return entity;
    }
}