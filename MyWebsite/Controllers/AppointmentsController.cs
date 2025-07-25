using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.Fiters;
using MyWebsite.Repositories;

namespace MyWebsite.Controllers
{
    [AdminFilter]
    public class AppointmentsController : Controller
    {
        // GET: AppointmentsController
        /*public ActionResult Index()
        {
            return View();
        }*/

        public async Task<ActionResult> Index(int pageNumber = 1)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository();
            var listAppointment = await appointmentRepo.GetAll(pageNumber);
            return View(listAppointment);
        }

        // GET: AppointmentsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                int? CreateBy = HttpContext.Session.GetInt32("UserID");
                AppointmentRepository appointmentRepo = new AppointmentRepository();
                TempData["message"] = appointmentRepo.MarkAsReadOrUnread(id, CreateBy);
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }

        // GET: AppointmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentsController/Create
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

        // GET: AppointmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                int? CreateBy = HttpContext.Session.GetInt32("UserID");
                AppointmentRepository appointmentRepo = new AppointmentRepository();
                TempData["message"] = appointmentRepo.SuspendOrRestore(id, CreateBy);
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }

        // POST: AppointmentsController/Edit/5
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

        // GET: AppointmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Delete/5
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
