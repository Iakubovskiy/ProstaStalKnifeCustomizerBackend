namespace Application.Components.Products.Reviews;

public interface IReviewService
{
    public Task AddReviewToProduct(Guid productId, Guid userId, ReviewDto reviewDto);
    public Task<bool> IsReviewAllowed(Guid productId, Guid userId);
}