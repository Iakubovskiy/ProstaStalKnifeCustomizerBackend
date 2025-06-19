using Domain.Component.Product;
using Domain.Component.Product.Reviews;
using Domain.Orders;
using Domain.Users;
using Infrastructure;
using Infrastructure.Components.Products;
using Infrastructure.Users;

namespace Application.Components.Products.Reviews;

public class ReviewService : IReviewService
{
    private readonly IRepository<Review> _reviewRepository;
    private readonly IGetUserWithOrder _userRepository;
    private readonly IAddReviewToProductRepository _addReviewToProductRepository;
    private readonly IProductRepository _productRepository;

    public ReviewService(
        IRepository<Review> reviewRepository, 
        IAddReviewToProductRepository addReviewToProductRepository,
        IGetUserWithOrder userRepository,
        IProductRepository productRepository
    )
    {
        this._reviewRepository = reviewRepository;
        this._addReviewToProductRepository = addReviewToProductRepository;
        this._userRepository = userRepository;
        this._productRepository = productRepository;
    }

    public async Task AddReviewToProduct(Guid productId, Guid userId, ReviewDto reviewDto)
    {
        User user = await _userRepository.GetUserWithOrder(userId);
        Review review = new Review(reviewDto.Id, reviewDto.Comment, reviewDto.Rating, user);
        await this._reviewRepository.Create(review);
        
        await this._addReviewToProductRepository.AddReview(productId, review);
    }

    public async Task<bool> IsReviewAllowed(Guid productId, Guid userId)
    {
        Product product = await this._productRepository.GetById(productId);
        User user = await this._userRepository.GetUserWithOrder(userId);
        if (product.Reviews != null && product.Reviews.Select(r => r.User).ToList().Contains(user))
        {
            return false;
        }
        
        List<OrderItem> items = user.Orders.SelectMany(o => o.OrderItems).ToList();
        List<Product> products = items.Select(p => p.Product).ToList();

        if (products.All(p => p.Id != productId))
        {
            return false;
        }
        
        return true;
    }
}