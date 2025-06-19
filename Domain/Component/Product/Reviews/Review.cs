using System.ComponentModel.DataAnnotations;
using Domain.Users;

namespace Domain.Component.Product.Reviews;

public class Review : IEntity, IUpdatable<Review>
{
    private Review()
    {
        
    }
    
    public Review(
        Guid id,
        string comment,
        int rating,
        User user
    )
    {
        if (rating is < 0 or > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "rating should be grater then 0 and less then 5");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new ArgumentException(nameof(comment));
        }
        
        this.Id = id;
        this.Comment = comment;
        this.Rating = rating;
        this.User = user;
    }
    public Guid Id { get; private set; }
    
    [MaxLength(700)]
    public string Comment { get; private set; }
    public int Rating { get; private set; }

    public User User { get; private set; }
    
    public void Update(Review review)
    {
        this.Comment = review.Comment;
        this.Rating = review.Rating;
    }
}