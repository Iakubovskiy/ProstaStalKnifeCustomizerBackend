using System.Data.Entity.Core;
using System.Security.Claims;
using API.Components.Products.AllProducts.Presenters;
using Application.Components.Products.Reviews;
using Domain.Component.Product.Reviews;
using Infrastructure;
using Infrastructure.Components.Products;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Components.Products.Reviews;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IProductRepository _productRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly ProductPresenter _presenter;

    public ReviewController(
        IReviewService reviewService,
        IProductRepository productRepository,
        IRepository<Review> reviewRepository,
        ProductPresenter presenter
    )
    {
        this._reviewService = reviewService;
        this._productRepository = productRepository;
        this._reviewRepository = reviewRepository;
        this._presenter = presenter;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("/api/products/{productId:guid}/reviews")]
    public async Task<IActionResult> CreateReview(
        Guid productId, 
        [FromBody]ReviewDto review, 
        [FromHeader(Name = "Currency")] string currency, 
        [FromHeader(Name = "Locale")] string locale 
    )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "User not authenticated" });

        Guid id = new Guid(userId);
        if (!await this._reviewService.IsReviewAllowed(productId, id))
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "User is not allowed to leave comment here" });
        }
        
        await this._reviewService.AddReviewToProduct(productId, Guid.Parse(userId),review);
        return Ok( await this._presenter.Present(await this._productRepository.GetById(productId), locale, currency));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "User not authenticated" });
        try
        {
            Review review = await this._reviewRepository.GetById(id);
            if (review.User.Id != new Guid(userId))
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "User is not allowed to delete this review" });
            
            return Ok(new { isDeleted = await this._reviewRepository.Delete(id) });
        }
        catch (ObjectNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new { message = e.Message });
        }
    }

    [HttpGet("product/{productId:guid}/allowed/{userId:guid}")]
    public async Task<IActionResult> IsReviewAllowed(Guid userId, Guid productId)
    {
        return Ok(await this._reviewService.IsReviewAllowed(productId, userId));
    }
}