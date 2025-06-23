namespace Domain.Files;

public class FileEntity : IEntity, IUpdatable<FileEntity>
{
    private FileEntity()
    {
    }
    public FileEntity(
        Guid id,
        string fileUrl
    )
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
        {
            throw new ArgumentException("File url cannot be empty");
        }
        
        this.Id = id;
        this.FileUrl = fileUrl;
    }
    public Guid Id { get; set; }
    public string FileUrl { get; set; }
    public void Update(FileEntity fileEntity)
    {
        if (fileEntity.FileUrl == null)
        {
            throw new ArgumentException("File url cannot be empty");
        }
        this.FileUrl = fileEntity.FileUrl;
    }
}