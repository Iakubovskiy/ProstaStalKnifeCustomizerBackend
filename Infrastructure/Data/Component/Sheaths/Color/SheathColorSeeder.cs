using System.Reflection;
using System.Runtime.Serialization;
using Domain.Component.BladeShapes.BladeShapeTypes;
using Domain.Component.Sheaths.Color;
using Domain.Component.Textures;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components.Sheaths.Color;

namespace Infrastructure.Data.Component.Sheaths.Color;

public class SheathColorSeeder : ISeeder
{
    public int Priority => 3;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;
    private readonly IRepository<Texture> _textureRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly DBContext _context;

    public SheathColorSeeder(
        ISheathColorRepository sheathColorRepository,
        IRepository<BladeShapeType> bladeShapeTypeRepository,
        IRepository<Texture> textureRepository,
        IRepository<FileEntity> fileEntityRepository,
        DBContext context
    )
    {
        this._sheathColorRepository = sheathColorRepository;
        this._bladeShapeTypeRepository = bladeShapeTypeRepository;
        this._textureRepository = textureRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._context = context;
    }

    public async Task SeedAsync()
    {
        if ((await _sheathColorRepository.GetAll()).Any())
        {
            return;
        }

        var allBladeShapeTypes = await _bladeShapeTypeRepository.GetAll();
        var textureLeather = await _textureRepository.GetById(new Guid("d4e5f6a7-b8c9-4012-def0-123456789012"));
        var textureMatte = await _textureRepository.GetById(new Guid("e5f6a7b8-c9d0-4123-ef01-234567890123"));
        var file1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        
        if (!allBladeShapeTypes.Any() || textureLeather == null || textureMatte == null || file1 == null)
        {
            return;
        }
        
        var sheathColors = new List<SheathColor>();

        var color1 = CreateSheathColorInstance(new Guid("a1a1a1a1-1111-4111-8111-a1a1a1a1a1a1"), new Translations(new Dictionary<string, string> { { "en", "Black" }, { "ua", "Чорний" }, }), true, "#000000", new Translations(new Dictionary<string, string> { { "en", "Kydex" }, { "ua", "Кайдекс" }, }), "#FFFFFF", textureMatte, file1);
        var prices1 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color1, 200.0 + (index * 10.0))).ToList();
        SetPrivateProperty(color1, nameof(SheathColor.Prices), prices1);
        sheathColors.Add(color1);

        var color2 = CreateSheathColorInstance(new Guid("b2b2b2b2-2222-4222-8222-b2b2b2b2b2b2"), new Translations(new Dictionary<string, string> { { "en", "Brown" }, { "ua", "Коричневий" }, }), true, "#A52A2A", new Translations(new Dictionary<string, string> { { "en", "Leather" }, { "ua", "Шкіра" }, }), "#000000", textureLeather, null);
        var prices2 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color2, 350.0 + (index * 15.0))).ToList();
        SetPrivateProperty(color2, nameof(SheathColor.Prices), prices2);
        sheathColors.Add(color2);

        var color3 = CreateSheathColorInstance(new Guid("c3c3c3c3-3333-4333-8333-c3c3c3c3c3c3"), new Translations(new Dictionary<string, string> { { "en", "Olive Drab" }, { "ua", "Оливковий" }, }), true, "#6B8E23", new Translations(new Dictionary<string, string> { { "en", "Kydex" }, { "ua", "Кайдекс" }, }), "#000000", textureMatte, null);
        var prices3 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color3, 220.0 + (index * 10.0))).ToList();
        SetPrivateProperty(color3, nameof(SheathColor.Prices), prices3);
        sheathColors.Add(color3);
        
        var color4 = CreateSheathColorInstance(new Guid("d4d4d4d4-4444-4444-8444-d4d4d4d4d4d4"), new Translations(new Dictionary<string, string> { { "en", "Coyote Brown" }, { "ua", "Койот" }, }), true, "#81613C", new Translations(new Dictionary<string, string> { { "en", "Nylon" }, { "ua", "Нейлон" }, }), "#FFFFFF", null, null);
        var prices4 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color4, 150.0 + (index * 5.0))).ToList();
        SetPrivateProperty(color4, nameof(SheathColor.Prices), prices4);
        sheathColors.Add(color4);
        
        var color5 = CreateSheathColorInstance(new Guid("e5e5e5e5-5555-4555-8555-e5e5e5e5e5e5"), new Translations(new Dictionary<string, string> { { "en", "Carbon Fiber" }, { "ua", "Карбон" }, }), false, "#81613C", new Translations(new Dictionary<string, string> { { "en", "Kydex" }, { "ua", "Кайдекс" }, }), "#EAEAEA", textureMatte, file1);
        var prices5 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color5, 500.0 + (index * 20.0))).ToList();
        SetPrivateProperty(color5, nameof(SheathColor.Prices), prices5);
        sheathColors.Add(color5);

        var color6 = CreateSheathColorInstance(new Guid("f6f6f6f6-6666-4666-8666-f6f6f6f6f6f6"), new Translations(new Dictionary<string, string> { { "en", "Dark Brown" }, { "ua", "Темно-коричневий" }, }), true, "#654321", new Translations(new Dictionary<string, string> { { "en", "Leather" }, { "ua", "Шкіра" }, }), "#F0F0F0", textureLeather, null);
        var prices6 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color6, 400.0 + (index * 15.0))).ToList();
        SetPrivateProperty(color6, nameof(SheathColor.Prices), prices6);
        sheathColors.Add(color6);

        var color7 = CreateSheathColorInstance(new Guid("a7a7a7a7-7777-4777-8777-a7a7a7a7a7a7"), new Translations(new Dictionary<string, string> { { "en", "Gray" }, { "ua", "Сірий" }, }), true, "#808080", new Translations(new Dictionary<string, string> { { "en", "Nylon" }, { "ua", "Нейлон" }, }), "#000000", null, null);
        var prices7 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color7, 180.0 + (index * 5.0))).ToList();
        SetPrivateProperty(color7, nameof(SheathColor.Prices), prices7);
        sheathColors.Add(color7);

        var color8 = CreateSheathColorInstance(new Guid("b8b8b8b8-8888-4888-8888-b8b8b8b8b8b8"), new Translations(new Dictionary<string, string> { { "en", "Red" }, { "ua", "Червоний" }, }), false, "#FF0000", new Translations(new Dictionary<string, string> { { "en", "Kydex" }, { "ua", "Кайдекс" }, }), "#FFFFFF", textureMatte, null);
        var prices8 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color8, 350.0 + (index * 10.0))).ToList();
        SetPrivateProperty(color8, nameof(SheathColor.Prices), prices8);
        sheathColors.Add(color8);

        var color9 = CreateSheathColorInstance(new Guid("c9c9c9c9-9999-4999-8999-c9c9c9c9c9c9"), new Translations(new Dictionary<string, string> { { "en", "Black Leather" }, { "ua", "Чорна шкіра" }, }), true, "#1C1C1C", new Translations(new Dictionary<string, string> { { "en", "Leather" }, { "ua", "Шкіра" }, }), "#D3D3D3", textureLeather, null);
        var prices9 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color9, 380.0 + (index * 15.0))).ToList();
        SetPrivateProperty(color9, nameof(SheathColor.Prices), prices9);
        sheathColors.Add(color9);
        
        var color10 = CreateSheathColorInstance(new Guid("d0d0d0d0-0000-4000-8000-d0d0d0d0d0d0"), new Translations(new Dictionary<string, string> { { "en", "Tan" }, { "ua", "Тан" }, }), true, "#D2B48C", new Translations(new Dictionary<string, string> { { "en", "Kydex" }, { "ua", "Кайдекс" }, }), "#000000", textureMatte, file1);
        var prices10 = allBladeShapeTypes.Select((type, index) => new SheathColorPriceByType(type, color10, 230.0 + (index * 10.0))).ToList();
        SetPrivateProperty(color10, nameof(SheathColor.Prices), prices10);
        sheathColors.Add(color10);
        
        foreach (var color in sheathColors)
        {
            await _sheathColorRepository.Create(color);
        }

        await _context.SaveChangesAsync();
    }

    private SheathColor CreateSheathColorInstance(
        Guid id,
        Translations color,
        bool isActive,
        string? colorCode,
        Translations material,
        string engravingColorCode,
        Texture? texture,
        FileEntity? colorMap
    )
    {
        var instance = (SheathColor)FormatterServices.GetUninitializedObject(typeof(SheathColor));

        SetPrivateProperty(instance, nameof(SheathColor.Id), id);
        SetPrivateProperty(instance, nameof(SheathColor.Color), color);
        SetPrivateProperty(instance, nameof(SheathColor.IsActive), isActive);
        SetPrivateProperty(instance, nameof(SheathColor.ColorCode), colorCode);
        SetPrivateProperty(instance, nameof(SheathColor.Material), material);
        SetPrivateProperty(instance, nameof(SheathColor.EngravingColorCode), engravingColorCode);
        SetPrivateProperty(instance, nameof(SheathColor.Texture), texture);
        SetPrivateProperty(instance, nameof(SheathColor.ColorMap), colorMap);

        return instance;
    }
    
    private void SetPrivateProperty(object obj, string propertyName, object value)
    {
        var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        propertyInfo?.SetValue(obj, value, null);
    }
}