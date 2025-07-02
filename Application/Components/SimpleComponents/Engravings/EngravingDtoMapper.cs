using Application.Components.Prices.Engravings;
using Domain.Component.Engravings;
using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Engravings.Parameters;
using Domain.Component.Engravings.Support;
using Domain.Files;
using Domain.Translation;
using Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Application.Components.SimpleComponents.Engravings;

public class EngravingDtoMapper : IComponentDtoMapper<Engraving, EngravingDto>
{
    private readonly IRepository<EngravingTag> _engravingTagRepository;
    private readonly IRepository<FileEntity> _fileRepository;
    private readonly IGetEngravingPrice _getEngravingPrice;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const double MockedEngravingPrice = 0.0; 
    
    public EngravingDtoMapper(
        IRepository<EngravingTag> engravingTagRepository,
        IRepository<FileEntity> fileRepository,
        IGetEngravingPrice getEngravingPrice,
        IHttpContextAccessor httpContextAccessor
    )
    {
        this._engravingTagRepository = engravingTagRepository;
        this._getEngravingPrice = getEngravingPrice;
        this._httpContextAccessor = httpContextAccessor;
        this._fileRepository = fileRepository;
    }
    public async Task<Engraving> Map(EngravingDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        Translations names = new Translations(dto.Names);
        Translations descriptions = new Translations(dto.Descriptions);
        List<EngravingTag> tags = new List<EngravingTag>();
        EngravingPosition engravingPosition = new EngravingPosition(
            dto.LocationX,
            dto.LocationY,
            dto.LocationZ
        );
        EngravingRotation engravingRotation = new EngravingRotation(
            dto.RotationX,
            dto.RotationY,
            dto.RotationZ
        );
        EngravingScale engravingScale = new EngravingScale(
            dto.ScaleX,
            dto.ScaleY,
            dto.ScaleZ
        );
        FileEntity image = null;
        if(dto.PictureId is not null)
        {
            image = await this._fileRepository.GetById(dto.PictureId.Value);
        }
        
        foreach (Guid tagId in dto.TagsIds)
        {
            EngravingTag tag = await this._engravingTagRepository.GetById(tagId);
            tags.Add(tag);
        }

        return new Engraving(
            id,
            names,
            dto.Side,
            dto.Text,
            dto.Font,
            image,
            engravingPosition,
            engravingRotation,
            engravingScale,
            tags,
            descriptions,
            MockedEngravingPrice,
            dto.IsActive
        );
    }
}