using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var errorMessage = TempData["ErrorMessage"] as string;

            return View("ErrorPage", errorMessage);
        }
    }
}
