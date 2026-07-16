using Microsoft.AspNetCore.Http;

namespace Base.Application.Contracts
{
    public interface IMinIoService
    {
        Task<bool> UploadFile(IFormFile file, string folderPath);
        Task<string> GetDownloadUrl(string fileName, string folderPath);
    }
}
