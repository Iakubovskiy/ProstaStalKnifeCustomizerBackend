using Application.Components.ComponentsWithType.SheathColors;
using Application.Components.SimpleComponents.Sheaths;
using Application.Components.SimpleComponents.UseCases;
using Domain;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.ComponentsWithType.UseCases.Create;

public class CreateSheathColor : ICreateTypeDependencyComponentService<SheathColor, SheathColorDto> 
{
    private readonly ISheathColorRepository _componentRepository;
    private readonly IComponentWithTypeDtoMapper<SheathColor, SheathColorDto> _componentDtoMapper;
    
    public CreateSheathColor(
        ISheathColorRepository componentRepository,
        IComponentWithTypeDtoMapper<SheathColor, SheathColorDto> componentDtoMapper
    )
    {
        this._componentRepository = componentRepository;
        this._componentDtoMapper = componentDtoMapper;
    }
    
    public virtual async Task<SheathColor> Create(SheathColorDto component)
    {
        
        var entity = await this._componentDtoMapper.Map(component);
        await this._componentRepository.Create(entity);
        
        return entity;
    }
}