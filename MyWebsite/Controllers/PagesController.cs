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
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;
        public PagesController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("WebsiteContext");
            _environment = environment;
        }

        // GET: PagesController
        public ActionResult Index()
        {
            var listPage = new List<PageVM>();
            try
            {
                PageRepository pageRepo = new PageRepository(_connectionString);
                listPage = pageRepo.GetAll();
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
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

        //[Route("Pages/Details/{slug}")]
        public ActionResult Details(string slug)
        {
            var listPage = new List<PageVM>();
            try
            {
                PageRepository pRepo = new PageRepository(_connectionString);
                PageContentRepository pcRepo = new PageContentRepository(_connectionString);

                var oPage = pRepo.GetBySlug(slug);
                oPage.ListPageContent = pcRepo.GetBySlugPage(slug);

                listPage = new List<PageVM>();
                listPage.Add(oPage);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return View(listPage);
        }

    }
}
