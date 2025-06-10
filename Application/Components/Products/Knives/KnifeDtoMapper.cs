using Application.Components.Products.Attachments;
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
    private readonly IProductDtoMapper<Attachment, AttachmentDto> _attachmentDtoMapper;

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
        IProductDtoMapper<Attachment, AttachmentDto> attachmentDtoMapper
    )
    {
        _bladeShapeRepository = bladeShapeRepository;
        _bladeCoatingColorRepository = bladeCoatingColorRepository;
        _handleRepository = handleRepository;
        _sheathRepository = sheathRepository;
        _sheathColorRepository = sheathColorRepository;
        _engravingRepository = engravingRepository;
        _attachmentRepository = attachmentRepository;
        _tagRepository = tagRepository;
        _fileRepository = fileRepository;
        _engravingDtoMapper = engravingDtoMapper;
        _attachmentDtoMapper = attachmentDtoMapper;
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
        var existingAttachments = new List<Attachment>();
        foreach (var attachmentId in dto.ExistingAttachmentIds)
        {
            var attachment = await _attachmentRepository. GetById(attachmentId);
            existingAttachments.Add(attachment);
        }

        var newAttachments = new List<Attachment>();
        foreach (var attachmentDto in dto.NewAttachments)
        {
            var attachment = await _attachmentDtoMapper.Map(attachmentDto);
            newAttachments.Add(attachment);
        }

        var allAttachments = existingAttachments.Concat(newAttachments).ToList();

        var name = new Translations(dto.Name);
        var title = new Translations(dto.Title);
        var description = new Translations(dto.Description);
        var metaTitle = new Translations(dto.MetaTitle);
        var metaDescription = new Translations(dto.MetaDescription);
        
        return new Knife(
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
    }
}