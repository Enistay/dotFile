using Microsoft.AspNetCore.WebUtilities;

namespace dotFile
{
    public interface IStreamFileUploadService
    {
        Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
    }
}
