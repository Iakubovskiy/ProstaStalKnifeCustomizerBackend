using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using WorkshopBackend.Interfaces;

namespace WorkshopBackend.Services
{
    public class CloudinaryService : IFileService
    {
        private readonly IConfiguration _configuration;
        Cloudinary cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            string cloudinaryUrl = _configuration.GetSection("Cloudinary:CLOUDINARY_URL").Value;
            cloudinary = new Cloudinary(cloudinaryUrl);
        }

        public async Task<string> SaveFile(IFormFile file)
        {            
            Stream stream = file.OpenReadStream();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
            };
            ImageUploadResult result =  await cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }

        public async Task<bool> DeleteFile(string publicId)
        {
            DeletionParams deletionParams = new DeletionParams(publicId);
            await cloudinary.DestroyAsync(deletionParams);
            return true;
        }

        public string GetIdFromUrl(string url)
        {
            string[] elements = url.Split('/');
            string element = elements[elements.Length - 1];
            string publicId = element.Split('.')[0];
            return publicId;
        }
    }
}
