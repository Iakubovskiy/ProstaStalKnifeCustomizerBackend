using Domain.Component.Product.Attachments;
using Domain.Translation;

namespace Infrastructure.Data.Component.Products.Attachments;

public class AttachmentTypeSeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<AttachmentType> _attachmentTypeRepository;

    public AttachmentTypeSeeder(
        IRepository<AttachmentType> attachmentTypeRepository
    )
    {
        this._attachmentTypeRepository = attachmentTypeRepository;
    }

    public async Task SeedAsync()
    {
        if ((await _attachmentTypeRepository.GetAll()).Any())
        {
            return;
        }
        
        var type1 = new AttachmentType(
            new Guid("1a1b1c1d-1e1f-4a2b-8c3d-4e5f6a7b8c9d"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Belt Clip" },
                { "ua", "Кліпса на пояс" },
            })
        );

        var type2 = new AttachmentType(
            new Guid("2b2c2d2e-2f3a-4b3c-9d4e-5f6a7b8c9d0e"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Lanyard" },
                { "ua", "Темляк" },
            })
        );

        var type3 = new AttachmentType(
            new Guid("3c3d3e3f-3a4b-4c4d-ae5f-6a7b8c9d0e1f"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Ferro Rod Holder" },
                { "ua", "Тримач для кресала" },
            })
        );

        var type4 = new AttachmentType(
            new Guid("4d4e4f4a-4b5c-4d5e-bf6a-7b8c9d0e1f2a"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "MOLLE Lock" },
                { "ua", "Кріплення MOLLE" },
            })
        );
        
        var type5 = new AttachmentType(
            new Guid("5e5f5a5b-5c6d-4e6f-c07b-8c9d0e1f2a3b"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Lanyard Bead" },
                { "ua", "Намистина для темляка" },
            })
        );

        var type6 = new AttachmentType(
            new Guid("6f6a6b6c-6d7e-4f7a-d18c-9d0e1f2a3b4c"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Paracord" },
                { "ua", "Паракорд" },
            })
        );

        var type7 = new AttachmentType(
            new Guid("7a7b7c7d-7e8f-4a8b-e29d-0e1f2a3b4c5d"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Sharpening Stone" },
                { "ua", "Точильний камінь" },
            })
        );

        var type8 = new AttachmentType(
            new Guid("8b8c8d8e-8f9a-4b9c-f3ae-1f2a3b4c5d6e"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Fastening Screws" },
                { "ua", "Кріпильні гвинти" },
            })
        );
        
        var type9 = new AttachmentType(
            new Guid("9c9d9e9f-9a0b-4cac-04bf-2a3b4c5d6e7f"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Belt Loop" },
                { "ua", "Петля на пояс" },
            })
        );

        var type10 = new AttachmentType(
            new Guid("0d0e0f0a-0b1c-4dcd-15c0-3b4c5d6e7f8a"),
            new Translations(new Dictionary<string, string>
            {
                { "en", "Glass Breaker" },
                { "ua", "Склобій" },
            })
        );

        await _attachmentTypeRepository.Create(type1);
        await _attachmentTypeRepository.Create(type2);
        await _attachmentTypeRepository.Create(type3);
        await _attachmentTypeRepository.Create(type4);
        await _attachmentTypeRepository.Create(type5);
        await _attachmentTypeRepository.Create(type6);
        await _attachmentTypeRepository.Create(type7);
        await _attachmentTypeRepository.Create(type8);
        await _attachmentTypeRepository.Create(type9);
        await _attachmentTypeRepository.Create(type10);
    }
}