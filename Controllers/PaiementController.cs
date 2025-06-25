using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElearningBackend.Controllers
{
    public class PaiementController : Controller
    {
        // GET: PaiementController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaiementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaiementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaiementController/Create
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

        // GET: PaiementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaiementController/Edit/5
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

        // GET: PaiementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaiementController/Delete/5
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
