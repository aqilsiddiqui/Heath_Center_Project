using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WPFMedinova.Models;

namespace WPFMedinova.Controllers
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
        public IActionResult Wpf_Medinova()
        {
            return View();
        }
        public IActionResult Features()
        {
            return View();
        }
        public IActionResult About_Us()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Service_Medical_Couselling()
        {
            return View();
        }
        public IActionResult Service_Medical_Reasearch()
        {
            return View();
        }
        public IActionResult Service_Blood_Bank()
        {
            return View();
        }
        public IActionResult Page_404()
        {
            return View();
        }
        public IActionResult News_Blog_Archive()
        {
            return View();
        }
        public IActionResult News_Blog_Archive_with_Left_Sidebar()
        {
            return View();
        }
        public IActionResult News_Blog_Archive_with_Right_Sidebar()
        {
            return View();
        }
        public IActionResult News_Blog_Single()
        {
            return View();
        }
        public IActionResult News_Blog_Single_with_Left_Sidebar()
        {
            return View();
        }
        public IActionResult News_Blog_Single_with_Right_Sidebar()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}