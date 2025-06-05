using Application.Components.SimpleComponents.UseCases;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;
using Domain.Translation;
using Infrastructure;
using Infrastructure.Components;

namespace Application.Components.SimpleComponents.BladeShapes;

public class BladeShapeDtoMapper : IComponentDtoMapper<BladeShape, BladeShapeDto>
{
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly IRepository<BladeShapeType> _typeRepository;
    private readonly IRepository<FileEntity> _fileRepository;
    
    public BladeShapeDtoMapper(
        IComponentRepository<Sheath> sheathRepository,
        IRepository<BladeShapeType> typeRepository,
        IRepository<FileEntity> fileRepository
    )
    {
        this._sheathRepository = sheathRepository;
        this._typeRepository = typeRepository;
        this._fileRepository = fileRepository;
    }
    public async Task<BladeShape> Map(BladeShapeDto dto)
    {
        Guid id =dto.Id ?? Guid.NewGuid();
        Sheath sheath = null;
        BladeShapeType type = await this._typeRepository.GetById(dto.TypeId);
        Translations name = new Translations(dto.Name);
        BladeCharacteristics characteristics = new BladeCharacteristics(
            dto.TotalLength,
            dto.BladeLength,
            dto.BladeWidth,
            dto.BladeWeight,
            dto.SharpeningAngle,
            dto.RockwellHardnessUnits
        );
        
        if (dto.SheathId is not null)
        {
            sheath = await this._sheathRepository.GetById(dto.SheathId.Value);
        }
        FileEntity image = await this._fileRepository.GetById(dto.BladeShapePhotoId);
        FileEntity model = await this._fileRepository.GetById(dto.BladeShapeModelId);

        return new BladeShape(
            id,
            type,
            name,
            image,
            dto.Price,
            characteristics,
            sheath,
            model,
           dto.IsActive 
        );
    }
}