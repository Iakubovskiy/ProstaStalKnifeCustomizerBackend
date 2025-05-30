using Domain.Component.Product.Attachments;
using Domain.Translation;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AttachmentTypeController : ControllerBase
{
    private readonly IRepository<AttachmentType> _attachmentTypeRepository;

    public AttachmentTypeController(IRepository<AttachmentType> attachmentTypeRepository)
    {
        this._attachmentTypeRepository = attachmentTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttachmentTypes()
    {
        return Ok(await this._attachmentTypeRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAttachmentTypeById(Guid id)
    {
        return Ok(await this._attachmentTypeRepository.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttachmentType([FromForm] Dictionary<string, string> names)
    {
        Translations nameTranslations = new Translations(names);
        AttachmentType type = new AttachmentType(Guid.NewGuid(), nameTranslations);
        return Ok(await this._attachmentTypeRepository.Create(type));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAttachmentType(Guid id)
    {
        return Ok(await this._attachmentTypeRepository.Delete(id));
    }
}