using System.Data.Entity.Core;
using API.Components.Products.Attachments.Presenters;
using Application.Components.Prices;
using Application.Components.Products.Attachments;
using Application.Components.Products.UseCases.Activate;
using Application.Components.Products.UseCases.Create;
using Application.Components.Products.UseCases.Deactivate;
using Application.Components.Products.UseCases.Update;
using Microsoft.AspNetCore.Mvc;
using Domain.Component.Product.Attachments;
using Infrastructure.Components;

namespace API.Components.Products.Attachments;

[Route("api/attachments")]
[ApiController]
public class AttachmentController : ControllerBase
{
    private readonly ICreateProductService< Attachment, AttachmentDto> _createAttachmentService;
    private readonly IUpdateProductService< Attachment, AttachmentDto> _updateProductService;
    private readonly IComponentRepository< Attachment> _attachmentRepository;
    private readonly IActivateProduct<Attachment> _activateProductService;
    private readonly IDeactivateProduct<Attachment> _deactivateProductService;
    private readonly IGetComponentPrice _getComponentPrice;

    public AttachmentController(
        ICreateProductService<Attachment, AttachmentDto> createAttachmentService, 
        IUpdateProductService<Attachment, AttachmentDto> updateProductService, 
        IComponentRepository<Attachment> attachmentRepository, 
        IActivateProduct<Attachment> activateProductService, 
        IDeactivateProduct<Attachment> deactivateProductService,
        IGetComponentPrice getComponentPrice
    )
    {
        this._createAttachmentService = createAttachmentService;
        this._updateProductService = updateProductService;
        this._attachmentRepository = attachmentRepository;
        this._activateProductService = activateProductService;
        this._deactivateProductService = deactivateProductService;
        this._getComponentPrice = getComponentPrice;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAttachments(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await AttachmentPresenter
            .PresentList(await this._attachmentRepository.GetAll(), locale, currency, this._getComponentPrice));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAllActiveAttachments(
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await AttachmentPresenter
            .PresentList(await this._attachmentRepository.GetAllActive(), locale, currency, this._getComponentPrice));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAttachmentsById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        try
        {
            return Ok(await AttachmentPresenter
                .PresentWithTranslations(await this._attachmentRepository.GetById(id), locale, currency, this._getComponentPrice));
        }
        catch (ObjectNotFoundException)
        {
            return NotFound("Can't find attachment");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAttachment([FromBody] AttachmentDto createAttachment)
    {
        return Created(nameof(GetAttachmentsById), await this._createAttachmentService.Create(
                createAttachment
            )
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAttachment(Guid id, [FromBody] AttachmentDto updateAttachment)
    {
        try
        {
            return Ok(await this._updateProductService.Update(id, updateAttachment));
        }
        catch (Exception)
        {
            return NotFound("Can't find attachment");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAttachment(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._attachmentRepository.Delete(id) });
        }
        catch (Exception)
        {
            return NotFound("Can't find attachment");
        }
    }

    [HttpPatch("deactivate/{id:guid}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await this._deactivateProductService.Deactivate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find attachment");
        }
    }

    [HttpPatch("activate/{id:guid}")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            await this._activateProductService.Activate(id);
            return Ok();
        }
        catch (Exception)
        {
            return NotFound("Can't find attachment");
        }
    }
}