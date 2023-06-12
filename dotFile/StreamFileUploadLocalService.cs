using dotFile.Helper;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace dotFile
{
    public class StreamFileUploadLocalService : IStreamFileUploadService
    {
        public async Task<string> UploadFile(MultipartReader reader, MultipartSection? section)
        {
            string stringHash = string.Empty;
            string filePath = "";
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition, out var contentDisposition
                );

                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.DispositionType.Equals("form-data") &&
                    (!string.IsNullOrEmpty(contentDisposition.FileName.Value) ||
                    !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value)))
                    {
                        filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        byte[] fileArray;
                        using (var memoryStream = new MemoryStream())
                        {
                            await section.Body.CopyToAsync(memoryStream);
                            fileArray = memoryStream.ToArray();
                        }

                        stringHash = Sha256Helper.GetHashString(fileArray);

                        if (!System.IO.File.Exists(Path.Combine(filePath, stringHash)))
                        {
                            using (var fileStream = System.IO.File.Create(Path.Combine(filePath, stringHash)))
                            {
                                await fileStream.WriteAsync(fileArray);
                            }
                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }
            return stringHash;
        }
    }
}
