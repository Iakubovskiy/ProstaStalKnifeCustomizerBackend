namespace Application.Components.SimpleComponents.Sheaths;

public class SheathDto
{
    public Guid? Id { get; set; }
    public Guid TypeId { get; set; }
    public Dictionary<string, string> Name { get; set; }
    public double Price { get; set; }
    
    public Guid? SheathModelId { get; set; }
    public bool IsActive { get; set; }
}