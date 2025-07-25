using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Fiters;
using MyWebsite.Models;
using MyWebsite.Repositories;

namespace MyWebsite.Controllers
{
    [AdminFilter]
    public class PagesController : Controller
    {
        // GET: PagesController
        public ActionResult Index()
        {
            PageRepository pageRepo = new PageRepository();
            var listPage = pageRepo.GetAll();
            return View(listPage);
        }

        // GET: PagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PagesController/Edit/5
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

        public ActionResult Details(string slug)
        {
            PageRepository pRepo = new PageRepository();
            PageContentRepository pcRepo = new PageContentRepository();

            var oPage = pRepo.GetBySlug(slug);
            oPage.ListPageContent = pcRepo.GetBySlugPage(slug);

            var listPage = new List<PageVM>();
            listPage.Add(oPage);

            return View(listPage);
        }

        // GET: PagesController/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: PagesController/Create
        /*public ActionResult Create()
        {
            return View();
        }*/

        // POST: PagesController/Create
        /*[HttpPost]
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
        }*/

        // GET: PagesController/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }*/

        // POST: PagesController/Edit/5
        /*[HttpPost]
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
        }*/

        // GET: PagesController/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }*/

        // POST: PagesController/Delete/5
        /*[HttpPost]
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
        }*/

    }
}
