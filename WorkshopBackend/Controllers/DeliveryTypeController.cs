using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryTypesById(Guid id)
        {
            return Ok(await _deliveryTypeService.GetDeliveryTypeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryType([FromForm] DeliveryType type)
        {
            return Ok(await _deliveryTypeService.CreateDeliveryType(type));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliveryType(Guid id, [FromForm] DeliveryType type)
        {
            return Ok(await _deliveryTypeService.UpdateDeliveryType(id, type));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryType(Guid id)
        {
            return Ok(new { isDeleted = await _deliveryTypeService.DeleteDeliveryType(id) });
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            return Ok(await _deliveryTypeService.ChangeActive(id, false));
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            return Ok(await _deliveryTypeService.ChangeActive(id, true));
        }
    }
}
