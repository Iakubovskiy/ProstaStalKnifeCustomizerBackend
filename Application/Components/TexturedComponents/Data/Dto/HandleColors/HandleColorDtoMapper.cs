using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Handles;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Infrastructure;

namespace Application.Components.TexturedComponents.Data.Dto.HandleColors;

public class HandleColorDtoMapper : ITexturedComponentDtoMapper<Handle, HandleColorDto>
{
    private readonly IRepository<FileEntity> _fileRepository;
    private readonly IRepository<Texture> _textureRepository;
    private readonly IRepository<BladeShapeType> _typeRepository;
    
    public HandleColorDtoMapper(
        IRepository<FileEntity> fileRepository,
        IRepository<Texture> textureRepository,
        IRepository<BladeShapeType> typeRepository
    )
    {
        this._fileRepository = fileRepository;
        this._textureRepository = textureRepository;
        this._typeRepository = typeRepository;
    }
    public async Task<Handle> Map(HandleColorDto dto, Texture? texture)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations material = new Translations(dto.Materials);
        Translations color = new Translations(dto.Colors);
        FileEntity? colorMap = null;
        FileEntity? handleModel = null;
        Texture? textureEntity = null;
        BladeShapeType type = await this._typeRepository.GetById(dto.BladeShapeTypeId);

        if (dto.TextureId.HasValue)
        {
            textureEntity = await this._textureRepository.GetById(dto.TextureId.Value);
        }

        if (dto.ColorMapId is not null)
        {
            colorMap = await this._fileRepository.GetById(dto.ColorMapId.Value);
        }

        if (dto.HandleModelId is not null)
        {
            handleModel = await this._fileRepository.GetById(dto.HandleModelId.Value);
        }

        return new Handle(
            id,
            color,
            dto.ColorCode,
            dto.IsActive,
            material,
            textureEntity,
            colorMap,
            dto.Price,
            handleModel,
            type
        );
    }
}