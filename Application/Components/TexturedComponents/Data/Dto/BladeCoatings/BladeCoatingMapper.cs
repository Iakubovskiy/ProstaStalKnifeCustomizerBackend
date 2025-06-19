using Application.Files;
using Domain.Component.BladeCoatingColors;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Infrastructure;
using Newtonsoft.Json;

namespace Application.Components.TexturedComponents.Data.Dto.BladeCoatings;

public class BladeCoatingMapper : ITexturedComponentDtoMapper<BladeCoatingColor, BladeCoatingDto>
{
    private readonly IRepository<FileEntity> _fileRepository;
    public BladeCoatingMapper(IRepository<FileEntity> fileRepository)
    {
        this._fileRepository = fileRepository;
    }
    public async Task<BladeCoatingColor> Map(BladeCoatingDto dto, Texture? texture)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations type = new Translations(dto.Types);
        Translations color = new Translations(dto.Colors);
        FileEntity? colorMap = null;
        if (dto.ColorMapId is not null)
        {
            colorMap = await this._fileRepository.GetById(dto.ColorMapId.Value);
        }
        
        return new BladeCoatingColor(
                id,
                type,
                dto.Price,
                color,
                dto.ColorCode,
                dto.EngravingColorCode,
                texture,
                colorMap
        );
    }
}