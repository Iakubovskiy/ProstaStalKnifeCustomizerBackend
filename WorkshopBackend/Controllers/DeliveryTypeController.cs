using Microsoft.AspNetCore.Mvc;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly DeliveryTypeService _deliveryTypeService;

        public DeliveryTypeController(DeliveryTypeService service)
        {
            _deliveryTypeService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeliveryTypes()
        {
            return Ok(await _deliveryTypeService.GetAllDeliveryTypes());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveDeliveryTypes()
        {
            return Ok(await _deliveryTypeService.GetAllActiveDeliveryTypes());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDeliveryTypesById(Guid id)
        {
            try
            {
                return Ok(await _deliveryTypeService.GetDeliveryTypeById(id));
            }
            catch (Exception)
            {
                return BadRequest("Can't find DeliveryType");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryType([FromForm] DeliveryType type)
        {
            return Ok(await _deliveryTypeService.CreateDeliveryType(type));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateDeliveryType(Guid id, [FromForm] DeliveryType type)
        {
            try
            {
                return Ok(await _deliveryTypeService.UpdateDeliveryType(id, type));
            }
            catch (Exception)
            {
                return BadRequest("Can't find delivery type");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDeliveryType(Guid id)
        {
            try
            {
                return Ok(new { isDeleted = await _deliveryTypeService.DeleteDeliveryType(id) });
            }
            catch (Exception)
            {
                return BadRequest("Can't find delivery type");
            }
        }

        [HttpPatch("deactivate/{id:guid}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                return Ok(await _deliveryTypeService.ChangeActive(id, false));
            }
            catch (Exception)
            {
                return BadRequest("Can't find delivery type");
            }
        }

        [HttpPatch("activate/{id:guid}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            try
            {
                return Ok(await _deliveryTypeService.ChangeActive(id, true));
            }
            catch (Exception)
            {
                return BadRequest("Can't find delivery type");
            }
        }
    }
}
