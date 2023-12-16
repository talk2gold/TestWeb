using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestWeb_youtube.Models;

namespace TestWeb_youtube.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult YoutubeSearch()
        {
            mytestApi myap = new mytestApi();
            returnObject retobj= myap.YouTubeApi();
            return View(retobj);
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