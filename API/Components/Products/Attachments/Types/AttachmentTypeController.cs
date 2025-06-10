using System.Data.Entity.Core;
using Application.Components.Products.Attachments.Type;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Product.Attachments;
using Domain.Translation;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.Attachments.Types;

[ApiController]
[Route("api/attachment-types")]
public class AttachmentTypeController : ControllerBase
{
    private readonly IRepository<AttachmentType> _attachmentTypeRepository;
    private readonly ISimpleCreateService<AttachmentType, AttachmentTypeDto> _simpleCreateAttachmentTypeService;

    public AttachmentTypeController(
        IRepository<AttachmentType> attachmentTypeRepository,
        ISimpleCreateService<AttachmentType, AttachmentTypeDto> simpleCreateAttachmentTypeService
    )
    {
        this._attachmentTypeRepository = attachmentTypeRepository;
        this._simpleCreateAttachmentTypeService = simpleCreateAttachmentTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttachmentTypes()
    {
        return Ok(await this._attachmentTypeRepository.GetAll());
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAttachmentTypeById(Guid id)
    {
        try
        {
            return Ok(await this._attachmentTypeRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound("Entity not found");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttachmentType([FromBody]  AttachmentTypeDto attachmentType)
    {
        return Created(nameof(GetAttachmentTypeById), await this._simpleCreateAttachmentTypeService.Create(attachmentType));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAttachmentType(Guid id)
    {
        return Ok(await this._attachmentTypeRepository.Delete(id));
    }
}