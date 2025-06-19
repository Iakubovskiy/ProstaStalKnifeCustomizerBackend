using Domain.Component.Product.Reviews;

namespace API.Components.Products.AllProducts.Presenters;

public class ReviewPresenter
{
    public string Comment { get; set; }
    public int Rating { get; set; }
    public string Client { get; set; }

    public ReviewPresenter Present(Review review)
    {
        this.Comment = review.Comment;
        this.Rating = review.Rating;
        this.Client = review.User.Email ?? review.User.UserData?.ClientFullName ?? "";
        
        return this;
    }

    public List<ReviewPresenter> PresentList(List<Review> reviews)
    {
        List<ReviewPresenter> presenters = new List<ReviewPresenter>();
        foreach (Review review in reviews)
        {
            ReviewPresenter presenter = new ReviewPresenter();
            presenter.Present(review);
            presenters.Add(presenter);
        }
        return presenters;
    }
}