using System.Reflection;
using Application.Components.SimpleComponents.UseCases;
using Domain.Component.BladeShapes.BladeShapeTypes;
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
    private readonly IRepository<BladeShapeType> _typeRepository;

    public SheathColorDtoMapper(
        IRepository<FileEntity> fileRepository,
        IRepository<Texture> textureRepository,
        IRepository<BladeShapeType> typeRepository
    )
    {
        this._fileRepository = fileRepository;
        this._textureRepository = textureRepository;
        this._typeRepository = typeRepository;
    }

    public async Task<SheathColor> Map(SheathColorDto dto)
    {
        var id = dto.Id ?? Guid.NewGuid();
        var colors = new Translations(dto.Color);
        var materials = new Translations(dto.Material);

        Texture? texture = null;
        FileEntity? colorMap = null;

        if (dto.TextureId is not null)
        {
            texture = await _textureRepository.GetById(dto.TextureId.Value);
        }

        if (dto.ColorMapId is not null)
        {
            colorMap = await _fileRepository.GetById(dto.ColorMapId.Value);
        }

        var dummySheathColor = (SheathColor)typeof(SheathColor)
            .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null)!
            .Invoke(null);

        var prices = new List<SheathColorPriceByType>();

        foreach (var priceDto in dto.Prices)
        {
            var type = await _typeRepository.GetById(priceDto.TypeId);
            if (type == null)
            {
                throw new ArgumentException($"BladeShapeType with id {priceDto.TypeId} not found");
            }

            prices.Add(new SheathColorPriceByType(type, dummySheathColor, priceDto.Price));
        }

        var sheathColor = new SheathColor(
            id,
            colors,
            dto.IsActive,
            dto.ColorCode,
            prices,
            materials,
            dto.EngravingColorCode,
            texture,
            colorMap
        );

        foreach (var price in sheathColor.Prices)
        {
            typeof(SheathColorPriceByType)
                .GetProperty(nameof(SheathColorPriceByType.SheathColor))!
                .SetValue(price, sheathColor);

            typeof(SheathColorPriceByType)
                .GetProperty(nameof(SheathColorPriceByType.SheathColorId))!
                .SetValue(price, sheathColor.Id);
        }

        return sheathColor;
    }
}
