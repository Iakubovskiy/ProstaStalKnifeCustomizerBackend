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
            return Ok(new { deliveryTypes = await _deliveryTypeService.GetAllDeliveryTypes() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryTypesById(int id)
        {
            return Ok(new { type = await _deliveryTypeService.GetDeliveryTypeById(id) });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryType([FromForm] DeliveryType type)
        {
            return Ok(new { createdType = await _deliveryTypeService.CreateDeliveryType(type) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeliveryType(int id, [FromForm] DeliveryType type)
        {
            return Ok(new { updatedType = await _deliveryTypeService.UpdateDeliveryType(id, type) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryType(int id)
        {
            return Ok(new { isDeleted = await _deliveryTypeService.DeleteDeliveryType(id) });
        }
    }
}
