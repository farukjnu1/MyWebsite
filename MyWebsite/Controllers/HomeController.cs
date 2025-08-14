using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.EF;
using MyWebsite.Fiters;
using MyWebsite.Models;
using MyWebsite.Repositories;

namespace MyWebsite.Controllers
{
    [WebsiteFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _environment = environment;
        }

        public IActionResult Index()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var homePage = pRepo.GetBySlug("home");
            homePage.ListPageContent = pcRepo.GetBySlugPage("home");

            var servicesPage = pRepo.GetBySlug("services");
            servicesPage.ListPageContent = pcRepo.GetBySlugPage("services");

            var aboutPage = pRepo.GetBySlug("about");
            aboutPage.ListPageContent = pcRepo.GetBySlugPage("about");

            //var appointmentPage = pRepo.GetBySlug("appointment");
            //appointmentPage.ListPageContent = pcRepo.GetBySlugPage("appointment");

            var ourTeamPage = pRepo.GetBySlug("our_team");
            ourTeamPage.ListPageContent = pcRepo.GetBySlugPage("our_team");

            var testimonialPage = pRepo.GetBySlug("testimonial");
            testimonialPage.ListPageContent = pcRepo.GetBySlugPage("testimonial");

            var ourBlogPage = pRepo.GetBySlug("our_blog");
            ourBlogPage.ListPageContent = pcRepo.GetBySlugPage("our_blog");

            var listPage = new List<PageVM>();
            listPage.Add(homePage);
            listPage.Add(servicesPage);
            listPage.Add(aboutPage);
            //listPage.Add(appointmentPage);
            listPage.Add(ourTeamPage);
            listPage.Add(testimonialPage);
            listPage.Add(ourBlogPage);

            return View(listPage);
        }

        public IActionResult About()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var aboutPage = pRepo.GetBySlug("about");
            aboutPage.ListPageContent = pcRepo.GetBySlugPage("about");

            var listPage = new List<PageVM>();
            listPage.Add(aboutPage);

            return View(listPage);
        }

        public IActionResult Services()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var servicePage = pRepo.GetBySlug("services");
            servicePage.ListPageContent = pcRepo.GetBySlugPage("services");

            var listPage = new List<PageVM>();
            listPage.Add(servicePage);

            return View(listPage);
        }

        public IActionResult Service(int id)
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var servicePage = pRepo.GetBySlug("services");
            var oService = pcRepo.GetById(id);
            servicePage.ListPageContent.Add(oService);

            var listPage = new List<PageVM>();
            listPage.Add(servicePage);

            return View(listPage);
        }

        public IActionResult OurTeams()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var ourTeamPage = pRepo.GetBySlug("our_team");
            ourTeamPage.ListPageContent = pcRepo.GetBySlugPage("our_team");

            var listPage = new List<PageVM>();
            listPage.Add(ourTeamPage);

            return View(listPage);
        }

        public IActionResult Contact()
        {
            #region Read
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var layoutPage = pRepo.GetBySlug("layout");
            layoutPage.ListPageContent = pcRepo.GetBySlugPage("layout");

            var contactPage = pRepo.GetBySlug("contact");
            contactPage.ListPageContent = pcRepo.GetBySlugPage("contact");

            var listPage = new List<PageVM>();
            listPage.Add(contactPage);
            listPage.Add(layoutPage);
            ViewData["contact"] = listPage;
            #endregion

            #region Create
            ContactMessageVM model = new ContactMessageVM();
            #endregion

            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(ContactMessageVM model)
        {
            try
            {
                ContactMessageRepository contactRepo = new ContactMessageRepository();
                TempData["message"] = contactRepo.Add(model);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Appointments");
        }

        public IActionResult OurBlogs()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var ourBlogPage = pRepo.GetBySlug("our_blog");
            ourBlogPage.ListPageContent = pcRepo.GetBySlugPage("our_blog");

            var listPage = new List<PageVM>();
            listPage.Add(ourBlogPage);

            return View(listPage);
        }

        public IActionResult OurBlog(int id)
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var ourBlogPage = pRepo.GetBySlug("our_blog");
            ourBlogPage.ListPageContent.Add(pcRepo.GetById(id));

            var listPage = new List<PageVM>();
            listPage.Add(ourBlogPage);

            return View(listPage);
        }

        public IActionResult Appointments()
        {
            #region Read
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var appointmentPage = pRepo.GetBySlug("appointment");
            appointmentPage.ListPageContent = pcRepo.GetBySlugPage("appointment");

            var listPage = new List<PageVM>();
            listPage.Add(appointmentPage);
            ViewData["appointment"] = listPage;
            #endregion

            #region Create
            AppointmentVM model = new AppointmentVM();
            SpecialistRepository specialRepo = new SpecialistRepository();
            var listSpecialist = specialRepo.GetAll();
            List<SelectListItem> selectSpecialists = new List<SelectListItem>();
            foreach (var item in listSpecialist)
            {
                selectSpecialists.Add(new SelectListItem { Text = item.FullName, Value = item.SpecialistId.ToString() });
            }
            model.SpecialistOptions = selectSpecialists;

            DepartmentRepository deptRepo = new DepartmentRepository();
            var listDept = deptRepo.GetAll();
            List<SelectListItem> selectDepts = new List<SelectListItem>();
            foreach (var item in listDept)
            {
                selectDepts.Add(new SelectListItem { Text = item.Name, Value = item.DepartmentId.ToString() });
            }
            model.DepartmentOptions = selectDepts;
            #endregion
            return View(model);
        }

        [HttpPost]
        public IActionResult Appointment(AppointmentVM model)
        {
            try
            {
                AppointmentRepository appointRepo = new AppointmentRepository();
                TempData["message"] = appointRepo.Add(model);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Appointments");
        }

        public IActionResult Testimonials()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var testimonialPage = pRepo.GetBySlug("testimonial");
            testimonialPage.ListPageContent = pcRepo.GetBySlugPage("testimonial");

            var listPage = new List<PageVM>();
            listPage.Add(testimonialPage);

            return View(listPage);
        }

        public IActionResult Privacy()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var privacyPolicyPage = pRepo.GetBySlug("privacy_policy");
            privacyPolicyPage.ListPageContent = pcRepo.GetBySlugPage("privacy_policy");

            var listPage = new List<PageVM>();
            listPage.Add(privacyPolicyPage);

            return View(listPage);
        }

        public IActionResult Terms()
        {
            PageRepository pRepo = new PageRepository(_connectionString);
            PageContentRepository pcRepo = new PageContentRepository(_connectionString);

            var termsConditionPage = pRepo.GetBySlug("terms_condition");
            termsConditionPage.ListPageContent = pcRepo.GetBySlugPage("terms_condition");

            var listPage = new List<PageVM>();
            listPage.Add(termsConditionPage);

            return View(listPage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
