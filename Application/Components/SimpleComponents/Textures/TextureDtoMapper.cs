using Application.Components.SimpleComponents.UseCases;
using Application.Files;
using Domain.Component.Textures;
using Domain.Files;
using Infrastructure;

namespace Application.Components.SimpleComponents.Textures;

public class TextureDtoMapper : IComponentDtoMapper<Texture, TextureDto>
{
    private readonly IRepository<FileEntity> _fileRepository;
    
    public TextureDtoMapper(
        IRepository<FileEntity> fileRepository
    )
    {
        this._fileRepository = fileRepository;
    }
    public async Task<Texture> Map(TextureDto dto)
    {
        Guid id = dto.Id ?? Guid.NewGuid();
        FileEntity normalMap = await this._fileRepository.GetById(dto.NormalMapId);;
        FileEntity roughnessMap = await this._fileRepository.GetById(dto.RoughnessMapId);
        return new Texture(
            id,
            dto.Name,
            normalMap,
            roughnessMap
        );
    }
}