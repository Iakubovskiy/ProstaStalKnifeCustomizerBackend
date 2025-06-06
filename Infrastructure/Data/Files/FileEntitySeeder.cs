using Domain.Files;

namespace Infrastructure.Data.Files;

public class FileEntitySeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<FileEntity> _fileEntityRepository;
    
    public FileEntitySeeder(IRepository<FileEntity> fileEntityRepository)
    {
        this._fileEntityRepository = fileEntityRepository;
    }
    
    public async Task SeedAsync()
    {
        int count = (await this._fileEntityRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        
        FileEntity fileEntity1 = new FileEntity(
            new Guid("e1a2b3c4-d5e6-4f7a-8b9c-0d1e2f3a4b5c"),
            "https://example.com/files/sample1.jpg"
        );
        
        FileEntity fileEntity2 = new FileEntity(
            new Guid("f2b3c4d5-e6f7-4a8b-9c0d-1e2f3a4b5c6d"),
            "https://example.com/files/sample2.pdf"
        );
        
        FileEntity fileEntity3 = new FileEntity(
            new Guid("a3b4c5d6-e7f8-4a9b-0c1d-2e3f4a5b6c7d"),
            "https://example.com/files/sample3.png"
        );
        
        FileEntity fileEntity4 = new FileEntity(
            new Guid("b4c5d6e7-f8a9-4b0c-1d2e-3f4a5b6c7d8e"),
            "https://example.com/files/sample4.docx"
        );
        
        FileEntity fileEntity5 = new FileEntity(
            new Guid("c5d6e7f8-a9b0-4c1d-2e3f-4a5b6c7d8e9f"),
            "https://example.com/files/sample5.svg"
        );
        
        await this._fileEntityRepository.Create(fileEntity1);
        await this._fileEntityRepository.Create(fileEntity2);
        await this._fileEntityRepository.Create(fileEntity3);
        await this._fileEntityRepository.Create(fileEntity4);
        await this._fileEntityRepository.Create(fileEntity5);
    }
}