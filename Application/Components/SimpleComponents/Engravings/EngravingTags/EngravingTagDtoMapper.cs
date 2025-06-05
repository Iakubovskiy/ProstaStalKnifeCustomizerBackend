using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Engravings.Support;
using Domain.Translation;

namespace Application.Components.SimpleComponents.Engravings.EngravingTags;

public class EngravingTagDtoMapper : IComponentDtoMapper<EngravingTag, EngravingTagDto>
{
    public async Task<EngravingTag> Map(EngravingTagDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Name);

        return new EngravingTag(id, names);
    }
}