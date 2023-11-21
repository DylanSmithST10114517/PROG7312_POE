using Microsoft.AspNetCore.Mvc;
using DeweyDecimal.Models;
using System.Diagnostics;

namespace DeweyDecimal.Controllers
{
    // Controller responsible for handling requests related to the home page and error handling
    public class HomeController : Controller
    {
        // Logger instance for logging purposes
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method for handling requests to the home page
        public IActionResult Index()
        {
            return View();
        }

        // Action method for handling requests to the privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Action method for handling errors and displaying the error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create an ErrorViewModel with the current request ID for displaying error details
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
