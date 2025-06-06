using System.ComponentModel.DataAnnotations;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Files;
using Domain.Translation;

namespace Domain.Component.Sheaths;

public class Sheath : IEntity, IComponent, IUpdatable<Sheath>
{
    private Sheath()
    {
        
    }
    
    public Sheath(
        Guid id, 
        Translations name, 
        FileEntity? model, 
        BladeShapeType type, 
        double price,
        bool isActive 
    )
    {
        this.Id = id;
        this.Name = name;
        this.Model = model;
        this.Type = type;
        this.Price = price;
        this.IsActive = isActive;
    }
    
    public Guid Id { get; private set;  }
    public Translations Name { get; private set; }
    public bool IsActive { get; set; }
    [MaxLength(255)]
    public FileEntity? Model { get; private set; }
    public BladeShapeType Type { get; private set; }
    public double Price { get; private set; }
    
    public double GetPrice(double exchangerRate)
    {
        return this.Price / exchangerRate;
    }

    public void Update(Sheath sheath)
    {
        this.Name = sheath.Name;
        this.Model = sheath.Model;
        this.Type = sheath.Type;
        this.Price = sheath.Price;
        this.IsActive = sheath.IsActive;
    }

    public void Activate()
    {
        this.IsActive = true;
    }

    public void Deactivate()
    {
        this.IsActive = false;
    }
}