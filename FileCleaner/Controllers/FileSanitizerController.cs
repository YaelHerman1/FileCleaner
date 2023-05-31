using FileCleaner.Implementation;
using FileCleaner.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace FileCleaner.Controllers
{
    [Route("filesanitizer")]
    public class FileSanitizerController : ControllerBase
    {
        private readonly ILogger<FileSanitizerController> _logger;
        private readonly IFileFormatHandler<ABCFormatHandler> _abcFormatHandler;
        private readonly IFileFormatHandler<EFGFormatHandler> _efgFormatHandler;
        public FileSanitizerController(ILogger<FileSanitizerController> logger, IFileFormatHandler<ABCFormatHandler> abcFormatHandler, IFileFormatHandler<EFGFormatHandler> efgFormatHandler)
        {
            _logger = logger;
            _abcFormatHandler = abcFormatHandler;
            _efgFormatHandler = efgFormatHandler;
        }

        /// <summary>
        /// Sanitize the provided file.
        /// </summary>
        /// <param name="file">The file to sanitize.</param>
        /// <returns>The sanitized file as a downloadable response.</returns>
        [HttpPost("sanitize")]
        public ActionResult Sanitize(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file provided");
                }

                byte[] fileBytes;

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                if (_abcFormatHandler.IsFormatValid(fileBytes))
                {
                    var sanitizedBytes = _abcFormatHandler.Sanitize(file);
                    return File(sanitizedBytes, "application/octet-stream", file.FileName);
                }
                else if (_efgFormatHandler.IsFormatValid(fileBytes))
                {
                    var sanitizedBytes = _efgFormatHandler.Sanitize(file);
                    return File(sanitizedBytes, "application/octet-stream", file.FileName);
                }

                return BadRequest("Invalid file format");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sanitize failed");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
