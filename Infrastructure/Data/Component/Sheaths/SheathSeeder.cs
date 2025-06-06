using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;

namespace Infrastructure.Data.Component.Sheaths;

public class SheathSeeder : ISeeder
{
    public int Priority => 1;
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;

    public SheathSeeder(
        IComponentRepository<Sheath> sheathRepository, 
        IRepository<FileEntity> fileEntityRepository,
        IRepository<BladeShapeType> bladeShapeTypeRepository)
    {
        this._sheathRepository = sheathRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._bladeShapeTypeRepository = bladeShapeTypeRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _sheathRepository.GetAll()).Any())
        {
            return;
        }

        var file1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var file3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        var file5 = await _fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));

        var typeDropPoint = await _bladeShapeTypeRepository.GetById(new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde"));
        var typeClipPoint = await _bladeShapeTypeRepository.GetById(new Guid("2b3c4d5e-6f7a-4890-b123-456789abcdef"));
        var typeTanto = await _bladeShapeTypeRepository.GetById(new Guid("3c4d5e6f-7a8b-4901-c234-56789abcdef0"));
        var typeSpearPoint = await _bladeShapeTypeRepository.GetById(new Guid("4d5e6f7a-8b9c-4012-d345-6789abcdef01"));
        var typeSheepsfoot = await _bladeShapeTypeRepository.GetById(new Guid("5e6f7a8b-9c0d-4123-e456-789abcdef012"));
        var typeHawkbill = await _bladeShapeTypeRepository.GetById(new Guid("9c0d1e2f-3a4b-4567-289a-bcdef0123456"));
        var typeKarambit = await _bladeShapeTypeRepository.GetById(new Guid("0d1e2f3a-4b5c-4678-39ab-cdef01234567"));
        
        if (file1 == null || file3 == null || file5 == null || typeDropPoint == null || typeClipPoint == null ||
            typeTanto == null || typeSpearPoint == null || typeSheepsfoot == null || typeHawkbill == null || typeKarambit == null)
        {
            return;
        }
        
        var sheath1 = new Sheath(new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"), new Translations(new Dictionary<string, string> { { "en", "Kydex Sheath for Drop Point" }, { "ua", "Ножни з кайдекса для Дроп-поінт" } }), file1, typeDropPoint, 450.0, true);
        var sheath2 = new Sheath(new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"), new Translations(new Dictionary<string, string> { { "en", "Leather Sheath for Clip Point" }, { "ua", "Шкіряні ножни для Кліп-поінт" } }), null, typeClipPoint, 550.0, true);
        var sheath3 = new Sheath(new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"), new Translations(new Dictionary<string, string> { { "en", "Nylon Sheath for Tanto" }, { "ua", "Нейлонові ножни для Танто" } }), file3, typeTanto, 300.0, true);
        var sheath4 = new Sheath(new Guid("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"), new Translations(new Dictionary<string, string> { { "en", "Polymer Sheath for Spear Point" }, { "ua", "Полімерні ножни для Спір-поінт" } }), file5, typeSpearPoint, 400.0, false);
        var sheath5 = new Sheath(new Guid("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"), new Translations(new Dictionary<string, string> { { "en", "Compact Sheath for Sheepsfoot" }, { "ua", "Компактні ножни для Шипсфут" } }), file1, typeSheepsfoot, 350.0, true);
        var sheath6 = new Sheath(new Guid("f6a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c"), new Translations(new Dictionary<string, string> { { "en", "Neck Sheath for Karambit" }, { "ua", "Нашийні ножни для Керамбіт" } }), file3, typeKarambit, 600.0, true);
        var sheath7 = new Sheath(new Guid("07b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d"), new Translations(new Dictionary<string, string> { { "en", "Belt Sheath for Hawkbill" }, { "ua", "Поясні ножни для Хоукбілл" } }), null, typeHawkbill, 500.0, true);
        var sheath8 = new Sheath(new Guid("18c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"), new Translations(new Dictionary<string, string> { { "en", "Deluxe Leather Sheath for Drop Point" }, { "ua", "Шкіряні ножни люкс для Дроп-поінт" } }), file5, typeDropPoint, 750.0, true);
        var sheath9 = new Sheath(new Guid("29d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f"), new Translations(new Dictionary<string, string> { { "en", "MOLLE Sheath for Tanto" }, { "ua", "Ножни MOLLE для Танто" } }), file1, typeTanto, 650.0, false);
        var sheath10 = new Sheath(new Guid("3a0e1f2a-3b4c-4d6e-7f8a-9b0c1d2e3f4a"), new Translations(new Dictionary<string, string> { { "en", "Wooden Sheath for Spear Point" }, { "ua", "Дерев'яні ножни для Спір-поінт" } }), null, typeSpearPoint, 800.0, true);
        
        await _sheathRepository.Create(sheath1);
        await _sheathRepository.Create(sheath2);
        await _sheathRepository.Create(sheath3);
        await _sheathRepository.Create(sheath4);
        await _sheathRepository.Create(sheath5);
        await _sheathRepository.Create(sheath6);
        await _sheathRepository.Create(sheath7);
        await _sheathRepository.Create(sheath8);
        await _sheathRepository.Create(sheath9);
        await _sheathRepository.Create(sheath10);
    }
}