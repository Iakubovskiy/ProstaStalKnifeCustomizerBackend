namespace Domain.Component;

public interface IComponent
{
    public bool IsActive { get; set; }
    public double GetPrice();
}