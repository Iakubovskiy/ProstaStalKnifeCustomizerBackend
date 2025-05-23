using System.ComponentModel.DataAnnotations;
using Domain.Component.BladeShapeTypes;
using Domain.Component.Translation;

namespace Domain.Component.Sheaths;

public class Sheath : IEntity, IComponent, IUpdatable<Sheath>
{
    private Sheath()
    {
        
    }
    
    public Sheath(
        Guid id, 
        Translations name, 
        string? modelUrl, 
        BladeShapeType type, 
        double price,
        bool isActive 
    )
    {
        if (!Uri.IsWellFormedUriString(modelUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid sheath model URL");
        }
        this.Id = id;
        this.Name = name;
        this.ModelUrl = modelUrl;
        this.Type = type;
        this.Price = price;
        this.IsActive = isActive;
    }
    
    public Guid Id { get; private set;  }
    public Translations Name { get; private set; }
    public bool IsActive { get; set; }
    [MaxLength(255)]
    public string? ModelUrl { get; private set; }
    public BladeShapeType Type { get; private set; }
    public double Price { get; private set; }
    
    public double GetPrice(double exchangerRate)
    {
        return this.Price / exchangerRate;
    }

    public void Update(Sheath sheath)
    {
        if (!Uri.IsWellFormedUriString(sheath.ModelUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid sheath model URL");
        }
        
        this.Name = sheath.Name;
        this.ModelUrl = sheath.ModelUrl;
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