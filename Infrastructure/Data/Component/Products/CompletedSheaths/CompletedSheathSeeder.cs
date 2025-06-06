using Domain.Component.Engravings;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Sheaths;
using Domain.Component.Sheaths.Color;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;
using Infrastructure.Components.Sheaths.Color;

namespace Infrastructure.Data.Component.Products.CompletedSheaths;

public class CompletedSheathSeeder : ISeeder
{
    public int Priority => 5;
    private readonly IComponentRepository<CompletedSheath> _completedSheathRepository;
    private readonly IRepository<ProductTag> _productTagRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly IComponentRepository<Sheath> _sheathRepository;
    private readonly ISheathColorRepository _sheathColorRepository;
    private readonly IComponentRepository<Engraving> _engravingRepository;
    private readonly IComponentRepository<Attachment> _attachmentRepository;

    public CompletedSheathSeeder(
        IComponentRepository<CompletedSheath> completedSheathRepository,
        IRepository<ProductTag> productTagRepository,
        IRepository<FileEntity> fileEntityRepository,
        IComponentRepository<Sheath> sheathRepository,
        ISheathColorRepository sheathColorRepository,
        IComponentRepository<Engraving> engravingRepository,
        IComponentRepository<Attachment> attachmentRepository
    )
    {
        this._completedSheathRepository = completedSheathRepository;
        this._productTagRepository = productTagRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._sheathRepository = sheathRepository;
        this._sheathColorRepository = sheathColorRepository;
        this._engravingRepository = engravingRepository;
        this._attachmentRepository = attachmentRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _completedSheathRepository.GetAll()).Any())
        {
            return;
        }

        var sheathKydexDropPoint = await _sheathRepository.GetById(new Guid("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"));
        var sheathLeatherClipPoint = await _sheathRepository.GetById(new Guid("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"));
        var sheathNylonTanto = await _sheathRepository.GetById(new Guid("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"));
        var sheathDeluxeLeatherDropPoint = await _sheathRepository.GetById(new Guid("18c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"));

        var sheathColorBlackKydex = await _sheathColorRepository.GetById(new Guid("a1a1a1a1-1111-4111-8111-a1a1a1a1a1a1"));
        var sheathColorBrownLeather = await _sheathColorRepository.GetById(new Guid("b2b2b2b2-2222-4222-8222-b2b2b2b2b2b2"));
        var sheathColorCoyoteNylon = await _sheathColorRepository.GetById(new Guid("d4d4d4d4-4444-4444-8444-d4d4d4d4d4d4"));
        var sheathColorOliveKydex = await _sheathColorRepository.GetById(new Guid("c3c3c3c3-3333-4333-8333-c3c3c3c3c3c3"));

        var engravingTrident = await _engravingRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7e"));
        var engravingKnot = await _engravingRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e90"));
        
        var attachmentClip = await _attachmentRepository.GetById(new Guid("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa"));
        var attachmentMolle = await _attachmentRepository.GetById(new Guid("33333333-cccc-4ccc-cccc-cccccccccccc"));
        var attachmentBead = await _attachmentRepository.GetById(new Guid("55555555-eeee-4eee-eeee-eeeeeeeeeeee"));

        var tagTactical = await _productTagRepository.GetById(new Guid("a7b8c9d0-e1f2-3456-789a-bcdef1234567"));
        var tagHandmade = await _productTagRepository.GetById(new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"));
        var tagPremium = await _productTagRepository.GetById(new Guid("b2c3d4e5-f6a7-8901-2345-67890abcdef1"));

        var image1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var image3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        
        if (sheathKydexDropPoint == null || sheathLeatherClipPoint == null || sheathNylonTanto == null || 
            sheathDeluxeLeatherDropPoint == null || sheathColorBlackKydex == null || sheathColorBrownLeather == null || 
            sheathColorCoyoteNylon == null || sheathColorOliveKydex == null || engravingTrident == null || 
            engravingKnot == null || attachmentClip == null || attachmentMolle == null || attachmentBead == null ||
            tagTactical == null || tagHandmade == null || tagPremium == null || image1 == null || image3 == null)
        {
            return;
        }

        var completedSheath1 = new CompletedSheath( new Guid("5124e75e-b8ad-410e-b1fa-4ef24147087a"), true, image1, new Translations(new Dictionary<string, string> { { "en", "Tactical Kydex Sheath" }, { "ua", "Тактичні ножни з Кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Black Kydex Sheath with MOLLE lock" }, { "ua", "Чорні ножни з Кайдекса з кріпленням MOLLE" }, }), new Translations(new Dictionary<string, string> { { "en", "Durable Kydex sheath for Drop Point blades, equipped with a MOLLE lock." }, { "ua", "Міцні ножни з Кайдекса для клинків Дроп-поінт, оснащені кріпленням MOLLE." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Kydex Sheath" }, { "ua", "Купити ножни з Кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Kydex sheath for drop point knives." }, { "ua", "Ножни з кайдекса для ножів дроп-поінт." }, }), new List<ProductTag> { tagTactical }, sheathKydexDropPoint, sheathColorBlackKydex, null, new List<Attachment> { attachmentMolle });
        var completedSheath2 = new CompletedSheath( new Guid("67b58778-cded-46e0-91e6-a3abf1b8970e"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Handmade Leather Sheath" }, { "ua", "Шкіряні ножни ручної роботи" }, }), new Translations(new Dictionary<string, string> { { "en", "Premium Brown Leather Sheath" }, { "ua", "Преміальні коричневі шкіряні ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Classic leather sheath for Clip Point blades." }, { "ua", "Класичні шкіряні ножни для клинків Кліп-поінт." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Leather Sheath" }, { "ua", "Купити шкіряні ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Handmade leather sheath for hunting knives." }, { "ua", "Шкіряні ножни ручної роботи для мисливських ножів." }, }), new List<ProductTag> { tagHandmade, tagPremium }, sheathLeatherClipPoint, sheathColorBrownLeather, null, null);
        var completedSheath3 = new CompletedSheath( new Guid("5e4efd9a-ef73-47bc-8a00-ab71f2e1ea34"), false, image1, new Translations(new Dictionary<string, string> { { "en", "Nylon Sheath with Engraving" }, { "ua", "Нейлонові ножни з гравіюванням" }, }), new Translations(new Dictionary<string, string> { { "en", "Coyote Nylon Sheath for Tanto" }, { "ua", "Нейлонові ножни кольору койот для Танто" }, }), new Translations(new Dictionary<string, string> { { "en", "Practical nylon sheath with a trident engraving." }, { "ua", "Практичні нейлонові ножни з гравіюванням тризуба." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Nylon Sheath" }, { "ua", "Купити нейлонові ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Nylon sheath for tanto blades." }, { "ua", "Нейлонові ножни для клинків танто." }, }), new List<ProductTag> { tagTactical }, sheathNylonTanto, sheathColorCoyoteNylon, new List<Engraving> { engravingTrident }, null);
        var completedSheath4 = new CompletedSheath( new Guid("b174742f-29be-4476-8cb2-20e860a19e8a"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Deluxe Leather Sheath Set" }, { "ua", "Набір 'Делюкс' зі шкіряними ножнами" }, }), new Translations(new Dictionary<string, string> { { "en", "Deluxe Leather Sheath with Bead" }, { "ua", "Шкіряні ножни 'Делюкс' з намистиною" }, }), new Translations(new Dictionary<string, string> { { "en", "Premium leather sheath for Drop Point blades, comes with a bronze bead." }, { "ua", "Преміальні шкіряні ножни для клинків Дроп-поінт, поставляються з бронзовою намистиною." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Deluxe Sheath" }, { "ua", "Купити ножни 'Делюкс'" }, }), new Translations(new Dictionary<string, string> { { "en", "The perfect gift set with a leather sheath." }, { "ua", "Ідеальний подарунковий набір зі шкіряними ножнами." }, }), new List<ProductTag> { tagPremium, tagHandmade }, sheathDeluxeLeatherDropPoint, sheathColorBrownLeather, null, new List<Attachment> { attachmentBead });
        var completedSheath5 = new CompletedSheath( new Guid("85a3330f-a882-4ed3-beb5-75501d48b9e7"), true, image1, new Translations(new Dictionary<string, string> { { "en", "Minimalist Kydex Sheath" }, { "ua", "Мінімалістичні ножни з Кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Olive Kydex Sheath with Clip" }, { "ua", "Оливкові ножни з Кайдекса з кліпсою" }, }), new Translations(new Dictionary<string, string> { { "en", "A simple and reliable Kydex sheath with a belt clip." }, { "ua", "Прості та надійні ножни з Кайдекса з кліпсою на пояс." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Minimalist Sheath" }, { "ua", "Купити мінімалістичні ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Compact Kydex sheath." }, { "ua", "Компактні ножни з Кайдекса." }, }), new List<ProductTag>(), sheathKydexDropPoint, sheathColorOliveKydex, null, new List<Attachment> { attachmentClip });
        var completedSheath6 = new CompletedSheath( new Guid("8ad7812a-72d2-4836-916d-0e98621f0f95"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Engraved Leather Sheath" }, { "ua", "Шкіряні ножни з гравіюванням" }, }), new Translations(new Dictionary<string, string> { { "en", "Leather Sheath with Celtic Knot" }, { "ua", "Шкіряні ножни з кельтським вузлом" }, }), new Translations(new Dictionary<string, string> { { "en", "Beautiful leather sheath featuring a Celtic knot engraving." }, { "ua", "Красиві шкіряні ножни з гравіюванням кельтського вузла." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Engraved Leather Sheath" }, { "ua", "Купити шкіряні ножни з гравіюванням" }, }), new Translations(new Dictionary<string, string> { { "en", "Unique handmade leather sheath." }, { "ua", "Унікальні шкіряні ножни ручної роботи." }, }), new List<ProductTag> { tagHandmade }, sheathLeatherClipPoint, sheathColorBrownLeather, new List<Engraving> { engravingKnot }, null);
        var completedSheath7 = new CompletedSheath( new Guid("c0045538-d92a-471f-8ff4-9535e68c180b"), true, image1, new Translations(new Dictionary<string, string> { { "en", "Basic Nylon Sheath" }, { "ua", "Базові нейлонові ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Coyote Nylon Sheath" }, { "ua", "Нейлонові ножни 'Койот'" }, }), new Translations(new Dictionary<string, string> { { "en", "A simple and affordable nylon sheath." }, { "ua", "Прості та доступні нейлонові ножни." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Nylon Sheath" }, { "ua", "Купити нейлонові ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Sheath for tanto knives." }, { "ua", "Ножни для ножів танто." }, }), new List<ProductTag>(), sheathNylonTanto, sheathColorCoyoteNylon, null, null);
        var completedSheath8 = new CompletedSheath( new Guid("2f5f6373-1d0d-4b1a-992d-8a3d5326a6a7"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Fully Kitted Sheath" }, { "ua", "Повністю укомплектовані ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Ultimate Kydex Sheath Kit" }, { "ua", "Набір 'Ультиматум' з ножнами з Кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "The ultimate package: Kydex sheath with multiple engravings and attachments." }, { "ua", "Ультимативний набір: ножни з Кайдекса з кількома гравіюваннями та кріпленнями." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Ultimate Sheath Kit" }, { "ua", "Купити набір 'Ультиматум'" }, }), new Translations(new Dictionary<string, string> { { "en", "A complete solution for your knife." }, { "ua", "Комплексне рішення для вашого ножа." }, }), new List<ProductTag> { tagTactical, tagPremium }, sheathKydexDropPoint, sheathColorBlackKydex, new List<Engraving> { engravingTrident, engravingKnot }, new List<Attachment> { attachmentClip, attachmentMolle });
        var completedSheath9 = new CompletedSheath( new Guid("59d8db58-4de6-4f28-91a6-21944321ee66"), false, image1, new Translations(new Dictionary<string, string> { { "en", "Olive Kydex Sheath" }, { "ua", "Оливкові ножни з Кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Sheath for Drop Point" }, { "ua", "Ножни для Дроп-поінт" }, }), new Translations(new Dictionary<string, string> { { "en", "Olive drab Kydex sheath." }, { "ua", "Ножни з Кайдекса кольору олива." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Olive Kydex Sheath" }, { "ua", "Купити оливкові ножни з Кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Sheath for outdoor knives." }, { "ua", "Ножни для туристичних ножів." }, }), new List<ProductTag>(), sheathKydexDropPoint, sheathColorOliveKydex, null, null);
        var completedSheath10 = new CompletedSheath( new Guid("49a132d4-74be-4c3c-9904-ea3786037b43"), true, image3, new Translations(new Dictionary<string, string> { { "en", "Classic Leather Sheath" }, { "ua", "Класичні шкіряні ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "Brown Leather Sheath for Clip Point" }, { "ua", "Коричневі шкіряні ножни для Кліп-поінт" }, }), new Translations(new Dictionary<string, string> { { "en", "A timeless classic for your favorite knife." }, { "ua", "Нев'януча класика для вашого улюбленого ножа." }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Classic Leather Sheath" }, { "ua", "Купити класичні шкіряні ножни" }, }), new Translations(new Dictionary<string, string> { { "en", "High-quality leather sheath." }, { "ua", "Високоякісні шкіряні ножни." }, }), new List<ProductTag> { tagHandmade }, sheathLeatherClipPoint, sheathColorBrownLeather, null, new List<Attachment> { attachmentClip });

        await _completedSheathRepository.Create(completedSheath1);
        await _completedSheathRepository.Create(completedSheath2);
        await _completedSheathRepository.Create(completedSheath3);
        await _completedSheathRepository.Create(completedSheath4);
        await _completedSheathRepository.Create(completedSheath5);
        await _completedSheathRepository.Create(completedSheath6);
        await _completedSheathRepository.Create(completedSheath7);
        await _completedSheathRepository.Create(completedSheath8);
        await _completedSheathRepository.Create(completedSheath9);
        await _completedSheathRepository.Create(completedSheath10);
    }
}