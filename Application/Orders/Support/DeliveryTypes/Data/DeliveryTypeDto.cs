namespace Application.Orders.Support.DeliveryTypes.Data;

public class DeliveryTypeDto
{
    private DeliveryTypeDto(
        string namesJson,
        string? commentJson,
        double price,
        bool isActive,
        Guid? id = null    
    )
    {
        this.NamesJson = namesJson;
        this.CommentJson = commentJson;
        this.Price = price;
        this.IsActive = isActive;
        this.Id = id;
    }
    public Guid? Id { get; private set; }
    public string NamesJson { get; private set; }
    public string? CommentJson { get; private set; }
    public double Price { get; private set; }
    public bool IsActive { get; private set; }
}