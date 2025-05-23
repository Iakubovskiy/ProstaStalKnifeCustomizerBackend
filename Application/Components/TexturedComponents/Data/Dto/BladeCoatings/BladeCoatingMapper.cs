using System.Data.Entity.Core;
using Application.Files;
using Domain.Component.BladeCoatingColors;
using Domain.Component.Textures;
using Domain.Component.Translation;
using Infrastructure;
using Newtonsoft.Json;

namespace Application.Components.TexturedComponents.Data.Dto.BladeCoatings;

public class BladeCoatingMapper : ITexturedComponentDtoMapper<BladeCoatingColor, BladeCoatingDto>
{
    private readonly IFileService _fileService;
    public BladeCoatingMapper(IFileService fileService)
    {
        this._fileService = fileService;
    }
    public async Task<BladeCoatingColor> Map(BladeCoatingDto dto, Texture? texture)
    {
        Guid id = Guid.NewGuid();
        Translations type;
        Translations color;
        string? colorMapUrl = null;
        if (dto.ColorMap != null)
        {
            colorMapUrl = await this._fileService.SaveFile(dto.ColorMap, dto.ColorMap.FileName);
        }
        try
        {
            type = JsonConvert.DeserializeObject<Translations>(dto.Type)
                                ?? throw new Exception("Name is empty");
        }
        catch (Exception e)
        {
            throw new Exception("Name is not valid");
        }
        try
        {
            color = JsonConvert.DeserializeObject<Translations>(dto.Color)
                   ?? throw new Exception("Name is empty");
        }
        catch (Exception e)
        {
            throw new Exception("Name is not valid");
        }
        
        return new BladeCoatingColor(
                id,
                type,
                dto.Price,
                color,
                dto.ColorCode,
                dto.EngravingColorCode,
                texture,
                colorMapUrl
        );
    }
}