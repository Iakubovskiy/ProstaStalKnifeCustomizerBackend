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
        private readonly BladeCoatingService _bladeCoatingService;
        private readonly BladeCoatingColorService _bladeCoatingColorService;
        private readonly SheathColorService _sheathService;
        private readonly HandleColorService _handleColorService;
        private readonly BladeShapeService _bladeShapeService;
        private readonly FasteningService _fasteningService;
        private readonly EngravingService _engravingService;

        public KnifeController(
            KnifeService service,
            BladeCoatingService bladeCoatingService,
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
            _bladeCoatingService = bladeCoatingService;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKnifesById(int id)
        {
            return Ok(await _knifeService.GetKnifeById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateKnife([FromForm] KnifeDTO knife)
        {
            var fastenings = !string.IsNullOrEmpty(knife.FasteningJson)
            ? JsonSerializer.Deserialize<List<int>>(knife.FasteningJson)
                .Select(id => _fasteningService.GetFasteningById(id).Result)
                .ToList()
            : new List<Fastening>();

            var engravings = !string.IsNullOrEmpty(knife.EngravingsJson)
            ? JsonSerializer.Deserialize<List<int>>(knife.EngravingsJson)
                .Select(id => _engravingService.GetEngravingById(id).Result)
                .ToList()
            : new List<Engraving>();
            var newKnife = new Knife
            {
                Id = knife.Id,
                Shape = await _bladeShapeService.GetBladeShapeById(knife.ShapeId),
                BladeCoating = await _bladeCoatingService.GetBladeCoatingById(knife.BladeCoatingId),
                BladeCoatingColor = await _bladeCoatingColorService.GetBladeCoatingColorById(knife.BladeCoatingColorId),
                HandleColor = await _handleColorService.GetHandleColorById(knife.HandleColorId),
                SheathColor = await _sheathService.GetSheathColorById(knife.SheathColorId),
                Fastening = fastenings,
                Engravings = engravings,
                Quantity = knife.Quantity
            };
            return Ok(await _knifeService.CreateKnife(newKnife));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKnife(int id, [FromForm] KnifeDTO knifeDto)
        {
            var fastenings = !string.IsNullOrEmpty(knifeDto.FasteningJson)
            ? JsonSerializer.Deserialize<List<int>>(knifeDto.FasteningJson)
                .Select(id => _fasteningService.GetFasteningById(id).Result)
                .ToList()
            : new List<Fastening>();

            var engravings = !string.IsNullOrEmpty(knifeDto.EngravingsJson)
            ? JsonSerializer.Deserialize<List<int>>(knifeDto.EngravingsJson)
                .Select(id => _engravingService.GetEngravingById(id).Result)
                .ToList()
            : new List<Engraving>();

            var knife = new Knife
            {
                Id = id,
                Shape = await _bladeShapeService.GetBladeShapeById(knifeDto.ShapeId),
                BladeCoating = await _bladeCoatingService.GetBladeCoatingById(knifeDto.BladeCoatingId),
                BladeCoatingColor = await _bladeCoatingColorService.GetBladeCoatingColorById(knifeDto.BladeCoatingColorId),
                HandleColor = await _handleColorService.GetHandleColorById(knifeDto.HandleColorId),
                SheathColor = await _sheathService.GetSheathColorById(knifeDto.SheathColorId),
                Fastening = fastenings,
                Engravings = engravings,
                Quantity = knifeDto.Quantity
            };
            var updatedKnife = await _knifeService.UpdateKnife(id, knife);

            return Ok(updatedKnife);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnife(int id)
        {
            return Ok(new { isDeleted = await _knifeService.DeleteKnife(id) });
        }

        [HttpGet("price/{id}")]
        public async Task<IActionResult> GetKnifePrice(int id)
        {
            return Ok(new { price = await _knifeService.KnifePrice(id)});
        }
    }
}
