﻿using System.ComponentModel.DataAnnotations;
using Domain.Component.BladeCoatingColors.Validators;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Domain.Component.BladeCoatingColors;

[Index(nameof(ColorCode))]
public class BladeCoatingColor : IEntity, IComponent, IUpdatable<BladeCoatingColor>, ITextured
{
    private BladeCoatingColor()
    {
        
    }
    
    public BladeCoatingColor(
        Guid id, 
        Translations type, 
        double price, 
        Translations color, 
        string colorCode, 
        string engravingColorCode, 
        Texture? texture, 
        FileEntity? colorMap,
        bool isActive = true 
    )
    {
        this.Id = id;
        this.Type = type;
        this.Price = price;
        this.Color = color;
        this.ColorCode = colorCode;
        this.EngravingColorCode = engravingColorCode;
        this.Texture = texture;
        this.ColorMap = colorMap;
        this.IsActive = isActive;
    }
    [BindNever]
    public Guid Id { get; private set; }
    public Translations Type { get; private set; }
    public double Price { get; private set; }
    public Translations Color { get; private set; }
    [MaxLength(10)]
    public string ColorCode { get; private set; }
    [MaxLength(10)]
    public string EngravingColorCode { get; private set; }
    public bool IsActive { get; private set; }
    public Texture? Texture { get; private set; }
    public FileEntity? ColorMap { get; private set; }

    public double GetPrice(double exchangerRate)
    {
        return Math.Ceiling(this.Price / exchangerRate);
    }
    
    public void Update(BladeCoatingColor bladeCoatingColor)
    {
        if (bladeCoatingColor == null)
        {
            throw new ArgumentNullException(nameof(bladeCoatingColor));
        }
        BladeCoatingColorValidator.Validate(
            bladeCoatingColor.Price, 
            bladeCoatingColor.ColorCode, 
            bladeCoatingColor.EngravingColorCode, 
            bladeCoatingColor.ColorMap
        );
        
        this.Type = bladeCoatingColor.Type;
        this.Price = bladeCoatingColor.Price;
        this.Color = bladeCoatingColor.Color;
        this.ColorCode = bladeCoatingColor.ColorCode;
        this.EngravingColorCode = bladeCoatingColor.EngravingColorCode;
        this.Texture = bladeCoatingColor.Texture;
        this.ColorMap = bladeCoatingColor.ColorMap;
        this.IsActive = bladeCoatingColor.IsActive;
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