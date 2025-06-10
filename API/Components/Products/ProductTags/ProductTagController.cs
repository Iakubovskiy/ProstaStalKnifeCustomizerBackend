using Application.Components.SimpleComponents.Products.ProductTags;
using Application.Components.SimpleComponents.UseCases.Create;
using Application.Components.SimpleComponents.UseCases.Update;
using Domain.Component.Product;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.ProductTags;

[Route("api/product-tags")]
[ApiController]
public class ProductTagController : ControllerBase
{
    private readonly IRepository<ProductTag> _productRepository;
    private readonly ISimpleCreateService<ProductTag, ProductTagDto> _simpleCreateProductService;
    private readonly ISimpleUpdateService<ProductTag, ProductTagDto> _updateProductTagService;
    
    public ProductTagController(
        IRepository<ProductTag> productRepository,
        ISimpleCreateService<ProductTag, ProductTagDto> simpleCreateProductService,
        ISimpleUpdateService<ProductTag, ProductTagDto> updateProductTagService
    )
    {
        this._productRepository = productRepository;
        this._simpleCreateProductService = simpleCreateProductService;
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
            return NotFound("Can't find product");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductTag([FromBody] ProductTagDto newProduct)
    {
        return Created(nameof(GetProductsById),await this._simpleCreateProductService.Create(newProduct));
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
            return NotFound("Can't find product");
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
            return NotFound("Can't find product");
        }
    }
}