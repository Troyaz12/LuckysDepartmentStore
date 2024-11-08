using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckysDepartmentStore.Controllers
{
    public class AdministrativeController : Controller
    {
        // GET: AdministrativeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdministrativeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdministrativeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministrativeController/Create
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

        // GET: AdministrativeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdministrativeController/Edit/5
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

        // GET: AdministrativeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdministrativeController/Delete/5
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
