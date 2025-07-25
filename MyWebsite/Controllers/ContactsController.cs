using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Fiters;
using MyWebsite.Repositories;

namespace MyWebsite.Controllers
{
    [AdminFilter]
    public class ContactsController : Controller
    {
        // GET: ContactsController
        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            ContactMessageRepository  contactRepo = new ContactMessageRepository();
            var listContact = await contactRepo.GetAll(pageNumber);
            return View(listContact);
        }

        // GET: ContactsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                int? CreateBy = HttpContext.Session.GetInt32("UserID");
                ContactMessageRepository contactRepo = new ContactMessageRepository();
                TempData["message"] = contactRepo.MarkAsReadOrUnread(id, CreateBy);
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }

        // GET: ContactsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
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

        // GET: ContactsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                int? CreateBy = HttpContext.Session.GetInt32("UserID");
                ContactMessageRepository contactRepo = new ContactMessageRepository();
                TempData["message"] = contactRepo.SuspendOrRestore(id, CreateBy);
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }

        // POST: ContactsController/Edit/5
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

        // GET: ContactsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactsController/Delete/5
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
