using Domain.Files;

namespace Infrastructure.Files;

public interface IFileRepository : IRepository<FileEntity>
{
    public Task<bool> IsRecordReferencedAsync(Guid id);
}