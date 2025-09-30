using Domain.Component.BladeCoatingColors;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;

namespace Infrastructure.Data.Component.BladeCoatingColors;

public class BladeCoatingColorSeeder : ISeeder
{
    public int Priority => 2;
    private readonly IComponentRepository<BladeCoatingColor> _bladeCoatingColorRepository;
    private readonly IRepository<Texture> _textureRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;

    public BladeCoatingColorSeeder(
        IComponentRepository<BladeCoatingColor> bladeCoatingColorRepository,
        IRepository<Texture> textureRepository,
        IRepository<FileEntity> fileEntityRepository
    )
    {
        this._bladeCoatingColorRepository = bladeCoatingColorRepository;
        this._textureRepository = textureRepository;
        this._fileEntityRepository = fileEntityRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _bladeCoatingColorRepository.GetAll()).Any())
        {
            return;
        }

        var file1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var file3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));

        var textureMetal = await _textureRepository.GetById(new Guid("a1b2c3d4-e5f6-4789-abcd-ef0123456789"));
        var textureMatte = await _textureRepository.GetById(new Guid("e5f6a7b8-c9d0-4123-ef01-234567890123"));
        var textureCarbon = await _textureRepository.GetById(new Guid("c9d0e1f2-a3b4-4567-2345-678901234567"));

        if (file1 == null || file3 == null || textureMetal == null || textureMatte == null || textureCarbon == null)
        {
            return;
        }

        var coating1 = new BladeCoatingColor(
            new Guid("ecf8f3c7-1b3d-4e9a-8c4f-6a2b8d9c1e0a"),
            new Translations(new Dictionary<string, string> { { "en", "Satin Finish" }, { "ua", "Сатинування" }, }),
            0.0,
            new Translations(new Dictionary<string, string> { { "en", "Natural Steel" }, { "ua", "Натуральна сталь" }, }),
            "#479520",
            "#101010",
            textureMetal,
            null
        );

        var coating2 = new BladeCoatingColor(
            new Guid("d9e7c2b6-2a4e-4f8b-9d1c-7b3a9e8d2f1b"),
            new Translations(new Dictionary<string, string> { { "en", "DLC Coating" }, { "ua", "DLC покриття" }, }),
            800.0,
            new Translations(new Dictionary<string, string> { { "en", "Matte Black" }, { "ua", "Чорний матовий" }, }),
            "#1C1C1C",
            "#F0F0F0",
            textureMatte,
            null
        );

        var coating3 = new BladeCoatingColor(
            new Guid("c8d6b1a5-3b5f-4a7c-8e2d-8c4ba7d93a2c"),
            new Translations(new Dictionary<string, string> { { "en", "Stonewash" }, { "ua", "Стоунвош" }, }),
            250.0,
            new Translations(new Dictionary<string, string> { { "en", "Dark Gray" }, { "ua", "Темно-сірий" }, }),
            "#5A5A5A",
            "#000000",
            null,
            null
        );

        var coating4 = new BladeCoatingColor(
            new Guid("b7c5a094-4c6a-4b6d-9f3e-9d5cb6c84b3d"),
            new Translations(new Dictionary<string, string> { { "en", "Cerakote" }, { "ua", "Cerakote" }, }),
            950.0,
            new Translations(new Dictionary<string, string> { { "en", "Olive Drab" }, { "ua", "Оливковий" }, }),
            "#6B8E23",
            "#FFFFFF",
            textureMatte,
            file1
        );
        coating4.Deactivate();

        var coating5 = new BladeCoatingColor(
            new Guid("a6b49f83-5d7b-4c5e-8a4f-ad6dc5b75c4e"),
            new Translations(new Dictionary<string, string> { { "en", "PVD Coating" }, { "ua", "PVD покриття" }, }),
            750.0,
            new Translations(new Dictionary<string, string> { { "en", "Bronze" }, { "ua", "Бронзовий" }, }),
            "#CD7F32",
            "#1A1A1A",
            textureMetal,
            null
        );

        var coating6 = new BladeCoatingColor(
            new Guid("95a38e72-6e8c-4d4f-9b5a-be7ed4a66d5f"),
            new Translations(new Dictionary<string, string> { { "en", "Bead Blast" }, { "ua", "Склоструй" }, }),
            150.0,
            new Translations(new Dictionary<string, string> { { "en", "Light Gray" }, { "ua", "Світло-сірий" }, }),
            "#D3D3D3",
            "#202020",
            textureMatte,
            null
        );
        
        var coating7 = new BladeCoatingColor(
            new Guid("84927d61-7f9d-4e3a-8c6b-cf8fe3957e60"),
            new Translations(new Dictionary<string, string> { { "en", "Titanium Nitride (TiN)" }, { "ua", "Нітрид титану (TiN)" }, }),
            1100.0,
            new Translations(new Dictionary<string, string> { { "en", "Gold" }, { "ua", "Золотий" }, }),
            "#FFD700",
            "#000000",
            null,
            null
        );
        
        var coating8 = new BladeCoatingColor(
            new Guid("73816c50-80ae-4f2b-9d7c-d090f2848f71"),
            new Translations(new Dictionary<string, string> { { "en", "Blackwash" }, { "ua", "Блеквош" }, }),
            400.0,
            new Translations(new Dictionary<string, string> { { "en", "Worn Black" }, { "ua", "Потертий чорний" }, }),
            "#3A3A3A",
            "#E0E0E0",
            null,
            null
        );
        
        var coating9 = new BladeCoatingColor(
            new Guid("62705b4f-91bf-401c-8e8d-e1a1e1739082"),
            new Translations(new Dictionary<string, string> { { "en", "Cerakote" }, { "ua", "Cerakote" }, }),
            1000.0,
            new Translations(new Dictionary<string, string> { { "en", "Flat Dark Earth" }, { "ua", "FDE" }, }),
            "#736551",
            "#000000",
            textureMatte,
            file3
        );
        
        var coating10 = new BladeCoatingColor(
            new Guid("516f4a3e-a2c0-410d-9f9e-f2b2d062a193"),
            new Translations(new Dictionary<string, string> { { "en", "Two-Tone" }, { "ua", "Двоколірний" }, }),
            600.0,
            new Translations(new Dictionary<string, string> { { "en", "Satin & Black" }, { "ua", "Сатин та чорний" }, }),
            "#566hjd",
            "#FFFFFF",
            textureCarbon,
            null
        );
        coating10.Deactivate();

        await _bladeCoatingColorRepository.Create(coating1);
        await _bladeCoatingColorRepository.Create(coating2);
        await _bladeCoatingColorRepository.Create(coating3);
        await _bladeCoatingColorRepository.Create(coating4);
        await _bladeCoatingColorRepository.Create(coating5);
        await _bladeCoatingColorRepository.Create(coating6);
        await _bladeCoatingColorRepository.Create(coating7);
        await _bladeCoatingColorRepository.Create(coating8);
        await _bladeCoatingColorRepository.Create(coating9);
        await _bladeCoatingColorRepository.Create(coating10);
    }
}