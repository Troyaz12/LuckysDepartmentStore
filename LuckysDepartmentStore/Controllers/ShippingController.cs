using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class ShippingController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
      
    }
}
