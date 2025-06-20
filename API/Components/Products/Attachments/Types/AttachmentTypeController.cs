using System.Data.Entity.Core;
using API.Components.Products.Attachments.Types.Presenter;
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
    private readonly ISimpleUpdateService<AttachmentType, AttachmentTypeDto> _simpleUpdateAttachmentTypeService;

    public AttachmentTypeController(
        IRepository<AttachmentType> attachmentTypeRepository,
        ISimpleCreateService<AttachmentType, AttachmentTypeDto> simpleCreateAttachmentTypeService,
        ISimpleUpdateService<AttachmentType, AttachmentTypeDto> simpleUpdateAttachmentTypeService
    )
    {
        this._attachmentTypeRepository = attachmentTypeRepository;
        this._simpleCreateAttachmentTypeService = simpleCreateAttachmentTypeService;
        this._simpleUpdateAttachmentTypeService = simpleUpdateAttachmentTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttachmentTypes(
        [FromHeader(Name = "Locale")] string locale
    )
    {
        try
        {
            return Ok(await AttachmentTypePresenter.PresentList(await this._attachmentTypeRepository.GetAll(), locale));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAttachmentTypeById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale
    )
    {
        try
        {
            return Ok(await AttachmentTypePresenter
                .PresentWithTranslations(await this._attachmentTypeRepository.GetById(id), locale));
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
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAttachment(Guid id, [FromBody] AttachmentTypeDto updateAttachmentType)
    {
        try
        {
            return Ok(await this._simpleUpdateAttachmentTypeService.Update(id, updateAttachmentType));
        }
        catch (Exception)
        {
            return NotFound("Can't find attachment");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAttachmentType(Guid id)
    {
        return Ok(await this._attachmentTypeRepository.Delete(id));
    }
}