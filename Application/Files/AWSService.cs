using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Domain.Files;
using Infrastructure.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Files;

public class AwsService:IFileService
{
    private readonly AmazonS3Client _client;
    private readonly string _bucketName;
    private readonly string _domainName;
    private readonly IFileRepository _fileRepository;

    public AwsService(
        IConfiguration iConfiguration,
        IFileRepository fileRepository
    )
    {
        this._fileRepository = fileRepository;
        IConfiguration configuration = iConfiguration;
        string accessKey = configuration.GetValue<string>("S3_ACCESS_KEY") ?? 
                           configuration.GetSection("AWS:S3_ACCESS_KEY").Value ?? 
                           throw new ArgumentException("not access key provided");
        string secretKey = configuration.GetValue<string>("S3_SECRET_KET") ?? 
                           configuration.GetSection("AWS:S3_SECRET_KET").Value ??
                           throw new ArgumentException("not secret key provided");
        _bucketName = configuration.GetValue<string>("S3_BUCKET") ?? 
                      configuration.GetSection("AWS:S3_BUCKET").Value ?? 
                      throw new ArgumentException("not bucket name provided");
        _domainName = configuration.GetValue<string>("CDN_DOMAIN") ?? 
                      configuration.GetSection("AWS:CDN_DOMAIN").Value ??
                      throw new ArgumentException("not domain name provided");
        
        _client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.EUNorth1);
    }

    private string GenerateFileUrl(string fileName)
    {
        return $"https://{_domainName}/{fileName}";
    }

    public async Task<string> SaveFile(IFormFile file, string key)
    {
        string fileName = Uri.EscapeDataString(key);
        using var stream = file.OpenReadStream();

        TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            BucketName = _bucketName,
            Key = fileName,
            ContentType = file.ContentType,
        };
        TransferUtility transferUtility = new TransferUtility(_client);
        await transferUtility.UploadAsync(uploadRequest);
        return GenerateFileUrl(fileName);
    }

    public string GetIdFromUrl(string url)
    {
        string[] elements = url.Split('/');
        
        return elements[^1];
    }

    public async Task<bool> DeleteFile(Guid id)
    {
        try
        {
            FileEntity file = await this._fileRepository.GetById(id);
            string key = file.FileUrl.Split('/').Last();
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };
            await _client.DeleteObjectAsync(deleteRequest);
            await this._fileRepository.Delete(id);
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }

    public async Task RemoveUnusedFiles()
    {
        List<FileEntity> files = await this._fileRepository.GetAll();
        foreach (FileEntity file in files)
        {
            if (!await this._fileRepository.IsRecordReferencedAsync(file.Id))
            {
                await this.DeleteFile(file.Id);
            }
        }
    }
}