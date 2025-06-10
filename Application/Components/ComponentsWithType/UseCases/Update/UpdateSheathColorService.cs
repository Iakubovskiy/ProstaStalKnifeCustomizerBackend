using Application.Components.ComponentsWithType.SheathColors;
using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Sheaths.Color;
using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.ComponentsWithType.UseCases.Update;

public class UpdateSheathColorService : IUpdateTypeDependencyComponentService<SheathColor, SheathColorDto> 
{
    private readonly ISheathColorRepository _componentRepository;
    private readonly IComponentWithTypeDtoMapper<SheathColor, SheathColorDto> _componentDtoMapper;
    
    public UpdateSheathColorService(
        ISheathColorRepository componentRepository,
        IComponentWithTypeDtoMapper<SheathColor, SheathColorDto> componentDtoMapper
    )
    {
        this._componentRepository = componentRepository;
        this._componentDtoMapper = componentDtoMapper;
    }
    
    public virtual async Task<SheathColor> Update(Guid id, SheathColorDto component)
    {
        var entity = await this._componentDtoMapper.Map(component);
        await this._componentRepository.Update(id,entity);
        
        return entity;
    }
}