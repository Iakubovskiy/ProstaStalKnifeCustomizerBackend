using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Files;
using Domain.Translation;
using Infrastructure;

namespace Application.Components.Products.Attachments;

public class AttachmentDtoMapper : IProductDtoMapper<Attachment, AttachmentDto>
{
    private readonly IRepository<FileEntity> _fileRepository;
    private readonly IRepository<ProductTag> _tagRepository;
    private readonly IRepository<AttachmentType> _attachmentTypeRepository;

    public AttachmentDtoMapper(
        IRepository<FileEntity> fileRepository,
        IRepository<ProductTag> tagRepository,
        IRepository<AttachmentType> attachmentTypeRepository)
    {
        _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
        _tagRepository = tagRepository ?? throw new ArgumentNullException(nameof(tagRepository));
        _attachmentTypeRepository = attachmentTypeRepository ?? throw new ArgumentNullException(nameof(attachmentTypeRepository));
    }

    public async Task<Attachment> Map(AttachmentDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        Guid id = dto.Id ?? Guid.NewGuid();

        var imageFile = await _fileRepository.GetById(dto.ImageFileId);
        var modelFile = await _fileRepository.GetById(dto.ModelFileId);
        var attachmentType = await _attachmentTypeRepository.GetById(dto.TypeId);
        if (attachmentType == null)
        {
            throw new InvalidOperationException($"AttachmentType with id {dto.TypeId} not found.");
        }

        var tags = new List<ProductTag>();
        
        
        foreach (var tagId in dto.TagsIds)
        {
            var tag = await _tagRepository.GetById(tagId);
            tags.Add(tag);
        }
        

        var name = new Translations(dto.Names);
        var title = new Translations(dto.Titles);
        var description = new Translations(dto.Descriptions);
        var metaTitle = new Translations(dto.MetaTitles);
        var metaDescription = new Translations(dto.MetaDescriptions);
        var color = new Translations(dto.Colors);
        var material = new Translations(dto.Materials);
        DateTime createdAt = DateTime.Now;

        return new Attachment(
            id,
            dto.IsActive,
            imageFile,
            name,
            title,
            description,
            metaTitle,
            metaDescription,
            tags,
            attachmentType,
            color,
            dto.Price,
            material,
            modelFile,
            createdAt
        );
    }
}