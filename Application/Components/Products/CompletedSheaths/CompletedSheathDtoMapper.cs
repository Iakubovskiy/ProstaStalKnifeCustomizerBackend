using Application.Components.Products.Attachments;
using Application.Components.SimpleComponents.Engravings;
using Application.Components.SimpleComponents.UseCases;
using Domain.Component.Engravings;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Sheaths;
using Domain.Files;
using Domain.Translation;
using Infrastructure;
using Infrastructure.Components;
using Infrastructure.Components.Sheaths.Color;

namespace Application.Components.Products.CompletedSheaths;

public class CompletedSheathDtoMapper : IProductDtoMapper<CompletedSheath, CompletedSheathDto>
{
    private readonly IRepository<FileEntity> _fileRepository;
    private readonly IRepository<ProductTag> _tagRepository;
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly IComponentRepository<Engraving> _engravingRepository;
    private readonly IComponentRepository<Attachment> _attachmentRepository;
    private readonly IComponentDtoMapper<Engraving, EngravingDto> _engravingDtoMapper;
    private readonly IProductDtoMapper<Attachment, AttachmentDto> _attachmentDtoMapper;

    public CompletedSheathDtoMapper(
        IRepository<FileEntity> fileRepository,
        IRepository<ProductTag> tagRepository,
        IComponentRepository<Sheath> sheathRepository,
        ISheathColorRepository sheathColorRepository,
        IComponentRepository<Engraving> engravingRepository,
        IComponentRepository<Attachment> attachmentRepository,
        IComponentDtoMapper<Engraving, EngravingDto> engravingDtoMapper,
        IProductDtoMapper<Attachment, AttachmentDto> attachmentDtoMapper
    )
    {
        this._fileRepository = fileRepository;
        this._tagRepository = tagRepository;
        this._sheathRepository = sheathRepository;
        this._sheathColorRepository = sheathColorRepository;
        this._engravingRepository = engravingRepository;
        this._attachmentRepository = attachmentRepository;
        this._engravingDtoMapper = engravingDtoMapper;
        this._attachmentDtoMapper = attachmentDtoMapper;
    }

    public async Task<CompletedSheath> Map(CompletedSheathDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        Guid id = dto.Id ?? Guid.NewGuid();
        var imageFile = await this._fileRepository.GetById(dto.ImageFileId);
        var baseSheath = await this._sheathRepository.GetById(dto.SheathId);
        var sheathColor = await this._sheathColorRepository.GetById(dto.SheathColorId);

        var tags = new List<ProductTag>();
        foreach (var tagId in dto.TagsIds)
        {
            var tag = await this._tagRepository.GetById(tagId);
            tags.Add(tag);
        }
        
        var allEngravings = new List<Engraving>();
        foreach (var engravingId in dto.ExistingEngravingIds)
        {
            var engraving = await this._engravingRepository.GetById(engravingId);
            allEngravings.Add(engraving);
        }
        foreach (var engravingDto in dto.NewEngravings)
        {
            var newEngraving = await this._engravingDtoMapper.Map(engravingDto);
            allEngravings.Add(newEngraving);
        }
        
        var allAttachments = new List<Attachment>();
        foreach (var attachmentId in dto.ExistingAttachmentIds)
        {
            var attachment = await this._attachmentRepository.GetById(attachmentId);
            allAttachments.Add(attachment);
        }
        foreach (var attachmentDto in dto.NewAttachments)
        {
            var newAttachment = await this._attachmentDtoMapper.Map(attachmentDto);
            allAttachments.Add(newAttachment);
        }
        

        var name = new Translations(dto.Name);
        var title = new Translations(dto.Title);
        var description = new Translations(dto.Description);
        var metaTitle = new Translations(dto.MetaTitle);
        var metaDescription = new Translations(dto.MetaDescription);

        return new CompletedSheath(
            id,
            dto.IsActive,
            imageFile,
            name,
            title,
            description,
            metaTitle,
            metaDescription,
            tags,
            baseSheath,
            sheathColor,
            allEngravings,
            allAttachments
        );
    }
}