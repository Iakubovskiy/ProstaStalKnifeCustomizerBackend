using Application.Components.SimpleComponents.UseCases;
using Application.Files;
using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Component.Translation;
using Infrastructure;
using Infrastructure.Components;
using Newtonsoft.Json;

namespace Application.Components.SimpleComponents.BladeShapes;

public class BladeShapeDtoMapper : IComponentDtoMapper<BladeShape, BladeShapeDto>
{
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly IRepository<BladeShapeType> _typeRepository;
    private readonly IFileService _fileService;
    
    public BladeShapeDtoMapper(
        IComponentRepository<Sheath> sheathRepository,
        IRepository<BladeShapeType> typeRepository,
        IFileService fileService
    )
    {
        this._sheathRepository = sheathRepository;
        this._typeRepository = typeRepository;
        this._fileService = fileService;
    }
    public async Task<BladeShape> Map(BladeShapeDto dto)
    {
        Guid id = Guid.NewGuid();
        Sheath sheath = null;
        BladeShapeType type = await this._typeRepository.GetById(dto.TypeId);
        Translations name;
        BladeCharacteristics characteristics = new BladeCharacteristics(
            dto.TotalLength,
            dto.BladeLength,
            dto.BladeWidth,
            dto.BladeWeight,
            dto.SharpeningAngle,
            dto.RockwellHardnessUnits
        );
        
        if (dto.SheathId.HasValue)
        {
            sheath = await this._sheathRepository.GetById(dto.SheathId.Value);
        }
        try
        {
            name = JsonConvert.DeserializeObject<Translations>(dto.NameJson)
                   ?? throw new Exception("Name is empty");
        }
        catch (Exception e)
        {
            throw new Exception("Name is not valid");
        }
        string imageUrl = await this._fileService.SaveFile(dto.BladeShapePhoto, dto.BladeShapePhoto.FileName);
        string modelUrl = await this._fileService.SaveFile(dto.BladeShapeModel, dto.BladeShapeModel.FileName);

        return new BladeShape(
            id,
            type,
            name,
            imageUrl,
            dto.Price,
            characteristics,
            sheath,
            modelUrl,
           dto.IsActive 
        );
    }
}