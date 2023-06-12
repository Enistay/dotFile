using Microsoft.AspNetCore.WebUtilities;

namespace dotFile
{
    public interface IStreamFileUploadService
    {
        Task<string> UploadFile(MultipartReader reader, MultipartSection section);
    }
}
