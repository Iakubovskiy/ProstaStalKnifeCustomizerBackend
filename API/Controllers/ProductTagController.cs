using Application.Components.SimpleComponents.Products.ProductTags;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Product;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTagController : ControllerBase
{
    private readonly IRepository<ProductTag> _productRepository;
    private readonly ICreateService<ProductTag, ProductTagDto> _createProductTagService;
    private readonly IUpdateService<ProductTag, ProductTagDto> _updateProductTagService;
    
    public ProductTagController(
        IRepository<ProductTag> productRepository,
        ICreateService<ProductTag, ProductTagDto> createProductTagService,
        IUpdateService<ProductTag, ProductTagDto> updateProductTagService
    )
    {
        this._productRepository = productRepository;
        this._createProductTagService = createProductTagService;
        this._updateProductTagService = updateProductTagService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProductTags()
    {
        return Ok(await this._productRepository.GetAll());
    }
    
    [HttpGet ("{id:guid}")]
    public async Task<IActionResult> GetProductsById(Guid id)
    {
        try
        {
            return Ok( await this._productRepository.GetById(id));
        }
        catch (Exception)
        {
            return BadRequest("Can't find product");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductTag([FromBody] ProductTagDto newProduct)
    {
        return Ok(await this._createProductTagService.Create(newProduct));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProductTag(Guid id, [FromBody] ProductTagDto newProduct)
    {
        try
        {
            return Ok(await this._updateProductTagService.Update(id, newProduct));
        }
        catch (Exception)
        {
            return BadRequest("Can't find product");
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProductTag(Guid id)
    {
        try
        {
            return Ok(new { isDeleted = await this._productRepository.Delete(id) });
        }
        catch (Exception)
        {
            return BadRequest("Can't find product");
        }
    }
}