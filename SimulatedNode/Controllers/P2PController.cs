using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimulatedNode.Services;

namespace SimulatedNode.Controllers
{
    public class P2PController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetNodeAddress(long nodeNumber)
        {
            return SimulateAppService.GetNodeAddress(nodeNumber);
        }

        [HttpGet]
        public IActionResult GetFile()
        {
            throw new NotImplementedException("Mehrnoosh");
        }
    }
}