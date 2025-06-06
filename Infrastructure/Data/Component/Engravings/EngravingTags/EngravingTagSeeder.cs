using Domain.Component.Engravings.Support;
using Domain.Translation;

namespace Infrastructure.Data.Component.Engravings.EngravingTags;

public class EngravingTagSeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<EngravingTag> _engravingTagRepository;
    public EngravingTagSeeder(IRepository<EngravingTag> engravingTagRepository)
    {
        this._engravingTagRepository = engravingTagRepository;
    }
    public async Task SeedAsync()
    {
        int count = (await this._engravingTagRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        EngravingTag engravingTag1 = new EngravingTag(
            new Guid("40050eb8-9c1e-48a4-8070-4edefd1c08f3"), 
            new Translations( new Dictionary<string, string>
                {
                    { "en", "Scandinavian" },
                    { "ua", "Скандинавське" },
                }
            )
        );
         EngravingTag engravingTag2 = new EngravingTag(
            new Guid("9aef4a5e-013d-4bd1-94f7-dc1a93bbf0d1"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Military" },
                { "ua", "Військове" },
            })
        );

         EngravingTag engravingTag3 = new EngravingTag(
            new Guid("f1c1b3e6-44d6-45c2-880e-7d2e2c58a112"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Fantasy" },
                { "ua", "Фентезі" },
            })
        );

         EngravingTag engravingTag4 = new EngravingTag(
            new Guid("b205b7b0-e6fc-47ef-9d7d-5c9c7d0adbb3"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Celtic" },
                { "ua", "Кельтське" },
            })
        );

         EngravingTag engravingTag5 = new EngravingTag(
            new Guid("a2351832-dc14-4aa5-9117-47f0b5d7b5e9"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Geometric" },
                { "ua", "Геометричне" },
            })
        );

         EngravingTag engravingTag6 = new EngravingTag(
            new Guid("55f4cb74-c437-4ab1-9913-17b00e00f812"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Floral" },
                { "ua", "Рослинне" },
            })
        );

         EngravingTag engravingTag7 = new EngravingTag(
            new Guid("3db3b2cf-49e2-4634-a77e-3b12f88e19c5"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Tribal" },
                { "ua", "Трайбл" },
            })
        );

         EngravingTag engravingTag8 = new EngravingTag(
            new Guid("2f6485f2-683b-4b98-8eb2-529c218180b6"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Minimalist" },
                { "ua", "Мінімалістичне" },
            })
        );

         EngravingTag engravingTag9 = new EngravingTag(
            new Guid("f98f9d51-bae0-46d4-8cbf-057c8f7cb713"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Historical" },
                { "ua", "Історичне" },
            })
        );

        EngravingTag engravingTag10 = new EngravingTag(
            new Guid("c3469c3d-e2b2-4e57-9d68-4cd2e51e4c89"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Runes" },
                { "ua", "Руни" },
            })
        );
        
        await this._engravingTagRepository.Create(engravingTag1);
        await this._engravingTagRepository.Create(engravingTag2);
        await this._engravingTagRepository.Create(engravingTag3);
        await this._engravingTagRepository.Create(engravingTag4);
        await this._engravingTagRepository.Create(engravingTag5);
        await this._engravingTagRepository.Create(engravingTag6);
        await this._engravingTagRepository.Create(engravingTag7);
        await this._engravingTagRepository.Create(engravingTag8);
        await this._engravingTagRepository.Create(engravingTag9);
        await this._engravingTagRepository.Create(engravingTag10);
    }
    
}