using Domain.Component.BladeShapes;
using Domain.Component.BladeShapes.BladeCharacteristic;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;

namespace Infrastructure.Data.Component.BladeShapes;

public class BladeShapeSeeder : ISeeder
{
    public int Priority => 2;
    private readonly IComponentRepository<BladeShape> _bladeShapeRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;
    private readonly IComponentRepository<Sheath> _sheathRepository;

    public BladeShapeSeeder(
        IComponentRepository<BladeShape> bladeShapeRepository, 
        IRepository<FileEntity> fileEntityRepository, 
        IRepository<BladeShapeType> bladeShapeTypeRepository,
        IComponentRepository<Sheath> sheathRepository)
    {
        this._bladeShapeRepository = bladeShapeRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._bladeShapeTypeRepository = bladeShapeTypeRepository;
        this._sheathRepository = sheathRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _bladeShapeRepository.GetAll()).Any())
        {
            return;
        }

        var file1 = await this._fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var file2 = await this._fileEntityRepository.GetById(new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"));
        var file3 = await this._fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        var file4 = await this._fileEntityRepository.GetById(new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"));
        var file5 = await this._fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));

        var typeDropPoint = await this._bladeShapeTypeRepository.GetById(new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde"));
        var typeClipPoint = await this._bladeShapeTypeRepository.GetById(new Guid("2b3c4d5e-6f7a-4890-b123-456789abcdef"));
        var typeTanto = await this._bladeShapeTypeRepository.GetById(new Guid("3c4d5e6f-7a8b-4901-c234-56789abcdef0"));
        var typeSpearPoint = await this._bladeShapeTypeRepository.GetById(new Guid("4d5e6f7a-8b9c-4012-d345-6789abcdef01"));
        var typeSheepsfoot = await this._bladeShapeTypeRepository.GetById(new Guid("5e6f7a8b-9c0d-4123-e456-789abcdef012"));
        var typeHawkbill = await this._bladeShapeTypeRepository.GetById(new Guid("9c0d1e2f-3a4b-4567-289a-bcdef0123456"));
        var typeKarambit = await this._bladeShapeTypeRepository.GetById(new Guid("0d1e2f3a-4b5c-4678-39ab-cdef01234567"));

        var sheath1 = await this._sheathRepository.GetById(new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"));
        var sheath2 = await this._sheathRepository.GetById(new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"));
        var sheath3 = await this._sheathRepository.GetById(new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"));
        var sheath6 = await this._sheathRepository.GetById(new Guid("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c"));
        var sheath8 = await this._sheathRepository.GetById(new Guid("18c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"));
        
        if (file1 == null || file2 == null || file3 == null || file4 == null || file5 == null || typeDropPoint == null ||
            typeClipPoint == null || typeTanto == null || typeSpearPoint == null || typeSheepsfoot == null || 
            typeHawkbill == null || typeKarambit == null || sheath1 == null || sheath2 == null || sheath3 == null || 
            sheath6 == null || sheath8 == null)
        {
            return;
        }

        var bladeShape1 = new BladeShape(
            new Guid("1f768e1c-7201-4a3d-9d48-a9de3f2b6e7a"),
            typeDropPoint,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Classic Drop Point" },
                { "ua", "Класичний Дроп-поінт" },
            }),
            file1,
            1200.0,
            new BladeCharacteristics(210, 95, 28, 160, 22.0, 59.0),
            sheath1,
            file2,
            true
        );

        var bladeShape2 = new BladeShape(
            new Guid("2c89b5a3-8d34-4e45-b421-4f1a9c8b7d0f"),
            typeTanto,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Tactical Tanto" },
                { "ua", "Тактичний Танто" },
            }),
            file3,
            1550.0,
            new BladeCharacteristics(225, 110, 30, 185, 25.0, 60.0),
            sheath3,
            file4,
            true
        );

        var bladeShape3 = new BladeShape(
            new Guid("3e9ac6b4-9c45-4f56-c532-5a2b8d9c8e1a"),
            typeSpearPoint,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Symmetrical Spear Point" },
                { "ua", "Симетричний Спір-поінт" },
            }),
            null,
            1300.0,
            new BladeCharacteristics(200, 90, 25, 150, 20.0, 58.5),
            null,
            file5,
            true
        );

        var bladeShape4 = new BladeShape(
            new Guid("4a0bd7c5-ad56-4067-d643-6b3c7e0d9f2b"),
            typeClipPoint,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Hunting Clip Point" },
                { "ua", "Мисливський Кліп-поінт" },
            }),
            file1,
            1100.0,
            new BladeCharacteristics(230, 115, 27, 170, 23.0, 58.0),
            sheath2,
            file3,
            false
        );

        var bladeShape5 = new BladeShape(
            new Guid("5b1ce8d6-be67-4178-e754-7c4d6f1e0a3c"),
            typeSheepsfoot,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Utility Sheepsfoot" },
                { "ua", "Утилітарний Шипсфут" },
            }),
            file2,
            950.0,
            new BladeCharacteristics(190, 85, 32, 140, 18.0, 59.5),
            null,
            file4,
            true
        );
        
        var bladeShape6 = new BladeShape(
            new Guid("6c2df9e7-cf78-4289-f865-8d5e5a2f1b4d"),
            typeHawkbill,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Aggressive Hawkbill" },
                { "ua", "Агресивний Хоукбілл" },
            }),
            file5,
            1700.0,
            new BladeCharacteristics(195, 80, 26, 175, 28.0, 60.5),
            null,
            file1,
            true
        );

        var bladeShape7 = new BladeShape(
            new Guid("7d3ea0f8-d089-439a-0976-9e6f4b3a2c5e"),
            typeKarambit,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Curved Karambit" },
                { "ua", "Вигнутий Керамбіт" },
            }),
            file3,
            2100.0,
            new BladeCharacteristics(180, 75, 29, 190, 30.0, 61.0),
            sheath6,
            file2,
            true
        );

        var bladeShape8 = new BladeShape(
            new Guid("8e4fb109-e19a-44ab-1a87-af703c4b3d6f"),
            typeDropPoint,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Deluxe Drop Point" },
                { "ua", "Елітний Дроп-поінт" },
            }),
            file4,
            1900.0,
            new BladeCharacteristics(215, 100, 28, 165, 21.0, 59.5),
            sheath8,
            file5,
            true
        );

        var bladeShape9 = new BladeShape(
            new Guid("9f50c21a-f2ab-45bc-2b98-bf812d5c4e70"),
            typeClipPoint,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Large Bowie" },
                { "ua", "Великий Боуї" },
            }),
            null,
            2500.0,
            new BladeCharacteristics(350, 220, 38, 350, 25.0, 58.0),
            null,
            file1,
            false
        );

        var bladeShape10 = new BladeShape(
            new Guid("af61d32b-03bc-46cd-3ca9-cf921e6d5f81"),
            typeTanto,
            new Translations(new Dictionary<string, string>
            {
                { "en", "Compact Tanto" },
                { "ua", "Компактний Танто" },
            }),
            file2,
            1400.0,
            new BladeCharacteristics(200, 90, 29, 150, 26.0, 60.0),
            null,
            file3,
            true
        );

        await _bladeShapeRepository.Create(bladeShape1);
        await _bladeShapeRepository.Create(bladeShape2);
        await _bladeShapeRepository.Create(bladeShape3);
        await _bladeShapeRepository.Create(bladeShape4);
        await _bladeShapeRepository.Create(bladeShape5);
        await _bladeShapeRepository.Create(bladeShape6);
        await _bladeShapeRepository.Create(bladeShape7);
        await _bladeShapeRepository.Create(bladeShape8);
        await _bladeShapeRepository.Create(bladeShape9);
        await _bladeShapeRepository.Create(bladeShape10);
    }
}