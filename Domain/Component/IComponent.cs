namespace Domain.Component;

public interface IComponent
{
    public bool IsActive { get; }
    public double GetPrice(double exchangerRate);
    
    public void Activate();
    public void Deactivate();
}