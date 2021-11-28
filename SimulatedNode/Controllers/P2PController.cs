using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimulatedNode.Services;
using System.Security.Cryptography.Pkcs;
using System.Net.Mime;
using Microsoft.AspNetCore.StaticFiles;

namespace SimulatedNode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class P2PController : Controller
    {
        [HttpGet]
        [Route("GetNodeAddress")]
        public string GetNodeAddress(long nodeNumber)
        {
            return SimulateAppService.GetNodeAddress(nodeNumber);
        }

        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            if (fileName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return File(memory, contentType, Path.GetFileName(path));
        }
    }
}