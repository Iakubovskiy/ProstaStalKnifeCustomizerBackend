using Domain.Component.Engravings;
using Domain.Component.Engravings.Parameters;
using Domain.Component.Engravings.Support;
using Domain.Files;
using Domain.Translation;
using Infrastructure.Components;

namespace Infrastructure.Data.Component.Engravings;

public class EngravingSeeder : ISeeder
{
    public int Priority => 2;
    private readonly IComponentRepository<Engraving> _engravingRepository;
    private readonly IRepository<EngravingTag> _engravingTagRepository;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    private readonly IRepository<EngravingPrice> _engravingPriceRepository;

    public EngravingSeeder(
        IComponentRepository<Engraving> engravingRepository,
        IRepository<EngravingTag> engravingTagRepository,
        IRepository<FileEntity> fileEntityRepository,
        IRepository<EngravingPrice> engravingPriceRepository
    )
    {
        this._engravingRepository = engravingRepository;
        this._engravingTagRepository = engravingTagRepository;
        this._fileEntityRepository = fileEntityRepository;
        this._engravingPriceRepository = engravingPriceRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _engravingRepository.GetAll()).Any())
        {
            return;
        }

        var tagScandinavian = await _engravingTagRepository.GetById(new Guid("40050eb8-9c1e-48a4-8070-4edefd1c08f3"));
        var tagMilitary = await _engravingTagRepository.GetById(new Guid("9aef4a5e-013d-4bd1-94f7-dc1a93bbf0d1"));
        var tagFantasy = await _engravingTagRepository.GetById(new Guid("f1c1b3e6-44d6-45c2-880e-7d2e2c58a112"));
        var tagCeltic = await _engravingTagRepository.GetById(new Guid("b205b7b0-e6fc-47ef-9d7d-5c9c7d0adbb3"));
        var tagRunes = await _engravingTagRepository.GetById(new Guid("c3469c3d-e2b2-4e57-9d68-4cd2e51e4c89"));
        var tagMinimalist = await _engravingTagRepository.GetById(new Guid("2f6485f2-683b-4b98-8eb2-529c218180b6"));

        var picture1 = await _fileEntityRepository.GetById(new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"));
        var picture3 = await _fileEntityRepository.GetById(new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"));

        var standardPrice = await _engravingPriceRepository.GetById(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

        if (tagScandinavian == null || tagMilitary == null || tagFantasy == null || tagCeltic == null ||
            tagRunes == null || tagMinimalist == null || picture1 == null || picture3 == null || standardPrice == null)
        {
            return;
        }

        var engraving1 = new Engraving(
            new Guid("e1a1b2c3-d4e5-4f6a-8b9c-0d1e2f3a4b5c"),
            new Translations(new Dictionary<string, string> { { "en", "Vegvisir Compass" }, { "ua", "Компас Вегвізир" }, }),
            1, null, null, picture1,
            null,
            new EngravingPosition(0.0, 0.0, 0.1), new EngravingRotation(0.0, 0.0, 0.0), new EngravingScale(1.0, 1.0, 1.0),
            new List<EngravingTag> { tagScandinavian, tagRunes },
            new Translations(new Dictionary<string, string> { { "en", "Ancient runic compass" }, { "ua", "Стародавній рунічний компас" }, }),
            standardPrice.Price, true);

        var engraving2 = new Engraving(
            new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"),
            new Translations(new Dictionary<string, string> { { "en", "Custom Initials" }, { "ua", "Власні ініціали" }, }),
            2, "A.B.", "Times New Roman", null,
            null,
            new EngravingPosition(-2.0, 1.5, 0.0), new EngravingRotation(0.0, 0.0, 15.0), new EngravingScale(0.8, 0.8, 0.8),
            new List<EngravingTag> { tagMinimalist },
            new Translations(new Dictionary<string, string> { { "en", "Personalized text engraving" }, { "ua", "Персоналізоване гравіювання тексту" }, }),
            standardPrice.Price, true);

        var engraving3 = new Engraving(
            new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7e"),
            new Translations(new Dictionary<string, string> { { "en", "Trident" }, { "ua", "Тризуб" }, }),
            1, null, null, picture3, null,
            new EngravingPosition(0.0, -1.0, 0.05), new EngravingRotation(0.0, 0.0, 0.0), new EngravingScale(1.2, 1.2, 1.2),
            new List<EngravingTag> { tagMilitary },
            new Translations(new Dictionary<string, string> { { "en", "Ukrainian coat of arms" }, { "ua", "Герб України" }, }),
            standardPrice.Price, true);
            
        var engraving4 = new Engraving(
            new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8f"),
            new Translations(new Dictionary<string, string> { { "en", "Motto" }, { "ua", "Девіз" }, }),
            1, "Fortis Fortuna Adiuvat", "Arial", null,
            null,
            new EngravingPosition(1.0, 2.5, 0.0), new EngravingRotation(0.0, 0.0, -5.0), new EngravingScale(0.9, 0.9, 0.9),
            new List<EngravingTag>(),
            new Translations(new Dictionary<string, string> { { "en", "Fortune favors the bold" }, { "ua", "Фортуна допомагає сміливим" }, }),
            standardPrice.Price, false);
            
        var engraving5 = new Engraving(
            new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e90"),
            new Translations(new Dictionary<string, string> { { "en", "Celtic Knot" }, { "ua", "Кельтський вузол" }, }),
            2, null, null, picture1,
            null,
            new EngravingPosition(0.0, 0.0, 0.1), new EngravingRotation(0.0, 0.0, 45.0), new EngravingScale(1.1, 1.1, 1.1),
            new List<EngravingTag> { tagCeltic },
            new Translations(new Dictionary<string, string> { { "en", "Traditional Celtic pattern" }, { "ua", "Традиційний кельтський візерунок" }, }),
            standardPrice.Price, true);
            
        var engraving6 = new Engraving(
            new Guid("d6e7f8a9-b0c1-4d2e-3f4a-5b6c7d8e9f12"),
            new Translations(new Dictionary<string, string> { { "en", "Dragon" }, { "ua", "Дракон" }, }),
            1, null, null, picture3,
            null,
            new EngravingPosition(1.0, -1.5, 0.2), new EngravingRotation(0.0, 0.0, 0.0), new EngravingScale(1.5, 1.5, 1.5),
            new List<EngravingTag> { tagFantasy },
            new Translations(new Dictionary<string, string> { { "en", "Mythical dragon engraving" }, { "ua", "Гравіювання міфічного дракона" }, }),
            standardPrice.Price, true);
            
        var engraving7 = new Engraving(
            new Guid("e7f8a9b0-c1d2-4e3f-4a5b-6c7d8e9f1234"),
            new Translations(new Dictionary<string, string> { { "en", "Quote" }, { "ua", "Цитата" }, }),
            2, "Per aspera ad astra", "Georgia", null,
            null,
            new EngravingPosition(0.0, 3.0, 0.0), new EngravingRotation(0.0, 0.0, 0.0), new EngravingScale(0.7, 0.7, 0.7),
            new List<EngravingTag>(),
            new Translations(new Dictionary<string, string> { { "en", "Through hardship to the stars" }, { "ua", "Крізь терни до зірок" }, }),
            standardPrice.Price, true);
            
        var engraving8 = new Engraving(
            new Guid("f8a9b0c1-d2e3-4f4a-5b6c-7d8e9f123456"),
            new Translations(new Dictionary<string, string> { { "en", "Thor's Hammer" }, { "ua", "Молот Тора" }, }),
            1, null, null, picture1,
            null,
            new EngravingPosition(-0.5, 0.5, 0.15), new EngravingRotation(0.0, 0.0, -10.0), new EngravingScale(1.3, 1.3, 1.3),
            new List<EngravingTag> { tagScandinavian, tagFantasy },
            new Translations(new Dictionary<string, string> { { "en", "Mjölnir symbol" }, { "ua", "Символ Мйольнір" }, }),
            standardPrice.Price, true);
            
        var engraving9 = new Engraving(
            new Guid("a9b0c1d2-e3f4-4a5b-6c7d-8e9f12345678"),
            new Translations(new Dictionary<string, string> { { "en", "Important Date" }, { "ua", "Важлива дата" }, }),
            2, "24.08.1991", "Verdana", null,
            null,
            new EngravingPosition(2.0, -2.0, 0.0), new EngravingRotation(0.0, 0.0, 0.0), new EngravingScale(0.6, 0.6, 0.6),
            new List<EngravingTag> { tagMinimalist },
            new Translations(new Dictionary<string, string> { { "en", "Memorable date" }, { "ua", "Пам'ятна дата" }, }),
            standardPrice.Price, false);
            
        var engraving10 = new Engraving(
            new Guid("b0c1d2e3-f4a5-4b6c-7d8e-9f1234567890"),
            new Translations(new Dictionary<string, string> { { "en", "Wolf" }, { "ua", "Вовк" }, }),
            1, null, null, picture3,
            null,
            new EngravingPosition(0.0, 0.0, 0.1), new EngravingRotation(0.0, 0.0, 0.0), new EngravingScale(1.0, 1.0, 1.0),
            new List<EngravingTag> { tagScandinavian },
            new Translations(new Dictionary<string, string> { { "en", "Lone wolf engraving" }, { "ua", "Гравіювання самотнього вовка" }, }),
            standardPrice.Price, true);

        await _engravingRepository.Create(engraving1);
        await _engravingRepository.Create(engraving2);
        await _engravingRepository.Create(engraving3);
        await _engravingRepository.Create(engraving4);
        await _engravingRepository.Create(engraving5);
        await _engravingRepository.Create(engraving6);
        await _engravingRepository.Create(engraving7);
        await _engravingRepository.Create(engraving8);
        await _engravingRepository.Create(engraving9);
        await _engravingRepository.Create(engraving10);
    }
}