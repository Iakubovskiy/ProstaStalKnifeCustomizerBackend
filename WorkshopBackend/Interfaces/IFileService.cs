namespace WorkshopBackend.Interfaces
{
    public interface IFileService
    {
        public Task<string> SaveFile(IFormFile file, string key);
        public Task<bool> DeleteFile(string id);
        public string GetIdFromUrl(string url);
    }
}
