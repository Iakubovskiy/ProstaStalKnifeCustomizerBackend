using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Component.Engravings.Parameters;
using Domain.Component.Engravings.Support;
using Domain.Component.Translation;
using Domain.Component.Engravings.Validators;

namespace Domain.Component.Engravings;

public class Engraving : IEntity, IComponent
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
        string? pictureUrl,
        EngravingPosition engravingPosition, 
        EngravingRotation engravingRotation, 
        EngravingScale engravingScale,
        List<EngravingTag> tags,
        Translations description,
        EngravingPrice price,
        bool isActive
    )
    {
        EngravingVlaidator.Validate(side, text, font, pictureUrl);
        this.Id = id;
        this.Name = name;
        this.Side = side;
        this.Text = text;
        this.Font = font;
        this.PictureUrl = pictureUrl;
        this.EngravingPosition = engravingPosition;
        this.EngravingRotation = engravingRotation;
        this.EngravingScale = engravingScale;
        this.Tags = tags;
        this.Description = description;
        this.IsActive = isActive;
        this.Price = price.Price;
    }
    
    public Guid Id { get; private set; }
    [Required]
    public Translations Name { get; private set; }
    [Required]
    public int Side { get; private set; }
    public string? Text { get; private set; }
    public string? Font { get; private set; }
    public string? PictureUrl { get; private set; }
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

    public bool IsActive { get; }
    
    public double GetPrice()
    {
        return Price;
    }

    public void Update(Engraving engraving)
    {
        EngravingVlaidator.Validate(
            engraving.Side, 
            engraving.Text, 
            engraving.Font, 
            engraving.PictureUrl
        );
        
        this.Side = engraving.Side;
        this.Text = engraving.Text;
        this.Font = engraving.Font;
        this.PictureUrl = engraving.PictureUrl;
        this.EngravingPosition = engraving.EngravingPosition;
        this.EngravingRotation = engraving.EngravingRotation;
        this.EngravingScale = engraving.EngravingScale;
        this.Tags = engraving.Tags;
    }
}