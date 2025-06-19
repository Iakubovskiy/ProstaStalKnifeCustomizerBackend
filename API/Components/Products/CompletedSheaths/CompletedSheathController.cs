using API.Components.Products.CompletedSheaths.Presenters;
using Domain.Component.Product.CompletedSheath;
using Infrastructure.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.CompletedSheaths;

[ApiController]
[Route("products/completed-sheath")]
public class CompletedSheathController : ControllerBase
{
    private readonly IComponentRepository<CompletedSheath> _completedSheathRepository;
    private readonly CompletedSheathPresenter _completedSheathPresenter;

    public CompletedSheathController(
        IComponentRepository<CompletedSheath> completedSheathRepository,
        CompletedSheathPresenter completedSheathPresenter
    )
    {
        this._completedSheathRepository = completedSheathRepository;
        this._completedSheathPresenter = completedSheathPresenter;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompletedSheathById(
        Guid id,
        [FromHeader(Name = "Locale")] string locale,
        [FromHeader(Name = "Currency")] string currency
    )
    {
        return Ok(await this._completedSheathPresenter.Present(await this._completedSheathRepository.GetById(id), locale, currency));
    }
}