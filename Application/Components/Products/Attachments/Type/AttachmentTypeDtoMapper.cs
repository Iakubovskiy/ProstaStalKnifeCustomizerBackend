using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Product.Attachments;
using Domain.Translation;

namespace Application.Components.Products.Attachments.Type;

public class AttachmentTypeDtoMapper: IComponentDtoMapper<AttachmentType, AttachmentTypeDto>
{
    public async Task<AttachmentType> Map(AttachmentTypeDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Name);
        return new AttachmentType(
            id,
            names
        );
    }
}