using GoceryStore_DACN.Models;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IUploadService
    {
        Task<UploadResult> UploadAsync(IFormFile file);
        Task<DeletionResult> DeleteAsync(string publicId);
    }
}
