using System.Diagnostics;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _apiClient;

        public HomeController(ILogger<HomeController> logger, ApiClient client)
        {
            _logger = logger;
            _apiClient = client;
        }

        public IActionResult Index()
        {
            HomeModel homeModel = new HomeModel();

            homeModel.LastBooks = _apiClient.GetLastTenBooks().Result;
            homeModel.FeaturedBooks = _apiClient.GetFeaturedBooks().Result;

            return View(homeModel);
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