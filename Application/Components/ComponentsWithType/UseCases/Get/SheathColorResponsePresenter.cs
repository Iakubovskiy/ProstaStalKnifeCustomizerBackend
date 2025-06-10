namespace Application.Components.ComponentsWithType.UseCases.Get;

public class SheathColorResponsePresenter
{
    public Guid Id { get; set; }
    public Dictionary<string, string> Color { get; set; }
    public Dictionary<string, string> Material { get; set; }
    public string? ColorCode { get; set; }
    public string EngravingColorCode { get; set; }
    public bool IsActive { get; set; }
    public Guid? TextureId { get; set; }
    public Guid? ColorMapId { get; set; }
    public List<SheathColorPriceResponsePresenter> Prices { get; set; }
}