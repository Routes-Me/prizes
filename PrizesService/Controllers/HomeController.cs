using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrizesService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnv;
        public HomeController(IWebHostEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }
        [HttpGet]
        public string Get()
        {
            return "Prizes service started successfully. Environment - " + _hostingEnv.EnvironmentName + "";
        }
    }
}
