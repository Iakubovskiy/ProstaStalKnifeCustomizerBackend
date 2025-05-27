using Application.Components.SimpleComponents.UseCases;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;
using Domain.Translation;
using Infrastructure;
using Infrastructure.Components;

namespace Application.Components.SimpleComponents.Sheaths;

public class SheathDtoMapper : IComponentDtoMapper<Sheath, SheathDto>
{
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly IRepository<BladeShapeType> _typeRepository;
    private readonly IRepository<FileEntity> _fileRepository;
    
    public SheathDtoMapper(
        IComponentRepository<Sheath> sheathRepository,
        IRepository<BladeShapeType> typeRepository,
        IRepository<FileEntity> fileRepository
    )
    {
        this._sheathRepository = sheathRepository;
        this._typeRepository = typeRepository;
        this._fileRepository = fileRepository;
    }
    public async Task<Sheath> Map(SheathDto dto)
    {
        Guid id = Guid.NewGuid();
        BladeShapeType type = await this._typeRepository.GetById(dto.TypeId);
        Translations name = new Translations(dto.Name);

        FileEntity model = null;
        if (dto.SheathModelId is not null)
        {
            model = await this._fileRepository.GetById(dto.SheathModelId.Value);
        }

        return new Sheath(
            id,
            name,
            model,
            type,
            dto.Price,
            dto.IsActive 
        );
    }
}