using Domain.Component.Product.Reviews;

namespace Infrastructure.Components.Products;

public interface IAddReviewToProductRepository
{
    public Task AddReview(Guid productId, Review review);
}