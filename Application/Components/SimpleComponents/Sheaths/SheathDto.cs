namespace Application.Components.SimpleComponents.Sheaths;

public class SheathDto
{
    public SheathDto(
        Guid typeId,
        Dictionary<string, string> name,
        double price,
        Guid? sheathModelId,
        bool isActive
    )
    {
        this.TypeId = typeId;
        this.Name = name;
        this.Price = price;
        this.IsActive = isActive;
        this.SheathModelId = sheathModelId;
    }
    
    public Guid TypeId { get; private set; }
    public Dictionary<string, string> Name { get; private set; }
    public double Price { get; private set; }
    
    public Guid? SheathModelId { get; private set; }
    public bool IsActive { get; private set; }
}