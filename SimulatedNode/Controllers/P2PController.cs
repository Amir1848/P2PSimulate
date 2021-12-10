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
using Microsoft.Extensions.Logging;
using SimulatedNode.Models;
using SimulatedNode.Interfaces;

namespace SimulatedNode.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class P2PController : Controller
    {
        private readonly ILoggerManager _logger;
        public P2PController(ILoggerManager logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        [Route("GetNodeAddress")]
        public string GetNodeAddress(long nodeNumber)
        {
            _logger.LogInfo(string.Format("GetNodeAddress: {0}", nodeNumber));
            return SimulateAppService.GetNodeAddress(nodeNumber);
        }

        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            _logger.LogInfo(string.Format("GetFile: {0}", fileName));
            if (fileName == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           SimulateAppService.GetAppConfig().OwnedFilesDir, fileName);
            
            if (!System.IO.File.Exists(path))
            {
                var exp = new SimulateException("Own File Not Found" + path);
                _logger.LogError(exp.Message);
                throw exp;
            }

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