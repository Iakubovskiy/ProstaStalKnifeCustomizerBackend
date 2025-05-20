using Domain.Component.BladeShapeTypes;

namespace Domain.Component;

public interface IComponentWithTypeDependency
{
    public bool IsActive { get; }
    
    public double GetPrice(BladeShapeType type);
}