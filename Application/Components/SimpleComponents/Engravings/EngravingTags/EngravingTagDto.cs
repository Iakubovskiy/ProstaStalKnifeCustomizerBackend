namespace Application.Components.SimpleComponents.Engravings.EngravingTags;

public class EngravingTagDto
{
    public EngravingTagDto(
        Dictionary<string, string> name
    )
    {
        this.Name = name;
    }
    
    public Dictionary<string, string> Name { get; private set; }
}