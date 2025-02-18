using cis414_project.Database;
using cis414_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cis414_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBGateway _dbGateway;
        const int _userId = 1;

        public HomeController(ILogger<HomeController> logger, DBGateway dbGateway)
        {
            _logger = logger;
            _dbGateway = dbGateway;
        }

        public async Task<IActionResult> Index()
        {
            var playlists = await _dbGateway.GetPlaylists(_userId);
            ViewBag.Playlists = playlists;

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
