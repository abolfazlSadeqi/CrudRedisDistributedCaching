using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisDistributedCaching.Helper;
using RedisDistributedCaching.Model;
using RedisDistributedCaching.Models;
using System.Diagnostics;
using System.Threading;

namespace RedisDistributedCaching.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
  


        public HomeController( ILogger<HomeController> logger)
        {
            _logger = logger;
          
        }

        public async Task<IActionResult> IndexAsync()
        {

         


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}