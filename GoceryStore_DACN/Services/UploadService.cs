using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GoceryStore_DACN.Models;
using GoceryStore_DACN.Services.Interface;
using Microsoft.Extensions.Options;
using DeletionResult = GoceryStore_DACN.Models.DeletionResult;
using UploadResult = GoceryStore_DACN.Models.UploadResult;

namespace GoceryStore_DACN.Services
{
    public class UploadService : IUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;
        public UploadService(Cloudinary cloudinary, IOptions<CloudinarySettings> cloudinarySettings)
        {
            _cloudinary = cloudinary;
            var acc = new Account(
            cloudinarySettings.Value.CloudName,
            cloudinarySettings.Value.ApiKey,
            cloudinarySettings.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<UploadResult> UploadAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return new UploadResult
                    {
                        Success = false,
                        Error = "File is empty"
                    };

                }
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Quality("auto").FetchFormat("auto")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadResult
                    {
                        PublicId = uploadResult.PublicId,
                        Url = uploadResult.Url.ToString(),
                        SecureUrl = uploadResult.SecureUrl.ToString(),
                        Format = uploadResult.Format,
                        Size = uploadResult.Length,
                        Success = true
                    };
                }
                return new UploadResult
                {
                    Success = false,
                    Error = uploadResult.Error.Message
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<DeletionResult> DeleteAsync(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
