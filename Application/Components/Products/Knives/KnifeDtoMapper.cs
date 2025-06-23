using Application.Components.Prices.Engravings;
using Application.Components.SimpleComponents.Engravings;
using Application.Components.SimpleComponents.UseCases;
using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Engravings;
using Domain.Component.Handles;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Files;
using Domain.Translation;
using Infrastructure;
using Infrastructure.Components;
using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.Products.Knives;


public class KnifeDtoMapper : IProductDtoMapper<Knife, KnifeDto>
{
    private readonly IComponentRepository<BladeShape> _bladeShapeRepository;
    private readonly IComponentRepository<BladeCoatingColor> _bladeCoatingColorRepository;
    private readonly IComponentRepository<Handle> _handleRepository;
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly IComponentRepository<Engraving> _engravingRepository;
    private readonly IComponentRepository<Attachment> _attachmentRepository;
    private readonly IRepository<ProductTag> _tagRepository;
    private readonly IRepository<FileEntity> _fileRepository;
    private readonly IComponentDtoMapper<Engraving, EngravingDto> _engravingDtoMapper;
    private readonly IGetEngravingPrice _getEngravingPrice;

    public KnifeDtoMapper(
        IComponentRepository<BladeShape> bladeShapeRepository,
        IComponentRepository<BladeCoatingColor> bladeCoatingColorRepository,
        IComponentRepository<Handle> handleRepository,
        IComponentRepository<Sheath> sheathRepository,
        ISheathColorRepository sheathColorRepository,
        IComponentRepository<Engraving> engravingRepository,
        IComponentRepository<Attachment> attachmentRepository,
        IRepository<ProductTag> tagRepository,
        IRepository<FileEntity> fileRepository,
        IComponentDtoMapper<Engraving, EngravingDto> engravingDtoMapper,
        IGetEngravingPrice getEngravingPrice
    )
    {
        this._bladeShapeRepository = bladeShapeRepository;
        this._bladeCoatingColorRepository = bladeCoatingColorRepository;
        this._handleRepository = handleRepository;
        this._sheathRepository = sheathRepository;
        this._sheathColorRepository = sheathColorRepository;
        this._engravingRepository = engravingRepository;
        this._attachmentRepository = attachmentRepository;
        this._tagRepository = tagRepository;
        this._fileRepository = fileRepository;
        this._engravingDtoMapper = engravingDtoMapper;
        this._getEngravingPrice = getEngravingPrice;
    }

    public async Task<Knife> Map(KnifeDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        var shape = await _bladeShapeRepository.GetById(dto.ShapeId);
        var bladeCoatingColor = await _bladeCoatingColorRepository.GetById(dto.BladeCoatingColorId);
        var imageFile = await _fileRepository.GetById(dto.ImageFileId);
        Handle? handle = null;
        if (dto.HandleId.HasValue)
        {
            handle = await _handleRepository.GetById(dto.HandleId.Value);
        }
        Sheath? sheath = null;
        if (dto.SheathId.HasValue)
        {
            sheath = await _sheathRepository. GetById(dto.SheathId.Value);
        }
        SheathColor? sheathColor = null;
        if (dto.SheathColorId.HasValue)
        {
            sheathColor = await _sheathColorRepository. GetById(dto.SheathColorId.Value);
        }
        var tags = new List<ProductTag>();
        foreach (var tagId in dto.TagsIds)
        {
            var tag = await _tagRepository. GetById(tagId);
            tags.Add(tag);
        }
        var existingEngravings = new List<Engraving>();
        foreach (var engravingId in dto.ExistingEngravingIds)
        {
            var engraving = await _engravingRepository. GetById(engravingId);
            existingEngravings.Add(engraving);
        }
        var newEngravings = new List<Engraving>();
        foreach (var engravingDto in dto.NewEngravings)
        {
            var engraving = await _engravingDtoMapper.Map(engravingDto);
            newEngravings.Add(engraving);
        }
        var allEngravings = existingEngravings.Concat(newEngravings).ToList();
        var allAttachments = new List<Attachment>();
        foreach (var attachmentId in dto.ExistingAttachmentIds)
        {
            var attachment = await _attachmentRepository. GetById(attachmentId);
            allAttachments.Add(attachment);
        }
        
        var name = new Translations(dto.Names);
        var title = new Translations(dto.Titles);
        var description = new Translations(dto.Descriptions);
        var metaTitle = new Translations(dto.MetaTitles);
        var metaDescription = new Translations(dto.MetaDescriptions);
        
        Knife knife = new Knife(
            id,
            dto.IsActive,
            imageFile,
            name,
            title,
            description,
            metaTitle,
            metaDescription,
            tags,
            shape,
            bladeCoatingColor,
            handle,
            sheath,
            sheathColor,
            allEngravings,
            allAttachments
        );

        double engravingPrice = (await this._getEngravingPrice.GetPrice("uah")).Price;
        knife.CalculateTotalPriceInUah(engravingPrice);

        return knife;
    }
}