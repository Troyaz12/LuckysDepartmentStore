using Microsoft.AspNetCore.Mvc;
using System;

namespace LuckysDepartmentStore.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var errorMessage = TempData["ErrorMessage"] as string;

            if (errorMessage == null)
            {
                _logger.LogError("Unhandled exception occurred"); 
            }
            else
            {
                _logger.LogError(errorMessage);
            }                

            return View("ErrorPage", errorMessage);
        }
    }
}
