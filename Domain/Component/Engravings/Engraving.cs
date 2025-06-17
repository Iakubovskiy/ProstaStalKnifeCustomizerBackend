using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Component.Engravings.Parameters;
using Domain.Component.Engravings.Support;
using Domain.Translation;
using Domain.Component.Engravings.Validators;
using Domain.Files;

namespace Domain.Component.Engravings;

public class Engraving : IEntity, IComponent, IUpdatable<Engraving>
{
    private Engraving()
    {
        
    }

    public Engraving(
        Guid id, 
        Translations name, 
        int side, 
        string? text, 
        string? font, 
        FileEntity? picture,
        EngravingPosition engravingPosition, 
        EngravingRotation engravingRotation, 
        EngravingScale engravingScale,
        List<EngravingTag> tags,
        Translations description,
        double price,
        bool isActive = true
    )
    {
        EngravingVlaidator.Validate(side, text, font, picture);
        this.Id = id;
        this.Name = name;
        this.Side = side;
        this.Text = text;
        this.Font = font;
        this.Picture = picture;
        this.EngravingPosition = engravingPosition;
        this.EngravingRotation = engravingRotation;
        this.EngravingScale = engravingScale;
        this.Tags = tags;
        this.Description = description;
        this.IsActive = isActive;
        this.Price = price;
    }
    
    public Guid Id { get; private set; }
    [Required]
    public Translations Name { get; private set; }
    [Required]
    public int Side { get; private set; }
    [MaxLength(255)]
    public string? Text { get; private set; }
    [MaxLength(100)]
    public string? Font { get; private set; }
    public FileEntity? Picture { get; private set; }
    [Required]
    public EngravingPosition EngravingPosition { get; private set; }
    [Required]
    public EngravingRotation EngravingRotation { get; private set; }
    [Required]
    public EngravingScale EngravingScale { get; private set; }
    [Required]
    public List<EngravingTag> Tags { get; private set; }
    [Required]
    public Translations Description { get; private set; }
    
    [NotMapped]
    public double Price { get; private set; }

    public bool IsActive { get; private set;}
    
    public double GetPrice(double exchangerRate)
    {
        return Math.Ceiling(Price / exchangerRate);
    }

    public void Update(Engraving engraving)
    {
        EngravingVlaidator.Validate(
            engraving.Side, 
            engraving.Text, 
            engraving.Font, 
            engraving.Picture
        );
        
        this.Side = engraving.Side;
        this.Text = engraving.Text;
        this.Font = engraving.Font;
        this.Picture = engraving.Picture;
        this.EngravingPosition = engraving.EngravingPosition;
        this.EngravingRotation = engraving.EngravingRotation;
        this.EngravingScale = engraving.EngravingScale;
        this.Tags = engraving.Tags;
        this.Description = engraving.Description;
        this.Price = engraving.Price;
        this.IsActive = engraving.IsActive;
        this.Name = engraving.Name;
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