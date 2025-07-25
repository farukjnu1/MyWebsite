using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Controllers
{
    public class PageContentsController : Controller
    {
        // GET: PageContentsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PageContentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PageContentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PageContentsController/Create
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

        // GET: PageContentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PageContentsController/Edit/5
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

        // GET: PageContentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PageContentsController/Delete/5
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
