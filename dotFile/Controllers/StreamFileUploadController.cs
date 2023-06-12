using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace dotFile.Controllers
{
    public class StreamFileUploadController : Controller
    {
        readonly IStreamFileUploadService _streamFileUploadService;

        public StreamFileUploadController(IStreamFileUploadService streamFileUploadService)
        {
            _streamFileUploadService = streamFileUploadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Index")]
        [HttpPost]
        public async Task<IActionResult> SaveFileToPhysicalFolder()
        {
            var boundary = HeaderUtilities.RemoveQuotes(
                MediaTypeHeaderValue.Parse(Request.ContentType).Boundary
            ).Value;

            var reader = new MultipartReader(boundary, Request.Body);

            var section = await reader.ReadNextSectionAsync();

            string response = string.Empty;
            try
            {

               string shaHash =  await _streamFileUploadService.UploadFile(reader, section);
                if (!string.IsNullOrEmpty(shaHash))
                {
                    ViewBag.Message = $"File Upload Successful : {shaHash}";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }
    }
}
