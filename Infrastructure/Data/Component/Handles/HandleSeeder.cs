using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Handles;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;

namespace Infrastructure.Data.Component.Handles;

public class HandleSeeder : ISeeder
{
    public int Priority => 2;
    private readonly IComponentRepository<Handle> _handleRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;
    private readonly IRepository<Texture> _textureRepository;

    public HandleSeeder(
        IComponentRepository<Handle> handleRepository,
        IRepository<FileEntity> fileEntityRepository,
        IRepository<BladeShapeType> bladeShapeTypeRepository,
        IRepository<Texture> textureRepository
    )
    {
        this._handleRepository = handleRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._bladeShapeTypeRepository = bladeShapeTypeRepository;
        this._textureRepository = textureRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _handleRepository.GetAll()).Any())
        {
            return;
        }

        var file1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var file3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        var file5 = await _fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));

        var typeDropPoint = await _bladeShapeTypeRepository.GetById(new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde"));
        var typeTanto = await _bladeShapeTypeRepository.GetById(new Guid("3c4d5e6f-7a8b-4901-c234-56789abcdef0"));
        var typeSpearPoint = await _bladeShapeTypeRepository.GetById(new Guid("4d5e6f7a-8b9c-4012-d345-6789abcdef01"));
        var typeKarambit = await _bladeShapeTypeRepository.GetById(new Guid("0d1e2f3a-4b5c-4678-39ab-cdef01234567"));

        var textureCarbon = await _textureRepository.GetById(new Guid("c9d0e1f2-a3b4-4567-2345-678901234567"));
        var textureLeather = await _textureRepository.GetById(new Guid("d4e5f6a7-b8c9-4012-def0-123456789012"));
        var textureWood = await _textureRepository.GetById(new Guid("b2c3d4e5-f6a7-4890-bcde-f01234567890"));
        var textureMetal = await _textureRepository.GetById(new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789"));
        
        if (file1 == null || file3 == null || file5 == null || typeDropPoint == null || typeTanto == null || 
            typeSpearPoint == null || typeKarambit == null || textureCarbon == null || textureLeather == null || 
            textureWood == null || textureMetal == null)
        {
            return;
        }

        var handle1 = new Handle(
            new Guid("11a1b1c1-d1e1-41f1-81a1-b1c1d1e1f1a1"),
            new Translations(new Dictionary<string, string> { { "en", "Black" }, { "ua", "Чорний" }, }),
            "#1A1A1A",
            true,
            new Translations(new Dictionary<string, string> { { "en", "G10" }, { "ua", "G10" }, }),
            textureCarbon,
            file1,
            950.0,
            file3,
            typeDropPoint
        );
        
        var handle2 = new Handle(
            new Guid("22b2c2d2-e2f2-42a2-92b2-c2d2e2f2a2b2"),
            new Translations(new Dictionary<string, string> { { "en", "Olive Drab" }, { "ua", "Оливковий" }, }),
            "#6B8E23",
            true,
            new Translations(new Dictionary<string, string> { { "en", "Micarta" }, { "ua", "Мікарта" }, }),
            null,
            null,
            1100.0,
            file5,
            typeTanto
        );
        
        var handle3 = new Handle(
            new Guid("33c3d3e3-f3a3-43b3-a3c3-d3e3f3a3b3c3"),
            new Translations(new Dictionary<string, string> { { "en", "Dark Wood" }, { "ua", "Темне дерево" }, }),
            null,
            true,
            new Translations(new Dictionary<string, string> { { "en", "Walnut" }, { "ua", "Горіх" }, }),
            textureWood,
            file3,
            1500.0,
            null,
            typeSpearPoint
        );
        
        var handle4 = new Handle(
            new Guid("44d4e4f4-a4b4-44c4-b4d4-e4f4a4b4c4d4"),
            new Translations(new Dictionary<string, string> { { "en", "Desert Tan" }, { "ua", "Пустельний тан" }, }),
            "#C19A6B",
            false,
            new Translations(new Dictionary<string, string> { { "en", "Polymer" }, { "ua", "Полімер" }, }),
            null,
            null,
            700.0,
            file1,
            typeTanto
        );
        
        var handle5 = new Handle(
            new Guid("55e5f5a5-b5c5-45d5-c5e5-f5a5b5c5d5e5"),
            new Translations(new Dictionary<string, string> { { "en", "Titanium Gray" }, { "ua", "Титановий сірий" }, }),
            "#878681",
            true,
            new Translations(new Dictionary<string, string> { { "en", "Titanium" }, { "ua", "Титан" }, }),
            textureMetal,
            file5,
            2500.0,
            file5,
            typeDropPoint
        );
        
        var handle6 = new Handle(
            new Guid("66f6a6b6-c6d6-46e6-d6f6-a6b6c6d6e6f6"),
            new Translations(new Dictionary<string, string> { { "en", "Brown Leather" }, { "ua", "Коричнева шкіра" }, }),
            null,
            true,
            new Translations(new Dictionary<string, string> { { "en", "Leather Wrapped" }, { "ua", "Обмотка шкірою" }, }),
            textureLeather,
            null,
            1800.0,
            null,
            typeSpearPoint
        );
        
        var handle7 = new Handle(
            new Guid("77a7b7c7-d7e7-47f7-e7a7-b7c7d7e7f7a7"),
            new Translations(new Dictionary<string, string> { { "en", "Carbon Fiber" }, { "ua", "Карбон" }, }),
            "#282828",
            true,
            new Translations(new Dictionary<string, string> { { "en", "Carbon Fiber" }, { "ua", "Вуглецеве волокно" }, }),
            textureCarbon,
            file1,
            2200.0,
            file1,
            typeKarambit
        );
        
        var handle8 = new Handle(
            new Guid("88b8c8d8-e8f8-48a8-f8b8-c8d8e8f8a8b8"),
            new Translations(new Dictionary<string, string> { { "en", "Red" }, { "ua", "Червоний" }, }),
            "#FF0000",
            true,
            new Translations(new Dictionary<string, string> { { "en", "Anodized Aluminum" }, { "ua", "Анодований алюміній" }, }),
            null,
            null,
            1300.0,
            null,
            typeKarambit
        );
        
        var handle9 = new Handle(
            new Guid("99c9d9e9-f9a9-49b9-a9c9-d9e9f9a9b9c9"),
            new Translations(new Dictionary<string, string> { { "en", "Light Wood" }, { "ua", "Світле дерево" }, }),
            null,
            false,
            new Translations(new Dictionary<string, string> { { "en", "Birch" }, { "ua", "Береза" }, }),
            textureWood,
            file3,
            1250.0,
            file3,
            typeDropPoint
        );
        
        var handle10 = new Handle(
            new Guid("00d0e0f0-a0b0-40c0-b0d0-e0f0a0b0c0d0"),
            new Translations(new Dictionary<string, string> { { "en", "Blue" }, { "ua", "Синій" }, }),
            "#0000FF",
            true,
            new Translations(new Dictionary<string, string> { { "en", "G10" }, { "ua", "G10" }, }),
            null,
            file5,
            1000.0,
            file5,
            typeTanto
        );

        await _handleRepository.Create(handle1);
        await _handleRepository.Create(handle2);
        await _handleRepository.Create(handle3);
        await _handleRepository.Create(handle4);
        await _handleRepository.Create(handle5);
        await _handleRepository.Create(handle6);
        await _handleRepository.Create(handle7);
        await _handleRepository.Create(handle8);
        await _handleRepository.Create(handle9);
        await _handleRepository.Create(handle10);
    }
}