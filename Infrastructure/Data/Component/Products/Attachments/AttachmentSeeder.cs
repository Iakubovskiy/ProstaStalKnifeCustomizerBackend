using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;

namespace Infrastructure.Data.Component.Products.Attachments;

public class AttachmentSeeder : ISeeder
{
    public int Priority => 2;
    private readonly IComponentRepository<Attachment> _attachmentRepository;
    private readonly IRepository<AttachmentType> _attachmentTypeRepository;
    private readonly IRepository<ProductTag> _productTagRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;

    public AttachmentSeeder(
        IComponentRepository<Attachment> attachmentRepository,
        IRepository<AttachmentType> attachmentTypeRepository,
        IRepository<ProductTag> productTagRepository,
        IRepository<FileEntity> fileEntityRepository
    )
    {
        this._attachmentRepository = attachmentRepository;
        this._attachmentTypeRepository = attachmentTypeRepository;
        this._productTagRepository = productTagRepository;
        this._fileEntityRepository = fileEntityRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _attachmentRepository.GetAll()).Any())
        {
            return;
        }

        var typeClip = await _attachmentTypeRepository.GetById(new Guid("1a1b1c1d-1e1f-4a2b-8c3d-4e5f6a7b8c9d"));
        var typeLanyard = await _attachmentTypeRepository.GetById(new Guid("2b2c2d2e-2f3a-4b3c-9d4e-5f6a7b8c9d0e"));
        var typeMolle = await _attachmentTypeRepository.GetById(new Guid("4d4e4f4a-4b5c-4d5e-bf6a-7b8c9d0e1f2a"));
        var typeParacord = await _attachmentTypeRepository.GetById(new Guid("6f6a6b6c-6d7e-4f7a-d18c-9d0e1f2a3b4c"));
        var typeBead = await _attachmentTypeRepository.GetById(new Guid("5e5f5a5b-5c6d-4e6f-c07b-8c9d0e1f2a3b"));
        var typeBeltLoop = await _attachmentTypeRepository.GetById(new Guid("9c9d9e9f-9a0b-4cac-04bf-2a3b4c5d6e7f"));

        var tagTactical = await _productTagRepository.GetById(new Guid("a7b8c9d0-e1f2-3456-789a-bcdef1234567"));
        var tagOutdoor = await _productTagRepository.GetById(new Guid("b8c9d0e1-f2a3-4567-89ab-cdef12345678"));
        var tagHandmade = await _productTagRepository.GetById(new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"));
        var tagPremium = await _productTagRepository.GetById(new Guid("b2c3d4e5-f6a7-8901-2345-67890abcdef1"));
        var tagGift = await _productTagRepository.GetById(new Guid("c9d0e1f2-a3b4-5678-9abc-def123456789"));

        var file1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var file2 = await _fileEntityRepository.GetById(new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"));
        var file3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));
        var file4 = await _fileEntityRepository.GetById(new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"));
        var file5 = await _fileEntityRepository.GetById(new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"));

        if (typeClip == null || typeLanyard == null || typeMolle == null || typeParacord == null || typeBead == null ||
            typeBeltLoop == null || tagTactical == null || tagOutdoor == null || tagHandmade == null || 
            tagPremium == null || tagGift == null || file1 == null || file2 == null || file3 == null ||
            file4 == null || file5 == null)
        {
            return;
        }

        var attachment1 = new Attachment(new Guid("11111111-aaaa-4aaa-aaaa-aaaaaaaaaaaa"), true, file1, new Translations(new Dictionary<string, string> { { "en", "Titanium Belt Clip" }, { "ua", "Титанова кліпса" }, }), new Translations(new Dictionary<string, string> { { "en", "Durable Titanium Clip" }, { "ua", "Міцна титанова кліпса" }, }), new Translations(new Dictionary<string, string> { { "en", "Lightweight and strong belt clip for sheaths." }, { "ua", "Легка та міцна кліпса на пояс для ножен." }, }), new Translations(new Dictionary<string, string> { { "en", "Titanium Belt Clip for Knife" }, { "ua", "Титанова кліпса на пояс для ножа" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy titanium belt clip for sheaths." }, { "ua", "Купити титанову кліпсу для ножен." }, }), new List<ProductTag> { tagTactical, tagPremium }, typeClip, new Translations(new Dictionary<string, string> { { "en", "Gray" }, { "ua", "Сірий" }, }), 450.0, new Translations(new Dictionary<string, string> { { "en", "Titanium" }, { "ua", "Титан" }, }), file3, DateTime.UtcNow);
        var attachment2 = new Attachment(new Guid("22222222-bbbb-4bbb-bbbb-bbbbbbbbbbbb"), true, file2, new Translations(new Dictionary<string, string> { { "en", "Braided Paracord Lanyard" }, { "ua", "Плетений темляк з паракорду" }, }), new Translations(new Dictionary<string, string> { { "en", "Handmade Paracord Lanyard" }, { "ua", "Темляк з паракорду ручної роботи" }, }), new Translations(new Dictionary<string, string> { { "en", "Customizable handmade paracord lanyard." }, { "ua", "Темляк з паракорду ручної роботи з можливістю кастомізації." }, }), new Translations(new Dictionary<string, string> { { "en", "Paracord Lanyard for Knives" }, { "ua", "Темляк з паракорду для ножів" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy paracord lanyard for knives." }, { "ua", "Купити темляк з паракорду для ножів." }, }), new List<ProductTag> { tagHandmade, tagOutdoor }, typeLanyard, new Translations(new Dictionary<string, string> { { "en", "Olive" }, { "ua", "Олива" }, }), 200.0, new Translations(new Dictionary<string, string> { { "en", "Paracord 550" }, { "ua", "Паракорд 550" }, }), file4, DateTime.UtcNow);
        var attachment3 = new Attachment(new Guid("33333333-cccc-4ccc-cccc-cccccccccccc"), false, file3, new Translations(new Dictionary<string, string> { { "en", "Polymer MOLLE Lock" }, { "ua", "Полімерне кріплення MOLLE" }, }), new Translations(new Dictionary<string, string> { { "en", "Versatile MOLLE Lock System" }, { "ua", "Універсальна система кріплення MOLLE" }, }), new Translations(new Dictionary<string, string> { { "en", "Securely attach your sheath to any MOLLE gear." }, { "ua", "Надійно кріпіть ваші ножни до будь-якого MOLLE-спорядження." }, }), new Translations(new Dictionary<string, string> { { "en", "MOLLE Lock for Sheaths" }, { "ua", "Кріплення MOLLE для ножен" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy MOLLE lock for tactical gear." }, { "ua", "Купити кріплення MOLLE для тактичного спорядження." }, }), new List<ProductTag> { tagTactical }, typeMolle, new Translations(new Dictionary<string, string> { { "en", "Black" }, { "ua", "Чорний" }, }), 300.0, new Translations(new Dictionary<string, string> { { "en", "Polymer" }, { "ua", "Полімер" }, }), file5, DateTime.UtcNow);
        var attachment4 = new Attachment(new Guid("44444444-dddd-4ddd-dddd-dddddddddddd"), true, file4, new Translations(new Dictionary<string, string> { { "en", "Survival Paracord (10m)" }, { "ua", "Паракорд для виживання (10м)" }, }), new Translations(new Dictionary<string, string> { { "en", "10 meters of strong paracord" }, { "ua", "10 метрів міцного паракорду" }, }), new Translations(new Dictionary<string, string> { { "en", "High-quality 550 paracord for all your needs." }, { "ua", "Високоякісний паракорд 550 для будь-яких потреб." }, }), new Translations(new Dictionary<string, string> { { "en", "Paracord 10m Coil" }, { "ua", "Паракорд 10м моток" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy 550 paracord." }, { "ua", "Купити паракорд 550." }, }), new List<ProductTag> { tagOutdoor }, typeParacord, new Translations(new Dictionary<string, string> { { "en", "Coyote Brown" }, { "ua", "Койот" }, }), 150.0, new Translations(new Dictionary<string, string> { { "en", "Nylon" }, { "ua", "Нейлон" }, }), file1, DateTime.UtcNow);
        var attachment5 = new Attachment(new Guid("55555555-eeee-4eee-eeee-eeeeeeeeeeee"), true, file5, new Translations(new Dictionary<string, string> { { "en", "Bronze Lanyard Bead" }, { "ua", "Бронзова намистина для темляка" }, }), new Translations(new Dictionary<string, string> { { "en", "Skull Lanyard Bead" }, { "ua", "Намистина для темляка 'Череп'" }, }), new Translations(new Dictionary<string, string> { { "en", "Hand-casted bronze bead for your lanyard." }, { "ua", "Бронзова намистина ручного лиття для вашого темляка." }, }), new Translations(new Dictionary<string, string> { { "en", "Bronze Skull Lanyard Bead" }, { "ua", "Бронзова намистина Череп" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy lanyard bead." }, { "ua", "Купити намистину для темляка." }, }), new List<ProductTag> { tagHandmade, tagGift, tagPremium }, typeBead, new Translations(new Dictionary<string, string> { { "en", "Bronze" }, { "ua", "Бронза" }, }), 600.0, new Translations(new Dictionary<string, string> { { "en", "Bronze" }, { "ua", "Бронза" }, }), file2, DateTime.UtcNow);
        var attachment6 = new Attachment(new Guid("66666666-ffff-4fff-ffff-ffffffffffff"), true, file1, new Translations(new Dictionary<string, string> { { "en", "Leather Belt Loop" }, { "ua", "Шкіряна петля на пояс" }, }), new Translations(new Dictionary<string, string> { { "en", "Classic Leather Loop for Sheath" }, { "ua", "Класична шкіряна петля для ножен" }, }), new Translations(new Dictionary<string, string> { { "en", "Made from high-quality vegetable-tanned leather." }, { "ua", "Виготовлено з високоякісної шкіри рослинного дублення." }, }), new Translations(new Dictionary<string, string> { { "en", "Leather Belt Loop" }, { "ua", "Шкіряна петля" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy leather belt loop." }, { "ua", "Купити шкіряну петлю на пояс." }, }), new List<ProductTag> { tagHandmade }, typeBeltLoop, new Translations(new Dictionary<string, string> { { "en", "Dark Brown" }, { "ua", "Темно-коричневий" }, }), 250.0, new Translations(new Dictionary<string, string> { { "en", "Leather" }, { "ua", "Шкіра" }, }), file3, DateTime.UtcNow);
        var attachment7 = new Attachment(new Guid("77777777-a1a1-4a1a-a1a1-a1a1a1a1a1a1"), true, file2, new Translations(new Dictionary<string, string> { { "en", "Kydex Belt Clip" }, { "ua", "Кліпса з кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Adjustable Kydex Clip" }, { "ua", "Регульована кліпса з кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Durable and adjustable clip for Kydex sheaths." }, { "ua", "Міцна та регульована кліпса для ножен з кайдекса." }, }), new Translations(new Dictionary<string, string> { { "en", "Kydex Belt Clip" }, { "ua", "Кліпса з кайдекса" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy Kydex clip." }, { "ua", "Купити кліпсу з кайдекса." }, }), new List<ProductTag> { tagTactical }, typeClip, new Translations(new Dictionary<string, string> { { "en", "Black" }, { "ua", "Чорний" }, }), 180.0, new Translations(new Dictionary<string, string> { { "en", "Kydex" }, { "ua", "Кайдекс" }, }), file4, DateTime.UtcNow);
        var attachment8 = new Attachment(new Guid("88888888-b2b2-4b2b-b2b2-b2b2b2b2b2b2"), false, file3, new Translations(new Dictionary<string, string> { { "en", "Black Paracord Lanyard" }, { "ua", "Чорний темляк з паракорду" }, }), new Translations(new Dictionary<string, string> { { "en", "Simple and reliable lanyard." }, { "ua", "Простий та надійний темляк." }, }), new Translations(new Dictionary<string, string> { { "en", "Minimalistic black paracord lanyard." }, { "ua", "Мінімалістичний чорний темляк з паракорду." }, }), new Translations(new Dictionary<string, string> { { "en", "Black Lanyard" }, { "ua", "Чорний темляк" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy black lanyard." }, { "ua", "Купити чорний темляк." }, }), new List<ProductTag>(), typeLanyard, new Translations(new Dictionary<string, string> { { "en", "Black" }, { "ua", "Чорний" }, }), 120.0, new Translations(new Dictionary<string, string> { { "en", "Paracord 550" }, { "ua", "Паракорд 550" }, }), file5, DateTime.UtcNow);
        var attachment9 = new Attachment(new Guid("99999999-c3c3-4c3c-c3c3-c3c3c3c3c3c3"), true, file4, new Translations(new Dictionary<string, string> { { "en", "Titanium Lanyard Bead 'Helmet'" }, { "ua", "Титанова намистина 'Шолом'" }, }), new Translations(new Dictionary<string, string> { { "en", "Exclusive Titanium Bead" }, { "ua", "Ексклюзивна титанова намистина" }, }), new Translations(new Dictionary<string, string> { { "en", "A detailed helmet bead made of titanium." }, { "ua", "Деталізована намистина-шолом, виготовлена з титану." }, }), new Translations(new Dictionary<string, string> { { "en", "Titanium Helmet Bead" }, { "ua", "Титанова намистина Шолом" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy exclusive lanyard bead." }, { "ua", "Купити ексклюзивну намистину для темляка." }, }), new List<ProductTag> { tagPremium, tagGift }, typeBead, new Translations(new Dictionary<string, string> { { "en", "Stonewash" }, { "ua", "Стоунвош" }, }), 900.0, new Translations(new Dictionary<string, string> { { "en", "Titanium" }, { "ua", "Титан" }, }), file1, DateTime.UtcNow);
        var attachment10 = new Attachment(new Guid("aaaaaaaa-d4d4-4d4d-d4d4-d4d4d4d4d4d4"), true, file5, new Translations(new Dictionary<string, string> { { "en", "MOLLE Clip Set (2 pcs)" }, { "ua", "Набір кріплень MOLLE (2 шт)" }, }), new Translations(new Dictionary<string, string> { { "en", "Set of two MOLLE clips" }, { "ua", "Набір з двох кліпс MOLLE" }, }), new Translations(new Dictionary<string, string> { { "en", "A set of two reliable MOLLE locks for your gear." }, { "ua", "Набір з двох надійних кріплень MOLLE для вашого спорядження." }, }), new Translations(new Dictionary<string, string> { { "en", "MOLLE Clips Set" }, { "ua", "Набір кліпс MOLLE" }, }), new Translations(new Dictionary<string, string> { { "en", "Buy MOLLE clips set." }, { "ua", "Купити набір кліпс MOLLE." }, }), new List<ProductTag> { tagTactical, tagOutdoor }, typeMolle, new Translations(new Dictionary<string, string> { { "en", "Tan" }, { "ua", "Тан" }, }), 550.0, new Translations(new Dictionary<string, string> { { "en", "Polymer" }, { "ua", "Полімер" }, }), file2, DateTime.UtcNow);

        await _attachmentRepository.Create(attachment1);
        await _attachmentRepository.Create(attachment2);
        await _attachmentRepository.Create(attachment3);
        await _attachmentRepository.Create(attachment4);
        await _attachmentRepository.Create(attachment5);
        await _attachmentRepository.Create(attachment6);
        await _attachmentRepository.Create(attachment7);
        await _attachmentRepository.Create(attachment8);
        await _attachmentRepository.Create(attachment9);
        await _attachmentRepository.Create(attachment10);
    }
}