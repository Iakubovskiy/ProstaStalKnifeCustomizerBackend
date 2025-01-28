using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Util;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using WorkshopBackend.Interfaces;

namespace WorkshopBackend.Services;

public class AWSService:IFileService
{
    private readonly IConfiguration _configuration;
    private AmazonS3Client _client;
    private readonly string bucketName;

    public AWSService(IConfiguration configuration)
    {
        _configuration = configuration;
        string accessKey = _configuration.GetValue<string>("S3_ACCESS_KEY");
        string secretKey = _configuration.GetValue<string>("S3_SECRET_KET");
        bucketName = _configuration.GetValue<string>("S3_BUCKET");
        string region = "eu-north-1";

        if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(bucketName))
        {
            accessKey = _configuration.GetSection("AWS:S3_ACCESS_KEY").Value;
            secretKey = _configuration.GetSection("AWS:S3_SECRET_KET").Value;
            bucketName = _configuration.GetSection("AWS:S3_BUCKET").Value;
        }
        
        _client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.EUNorth1);
    }

    private string GenerateFileUrl(string FileName)
    {
        return $"https://{bucketName}.s3.amazonaws.com/{FileName}";
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        string fileName = file.FileName;
        using var stream = file.OpenReadStream();

        TransferUtilityUploadRequest UploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            BucketName = bucketName,
            Key = fileName,
            ContentType = file.ContentType,
        };
        TransferUtility TransferUtility = new TransferUtility(_client);
        await TransferUtility.UploadAsync(UploadRequest);
        return GenerateFileUrl(file.FileName);
    }

    public string GetIdFromUrl(string url)
    {
        string[] elements = url.Split('/');
        
        return elements[elements.Length - 1];
    }

    public async Task<bool> DeleteFile(string key)
    {
        try
        {
            if (string.IsNullOrEmpty(key))
                return false;
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };
            await _client.DeleteObjectAsync(deleteRequest);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }
}