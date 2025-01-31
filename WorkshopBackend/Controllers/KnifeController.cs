using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WorkshopBackend.DTO;
using WorkshopBackend.Models;
using WorkshopBackend.Services;

namespace WorkshopBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnifeController : ControllerBase
    {
        private readonly KnifeService _knifeService;
        private readonly BladeCoatingColorService _bladeCoatingColorService;
        private readonly SheathColorService _sheathService;
        private readonly HandleColorService _handleColorService;
        private readonly BladeShapeService _bladeShapeService;
        private readonly FasteningService _fasteningService;
        private readonly EngravingService _engravingService;

        public KnifeController(
            KnifeService service,
            BladeCoatingColorService bladeCoatingColorService,
            SheathColorService sheathColorService,
            HandleColorService handleColorService,
            BladeShapeService bladeShapeService,
            FasteningService fasteningService,
            EngravingService engravingService
        )
        {
            _knifeService = service;
            _bladeCoatingColorService = bladeCoatingColorService;
            _bladeShapeService = bladeShapeService;
            _engravingService = engravingService;
            _fasteningService = fasteningService;
            _handleColorService = handleColorService;
            _sheathService = sheathColorService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllKnifes()
        {
            return Ok(await _knifeService.GetAllKnives());
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveKnifes()
        {
            return Ok(await _knifeService.GetAllActiveKnives());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKnifesById(Guid id)
        {
            return Ok(await _knifeService.GetKnifeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateKnife([FromForm] KnifeDTO knife)
        {
            var engravings = !string.IsNullOrEmpty(knife.EngravingsJson)
            ? JsonSerializer.Deserialize<List<Guid>>(knife.EngravingsJson)
                .Select(id => _engravingService.GetEngravingById(id).Result)
                .ToList()
            : new List<Engraving>();
            var newKnife = new Knife
            {
                Shape = await _bladeShapeService.GetBladeShapeById(knife.ShapeId),
                BladeCoatingColor = await _bladeCoatingColorService.GetBladeCoatingColorById(knife.BladeCoatingColorId),
                HandleColor = await _handleColorService.GetHandleColorById(knife.HandleColorId),
                SheathColor = await _sheathService.GetSheathColorById(knife.SheathColorId),
                Engravings = engravings,
            };
            if (knife.FasteningId != null && knife.FasteningId != Guid.Empty)
            {
                newKnife.Fastening = await _fasteningService.GetFasteningById(knife.FasteningId.Value);
            }
            return Ok(await _knifeService.CreateKnife(newKnife));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKnife(Guid id, [FromForm] KnifeDTO knifeDto)
        {
            var engravings = !string.IsNullOrEmpty(knifeDto.EngravingsJson)
            ? JsonSerializer.Deserialize<List<Guid>>(knifeDto.EngravingsJson)
                .Select(id => _engravingService.GetEngravingById(id).Result)
                .ToList()
            : new List<Engraving>();

            var knife = new Knife
            {
                Id = id,
                Shape = await _bladeShapeService.GetBladeShapeById(knifeDto.ShapeId),
                BladeCoatingColor = await _bladeCoatingColorService.GetBladeCoatingColorById(knifeDto.BladeCoatingColorId),
                HandleColor = await _handleColorService.GetHandleColorById(knifeDto.HandleColorId),
                SheathColor = await _sheathService.GetSheathColorById(knifeDto.SheathColorId),
                Engravings = engravings,
            };
            if (knifeDto.FasteningId != null && knifeDto.FasteningId != Guid.Empty)
            {
                knife.Fastening = await _fasteningService.GetFasteningById(knifeDto.FasteningId.Value);
            }
            var updatedKnife = await _knifeService.UpdateKnife(id, knife);

            return Ok(updatedKnife);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnife(Guid id)
        {
            return Ok(new { isDeleted = await _knifeService.DeleteKnife(id) });
        }

        [HttpGet("price/{id}")]
        public async Task<IActionResult> GetKnifePrice(Guid id)
        {
            return Ok(new { price = await _knifeService.KnifePrice(id)});
        }

        [HttpPatch("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            return Ok(await _knifeService.ChangeActive(id, false));
        }

        [HttpPatch("activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            return Ok(await _knifeService.ChangeActive(id, true));
        }
    }
}
