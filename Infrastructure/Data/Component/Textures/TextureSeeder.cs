using Domain.Component.Textures;
using Domain.Files;

namespace Infrastructure.Data.Component.Textures;

public class TextureSeeder : ISeeder
{
    public int Priority => 1;
    private readonly IRepository<Texture> _textureRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    
    public TextureSeeder(IRepository<Texture> textureRepository, IRepository<FileEntity> fileEntityRepository)
    {
        this._textureRepository = textureRepository;
        this._fileEntityRepository = fileEntityRepository;
    }
    
    public async Task SeedAsync()
    {
        int count = (await this._textureRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        
        FileEntity normalMap1 = await this._fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        FileEntity normalMap2 = await this._fileEntityRepository.GetById(new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"));
        FileEntity normalMap3 = await this._fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        FileEntity normalMap4 = await this._fileEntityRepository.GetById(new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"));
        FileEntity normalMap5 = await this._fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));
        
        FileEntity roughnessMap1 = await this._fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        FileEntity roughnessMap2 = await this._fileEntityRepository.GetById(new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"));
        FileEntity roughnessMap3 = await this._fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        FileEntity roughnessMap4 = await this._fileEntityRepository.GetById(new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"));
        FileEntity roughnessMap5 = await this._fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));
        
        Texture texture1 = new Texture(
            new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789"),
            "Metal Brushed",
            normalMap1,
            roughnessMap1
        );
        
        Texture texture2 = new Texture(
            new Guid("b2c3d4e5-f6a7-4890-bcde-f01234567890"),
            "Wood Oak",
            normalMap2,
            roughnessMap2
        );
        
        Texture texture3 = new Texture(
            new Guid("c3d4e5f6-a7b8-4901-cdef-012345678901"),
            "Stone Granite",
            normalMap3,
            roughnessMap3
        );
        
        Texture texture4 = new Texture(
            new Guid("d4e5f6a7-b8c9-4012-def0-123456789012"),
            "Fabric Leather",
            normalMap4,
            roughnessMap4
        );
        
        Texture texture5 = new Texture(
            new Guid("e5f6a7b8-c9d0-4123-ef01-234567890123"),
            "Plastic Matte",
            normalMap5,
            roughnessMap5
        );
        
        Texture texture6 = new Texture(
            new Guid("f6a7b8c9-d0e1-4234-f012-345678901234"),
            "Concrete Rough",
            normalMap1,
            roughnessMap1
        );
        
        Texture texture7 = new Texture(
            new Guid("a7b8c9d0-e1f2-4345-0123-456789012345"),
            "Marble White",
            normalMap2,
            roughnessMap2
        );
        
        Texture texture8 = new Texture(
            new Guid("b8c9d0e1-f2a3-4456-1234-567890123456"),
            "Ceramic Glossy",
            normalMap3,
            roughnessMap3
        );
        
        Texture texture9 = new Texture(
            new Guid("c9d0e1f2-a3b4-4567-2345-678901234567"),
            "Carbon Fiber",
            normalMap4,
            roughnessMap4
        );
        
        Texture texture10 = new Texture(
            new Guid("d0e1f2a3-b4c5-4678-3456-789012345678"),
            "Brick Red",
            normalMap5,
            roughnessMap5
        );
        
        await this._textureRepository.Create(texture1);
        await this._textureRepository.Create(texture2);
        await this._textureRepository.Create(texture3);
        await this._textureRepository.Create(texture4);
        await this._textureRepository.Create(texture5);
        await this._textureRepository.Create(texture6);
        await this._textureRepository.Create(texture7);
        await this._textureRepository.Create(texture8);
        await this._textureRepository.Create(texture9);
        await this._textureRepository.Create(texture10);
    }
}