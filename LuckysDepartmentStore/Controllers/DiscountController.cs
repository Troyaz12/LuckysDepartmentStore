using LuckysDepartmentStore.Models.ViewModels.Discount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class DiscountController : Controller
    {
        // GET: DiscountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DiscountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiscountController/Create
        public ActionResult Create(DiscountVM discount)
        {

            return View();
        }

        // POST: DiscountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiscountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DiscountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiscountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
