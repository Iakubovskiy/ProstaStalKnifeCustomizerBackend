using Domain.Translation;

namespace Domain.Component.Engravings.Support;

public class EngravingTag : IEntity, IUpdatable<EngravingTag>
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

    public void Update(EngravingTag tag)
    {
        this.Name = tag.Name;
    }
}