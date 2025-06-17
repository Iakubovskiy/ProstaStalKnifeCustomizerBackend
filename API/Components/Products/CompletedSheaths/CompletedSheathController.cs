using Domain.Component.Product.CompletedSheath;
using Infrastructure.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.CompletedSheaths;

[ApiController]
[Route("products/completed-sheath")]
public class CompletedSheathController : ControllerBase
{
    private readonly IComponentRepository<CompletedSheath> _completedSheathRepository;

    public CompletedSheathController(IComponentRepository<CompletedSheath> completedSheathRepository)
    {
        this._completedSheathRepository = completedSheathRepository;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompletedSheathById(Guid id)
    {
        CompletedSheath completedSheath = await this._completedSheathRepository.GetById(id);
        return Ok(await this._completedSheathRepository.GetById(id));
    }
}