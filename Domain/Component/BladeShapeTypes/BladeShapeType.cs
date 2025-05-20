using Domain.Component.Sheaths.Color;

namespace Domain.Component.BladeShapeTypes;

public class BladeShapeType : IEntity
{
    private BladeShapeType()
    {
        
    }

    public BladeShapeType(Guid id, string name)
    {
        this.Id = id;
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required");
        }
        this.Name = name;
    }
    
    public Guid Id { get; private set;}
    public string Name { get; private set;}
    
    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required");
        }
        this.Name = name;
    }

    public bool Equals(BladeShapeType other)
    {
        return this.Id == other.Id;
    }

    public static bool operator == (BladeShapeType first, BladeShapeType second)
    {
        return first.Equals(second);
    }
    
    public static bool operator != (BladeShapeType first, BladeShapeType second)
    {
        return !first.Equals(second);
    }
}