using Domain.Component.Translation;

namespace Domain.Component.Engravings.Support;

public class EngravingTag : IEntity
{
    private EngravingTag()
    {
        
    }
    
    public EngravingTag(
        Guid id,
        Translations name
    )
    {
        this.Id = id;
        this.Name = name;
    }
    
    public Guid Id { get; private set; }
    public Translations Name { get; private set; }

    public void Update(Translations name)
    {
        this.Name = name;
    }
}