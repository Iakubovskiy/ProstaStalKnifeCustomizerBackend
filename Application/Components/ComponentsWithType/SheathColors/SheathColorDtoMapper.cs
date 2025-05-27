using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Infrastructure;

namespace Application.Components.ComponentsWithType.SheathColors;

public class SheathColorDtoMapper : IComponentWithTypeDtoMapper<SheathColor, SheathColorDto>
{
    private readonly IRepository<FileEntity> _fileRepository;
    private readonly IRepository<Texture> _textureRepository;

    public SheathColorDtoMapper(
        IRepository<FileEntity> fileRepository,
        IRepository<Texture> textureRepository
    )
    {
        this._fileRepository = fileRepository;
        this._textureRepository = textureRepository;
    }
    public async Task<SheathColor> Map(SheathColorDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations colors = new Translations(dto.Color);
        Translations materials = new Translations(dto.Material);
        Texture texture = null;
        FileEntity colorMap = null;
        if (dto.TextureId is not null)
        {
            texture = await this._textureRepository.GetById(dto.TextureId.Value);
        }

        if (dto.ColorMapId is not null)
        {
            colorMap = await this._fileRepository.GetById(dto.ColorMapId.Value);
        }

        return new SheathColor(
            id,
            colors,
            dto.IsActive,
            dto.ColorCode,
            dto.Prices,
            materials,
            dto.EngravingColorCode,
            texture,
            colorMap
        );
    }
}