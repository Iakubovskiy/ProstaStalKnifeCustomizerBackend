using Domain.Component.BladeCoatingColors;
using Domain.Component.BladeShapes;
using Domain.Component.Engravings;
using Domain.Component.Handles;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.Knife;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;
using Infrastructure.Components.Sheaths.Color;

namespace Infrastructure.Data.Component.Products.Knives;

public class KnifeSeeder : ISeeder
{
    public int Priority => 4;
    private readonly IComponentRepository<Knife> _knifeRepository;
    private readonly IRepository<ProductTag> _productTagRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly IComponentRepository<BladeShape> _bladeShapeRepository;
    private readonly IComponentRepository<BladeCoatingColor> _bladeCoatingColorRepository;
    private readonly IComponentRepository<Handle> _handleRepository;
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly IComponentRepository<Engraving> _engravingRepository;
    private readonly IComponentRepository<Attachment> _attachmentRepository;

    public KnifeSeeder(
        IComponentRepository<Knife> knifeRepository,
        IRepository<ProductTag> productTagRepository,
        IRepository<FileEntity> fileEntityRepository,
        IComponentRepository<BladeShape> bladeShapeRepository,
        IComponentRepository<BladeCoatingColor> bladeCoatingColorRepository,
        IComponentRepository<Handle> handleRepository,
        IComponentRepository<Sheath> sheathRepository,
        ISheathColorRepository sheathColorRepository,
        IComponentRepository<Engraving> engravingRepository,
        IComponentRepository<Attachment> attachmentRepository
    )
    {
        this._knifeRepository = knifeRepository;
        this._productTagRepository = productTagRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._bladeShapeRepository = bladeShapeRepository;
        this._bladeCoatingColorRepository = bladeCoatingColorRepository;
        this._handleRepository = handleRepository;
        this._sheathRepository = sheathRepository;
        this._sheathColorRepository = sheathColorRepository;
        this._engravingRepository = engravingRepository;
        this._attachmentRepository = attachmentRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _knifeRepository.GetAll()).Any())
        {
            return;
        }

        var bladeDropPoint = await _bladeShapeRepository.GetById(new Guid("1f768e1c-7201-4a3d-9d48-a9de3f2b6e7a"));
        var bladeTanto = await _bladeShapeRepository.GetById(new Guid("2c89b5a3-8d34-4e45-b421-4f1a9c8b7d0f"));
        var bladeSpearPoint = await _bladeShapeRepository.GetById(new Guid("3e9ac6b4-9c45-4f56-c532-5a2b8d9c8e1a"));
        var bladeClipPoint = await _bladeShapeRepository.GetById(new Guid("4a0bd7c5-ad56-4067-d643-6b3c7e0d9f2b"));

        var colorSatin = await _bladeCoatingColorRepository.GetById(new Guid("ecf8f3c7-1b3d-4e9a-8c4f-6a2b8d9c1e0a"));
        var colorDLC = await _bladeCoatingColorRepository.GetById(new Guid("d9e7c2b6-2a4e-4f8b-9d1c-7b3a9e8d2f1b"));
        var colorStonewash = await _bladeCoatingColorRepository.GetById(new Guid("c8d6b1a5-3b5f-4a7c-8e2d-8c4ba7d93a2c"));
        
        var handleG10 = await _handleRepository.GetById(new Guid("11a1b1c1-d1e1-41f1-81a1-b1c1d1e1f1a1"));
        var handleMicarta = await _handleRepository.GetById(new Guid("22b2c2d2-e2f2-42a2-92b2-c2d2e2f2a2b2"));
        var handleTitanium = await _handleRepository.GetById(new Guid("55e5f5a5-b5c5-45d5-c5e5-f5a5b5c5d5e5"));
        var handleWood = await _handleRepository.GetById(new Guid("33c3d3e3-f3a3-43b3-a3c3-d3e3f3a3b3c3"));

        var sheathKydexDropPoint = await _sheathRepository.GetById(new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"));
        var sheathLeatherClipPoint = await _sheathRepository.GetById(new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"));
        var sheathNylonTanto = await _sheathRepository.GetById(new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"));
        var sheathDeluxeLeatherDropPoint = await _sheathRepository.GetById(new Guid("18c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"));

        var sheathColorBlackKydex = await _sheathColorRepository.GetById(new Guid("a1a1a1a1-1111-4111-8111-a1a1a1a1a1a1"));
        var sheathColorBrownLeather = await _sheathColorRepository.GetById(new Guid("b2b2b2b2-2222-4222-8222-b2b2b2b2b2b2"));
        var sheathColorCoyoteNylon = await _sheathColorRepository.GetById(new Guid("d4d4d4d4-4444-4444-8444-d4d4d4d4d4d4"));

        var engravingTrident = await _engravingRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7e"));
        var engravingInitials = await _engravingRepository.GetById(new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"));
        var engravingKnot = await _engravingRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e90"));

        var attachmentClip = await _attachmentRepository.GetById(new Guid("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa"));
        var attachmentLanyard = await _attachmentRepository.GetById(new Guid("22222222-bbbb-4bbb-bbbb-bbbbbbbbbbbb"));
        var attachmentMolle = await _attachmentRepository.GetById(new Guid("33333333-cccc-4ccc-cccc-cccccccccccc"));
        var attachmentBead = await _attachmentRepository.GetById(new Guid("55555555-eeee-4eee-eeee-eeeeeeeeeeee"));

        var tagTactical = await _productTagRepository.GetById(new Guid("a7b8c9d0-e1f2-3456-789a-bcdef1234567"));
        var tagOutdoor = await _productTagRepository.GetById(new Guid("b8c9d0e1-f2a3-4567-89ab-cdef12345678"));
        var tagPremium = await _productTagRepository.GetById(new Guid("b2c3d4e5-f6a7-8901-2345-67890abcdef1"));
        var tagGift = await _productTagRepository.GetById(new Guid("c9d0e1f2-a3b4-5678-9abc-def123456789"));
        var tagHandmade = await _productTagRepository.GetById(new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"));

        var image1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var image3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        var image5 = await _fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));

        if (bladeDropPoint == null || bladeTanto == null || bladeSpearPoint == null || bladeClipPoint == null || colorSatin == null ||
            colorDLC == null || colorStonewash == null || handleG10 == null || handleMicarta == null ||
            handleTitanium == null || handleWood == null || sheathKydexDropPoint == null || sheathLeatherClipPoint == null ||
            sheathNylonTanto == null || sheathDeluxeLeatherDropPoint == null || sheathColorBlackKydex == null || 
            sheathColorBrownLeather == null || sheathColorCoyoteNylon == null || engravingTrident == null || 
            engravingInitials == null || engravingKnot == null || attachmentClip == null || attachmentLanyard == null || 
            attachmentMolle == null || attachmentBead == null || tagTactical == null || tagOutdoor == null || 
            tagPremium == null || tagGift == null || tagHandmade == null || image1 == null || image3 == null || image5 == null)
        {
            return;
        }
        
        var knife1 = new Knife(new Guid("4a7e35ec-57fa-4efb-bba6-3ef27ed4d168"), true, image1, new Translations(new Dictionary<string, string> { { "en", "Operator" }, { "ua", "Оператор" }, }), new Translations(new Dictionary<string, string> { { "en", "Tactical Tanto Knife 'Operator'" }, { "ua", "Тактичний ніж Танто 'Оператор'" }, }), new Translations(new Dictionary<string, string> { { "en", "A reliable tactical knife for any mission." }, { "ua", "Надійний тактичний ніж для будь-яких завдань." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Tactical Knife Operator" }, { "ua", "Купити тактичний ніж Оператор" }, }), new Translations(new Dictionary<string, string> { { "en", "Tactical knife with G10 handle and DLC coating." }, { "ua", "Тактичний ніж з руків'ям G10 та покриттям DLC." }, }), new List<ProductTag> { tagTactical }, bladeTanto, colorDLC, handleG10, sheathNylonTanto, sheathColorCoyoteNylon, new List<Engraving> { engravingTrident }, new List<Attachment> { attachmentMolle }, DateTime.UtcNow);
        var knife2 = new Knife(new Guid("eee42b50-e7c6-417c-a549-d752abef4f6b"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Pathfinder" }, { "ua", "Слідопит" }, }), new Translations(new Dictionary<string, string> { { "en", "Outdoor Drop Point Knife 'Pathfinder'" }, { "ua", "Ніж для активного відпочинку Дроп-поінт 'Слідопит'" }, }), new Translations(new Dictionary<string, string> { { "en", "A classic knife for hiking and outdoor activities." }, { "ua", "Класичний ніж для походів та активного відпочинку." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Outdoor Knife Pathfinder" }, { "ua", "Купити туристичний ніж Слідопит" }, }), new Translations(new Dictionary<string, string> { { "en", "Outdoor knife with Micarta handle and Kydex sheath." }, { "ua", "Туристичний ніж з руків'ям з мікарти та ножнами з кайдекса." }, }), new List<ProductTag> { tagOutdoor, tagHandmade }, bladeDropPoint, colorSatin, handleMicarta, sheathKydexDropPoint, sheathColorBlackKydex, null, new List<Attachment> { attachmentClip, attachmentLanyard }, DateTime.UtcNow);
        var knife3 = new Knife(new Guid("258c1001-a593-4283-b432-1b1c4e295700"), true, image5, new Translations(new Dictionary<string, string> { { "en", "Collector's Piece" }, { "ua", "Колекційний екземпляр" }, }), new Translations(new Dictionary<string, string> { { "en", "Premium Clip Point Knife" }, { "ua", "Преміальний ніж Кліп-поінт" }, }), new Translations(new Dictionary<string, string> { { "en", "An elegant and formidable premium knife." }, { "ua", "Елегантний та грізний преміальний ніж." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Premium Knife" }, { "ua", "Купити преміальний ніж" }, }), new Translations(new Dictionary<string, string> { { "en", "Premium knife with wood handle and leather sheath." }, { "ua", "Преміальний ніж з дерев'яним руків'ям та шкіряними ножнами." }, }), new List<ProductTag> { tagPremium, tagGift }, bladeClipPoint, colorSatin, handleWood, sheathLeatherClipPoint, sheathColorBrownLeather, new List<Engraving> { engravingInitials }, new List<Attachment> { attachmentBead }, DateTime.UtcNow);
        var knife4 = new Knife(new Guid("6ab86bca-df8a-43a4-8b75-ed44c741ae10"), false, image1, new Translations(new Dictionary<string, string> { { "en", "Stalker" }, { "ua", "Сталкер" }, }), new Translations(new Dictionary<string, string> { { "en", "Post-apocalyptic style knife 'Stalker'" }, { "ua", "Ніж у постапокаліптичному стилі 'Сталкер'" }, }), new Translations(new Dictionary<string, string> { { "en", "A knife that has seen it all." }, { "ua", "Ніж, що бачив усе." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Stalker Knife" }, { "ua", "Купити ніж Сталкер" }, }), new Translations(new Dictionary<string, string> { { "en", "Stonewashed tanto knife." }, { "ua", "Ніж танто з покриттям стоунвош." }, }), new List<ProductTag> { tagOutdoor, tagTactical }, bladeTanto, colorStonewash, handleMicarta, null, null, null, null, DateTime.UtcNow);
        var knife5 = new Knife(new Guid("bffee4ad-d3cf-4e9f-8876-6bbc120100a4"), true, image3, new Translations(new Dictionary<string, string> { { "en", "EDC Classic" }, { "ua", "EDC Класика" }, }), new Translations(new Dictionary<string, string> { { "en", "Classic everyday carry knife" }, { "ua", "Класичний ніж на кожен день" }, }), new Translations(new Dictionary<string, string> { { "en", "Simple, reliable, and effective." }, { "ua", "Простий, надійний та ефективний." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy EDC Knife" }, { "ua", "Купити EDC ніж" }, }), new Translations(new Dictionary<string, string> { { "en", "Drop point knife with G10 handle." }, { "ua", "Ніж дроп-поінт з руків'ям з G10." }, }), new List<ProductTag>(), bladeDropPoint, colorSatin, handleG10, null, null, null, new List<Attachment> { attachmentClip }, DateTime.UtcNow);
        var knife6 = new Knife(new Guid("ca047152-c3f2-4747-8fa6-caf3b73cd693"), true, image5, new Translations(new Dictionary<string, string> { { "en", "Vanguard" }, { "ua", "Авангард" }, }), new Translations(new Dictionary<string, string> { { "en", "Full tactical kit 'Vanguard'" }, { "ua", "Повний тактичний набір 'Авангард'" }, }), new Translations(new Dictionary<string, string> { { "en", "Fully equipped for any situation." }, { "ua", "Повністю екіпірований для будь-якої ситуації." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Vanguard Tactical Knife" }, { "ua", "Купити тактичний ніж Авангард" }, }), new Translations(new Dictionary<string, string> { { "en", "Drop point knife with all accessories." }, { "ua", "Ніж дроп-поінт з усіма аксесуарами." }, }), new List<ProductTag> { tagTactical, tagPremium }, bladeDropPoint, colorDLC, handleTitanium, sheathKydexDropPoint, sheathColorBlackKydex, new List<Engraving> { engravingTrident, engravingInitials }, new List<Attachment> { attachmentClip, attachmentMolle, attachmentLanyard }, DateTime.UtcNow);
        var knife7 = new Knife(new Guid("1950bd6d-5d31-4c3c-a2c5-4be5b65580ea"), true, image1, new Translations(new Dictionary<string, string> { { "en", "Forester" }, { "ua", "Лісник" }, }), new Translations(new Dictionary<string, string> { { "en", "Bushcraft knife 'Forester'" }, { "ua", "Ніж для бушкрафту 'Лісник'" }, }), new Translations(new Dictionary<string, string> { { "en", "Reliable tool for any forest trip." }, { "ua", "Надійний інструмент для будь-якої лісової подорожі." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Forester Knife" }, { "ua", "Купити ніж Лісник" }, }), new Translations(new Dictionary<string, string> { { "en", "Clip point knife with wooden handle." }, { "ua", "Ніж кліп-поінт з дерев'яним руків'ям." }, }), new List<ProductTag> { tagOutdoor, tagHandmade }, bladeClipPoint, colorStonewash, handleWood, null, null, null, null, DateTime.UtcNow);
        var knife8 = new Knife(new Guid("c785ecbe-7f3c-4b2b-bf9d-1cd0df319377"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Urban Ninja" }, { "ua", "Міський ніндзя" }, }), new Translations(new Dictionary<string, string> { { "en", "Compact tactical tanto" }, { "ua", "Компактний тактичний танто" }, }), new Translations(new Dictionary<string, string> { { "en", "Lightweight and concealable knife for urban environment." }, { "ua", "Легкий та прихований ніж для міських умов." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Urban Ninja Knife" }, { "ua", "Купити ніж Міський ніндзя" }, }), new Translations(new Dictionary<string, string> { { "en", "Tanto with G10 and DLC coating." }, { "ua", "Танто з G10 та покриттям DLC." }, }), new List<ProductTag> { tagTactical }, bladeTanto, colorDLC, handleG10, null, null, null, null, DateTime.UtcNow);
        var knife9 = new Knife(new Guid("e1b84a52-6e7e-42ab-979f-abcb41f3bd92"), true, image5, new Translations(new Dictionary<string, string> { { "en", "Centurion" }, { "ua", "Центуріон" }, }), new Translations(new Dictionary<string, string> { { "en", "Classic spear point knife" }, { "ua", "Класичний ніж спір-поінт" }, }), new Translations(new Dictionary<string, string> { { "en", "A modern interpretation of a classic design." }, { "ua", "Сучасна інтерпретація класичного дизайну." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Centurion Knife" }, { "ua", "Купити ніж Центуріон" }, }), new Translations(new Dictionary<string, string> { { "en", "Spear point with Micarta handle." }, { "ua", "Спір-поінт з руків'ям з мікарти." }, }), new List<ProductTag>(), bladeSpearPoint, colorSatin, handleMicarta, null, null, new List<Engraving> { engravingKnot }, null, DateTime.UtcNow);
        var knife10 = new Knife(new Guid("edd00e12-ea37-4a21-af76-f4236c3d72f7"), true, image1, new Translations(new Dictionary<string, string> { { "en", "Gentleman's Choice" }, { "ua", "Вибір джентльмена" }, }), new Translations(new Dictionary<string, string> { { "en", "Elegant gift knife" }, { "ua", "Елегантний подарунковий ніж" }, }), new Translations(new Dictionary<string, string> { { "en", "Perfect as a gift for a true connoisseur." }, { "ua", "Ідеальний як подарунок для справжнього поціновувача." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Gift Knife" }, { "ua", "Купити подарунковий ніж" }, }), new Translations(new Dictionary<string, string> { { "en", "Drop point with titanium handle and leather sheath." }, { "ua", "Дроп-поінт з титановим руків'ям та шкіряними ножнами." }, }), new List<ProductTag> { tagPremium, tagGift }, bladeDropPoint, colorSatin, handleTitanium, sheathDeluxeLeatherDropPoint, sheathColorBrownLeather, null, null, new DateTime(2025, 07, 10));

        await _knifeRepository.Create(knife1);
        await _knifeRepository.Create(knife2);
        await _knifeRepository.Create(knife3);
        await _knifeRepository.Create(knife4);
        await _knifeRepository.Create(knife5);
        await _knifeRepository.Create(knife6);
        await _knifeRepository.Create(knife7);
        await _knifeRepository.Create(knife8);
        await _knifeRepository.Create(knife9);
        await _knifeRepository.Create(knife10);
    }
}