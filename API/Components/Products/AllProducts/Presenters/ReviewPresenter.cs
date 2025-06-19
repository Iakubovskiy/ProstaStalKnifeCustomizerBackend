using Domain.Component.Product.Reviews;

namespace API.Components.Products.AllProducts.Presenters;

public class ReviewPresenter
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public string Client { get; set; }

    public static ReviewPresenter Present(Review review)
    {
        return new ReviewPresenter
        {
            Comment = review.Comment,
            Rating = review.Rating,
            Client = review.User.Email ?? review.User.UserData?.ClientFullName ?? ""
        };
    }

    public static List<ReviewPresenter> PresentList(List<Review> reviews)
    {
        var presenters = new List<ReviewPresenter>();
        foreach (Review review in reviews)
        {
            ReviewPresenter presenter = Present(review);
            presenters.Add(presenter);
        }
        return presenters;
    }
}