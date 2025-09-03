using Microsoft.AspNetCore.Http;

namespace Application.Files.FileConversion;

public interface IFileConversionService
{
    public Task<byte[]> ConvertFile(IFormFile fileFrom, string fromFormat, string targetFormat);
}