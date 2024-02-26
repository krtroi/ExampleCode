using System.Diagnostics;
using Ktsoft.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ktsoft.Controllers
{
    public class HomeController(ILogger<HomeController> l) : Controller
    {
        private readonly ILogger<HomeController> logger = l;

        public IActionResult Index() => View();
        
        public IActionResult Privacy() => View();
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        
    }
}
