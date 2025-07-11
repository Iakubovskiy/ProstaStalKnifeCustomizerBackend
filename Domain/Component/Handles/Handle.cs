﻿using System.ComponentModel.DataAnnotations;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Handles.Validators;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;

namespace Domain.Component.Handles;

public class Handle : IComponent, IEntity, IUpdatable<Handle>, ITextured
{
    private Handle()
    {
        
    }

    public Handle(
        Guid id, 
        Translations color, 
        string colorCode, 
        bool isActive, 
        Translations material,
        Texture? texture,
        FileEntity? colorMap,
        double price,
        FileEntity? handleModel,
        BladeShapeType bladeShapeType
    )
    {
        HandleValidator.Validate(colorCode, colorMap);
        this.Id = id;
        this.Color = color;
        this.ColorCode = colorCode;
        this.IsActive = isActive;
        this.Material = material;
        this.Texture = texture;
        this.ColorMap = colorMap;
        this.Price = price;
        this.HandleModel = handleModel;
        this.BladeShapeType = bladeShapeType;
    }

    public Guid Id { get; private set; }
    public Translations Color { get; private set; }
    [MaxLength(255)]
    public string ColorCode { get; private set; }
    public bool IsActive { get; set; }
    public Translations Material { get; private set; }
    public Texture? Texture { get; private set; }
    [MaxLength(255)]
    public FileEntity? ColorMap { get; private set; }
    public double Price { get; private set; }
    
    public FileEntity? HandleModel { get; private set; }
    public BladeShapeType BladeShapeType { get; private set; }
    
    public double GetPrice(double exchangerRate)
    {
        return Math.Ceiling(this.Price / exchangerRate);
    }

    public void Update(Handle handle)
    {
        HandleValidator.Validate(handle.ColorCode, handle.ColorMap);
        this.Color = handle.Color;
        this.ColorCode = handle.ColorCode;
        this.IsActive = handle.IsActive;
        this.Material = handle.Material;
        this.Texture = handle.Texture;
        this.ColorMap = handle.ColorMap;
        this.Price = handle.Price;
        this.BladeShapeType = handle.BladeShapeType;
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