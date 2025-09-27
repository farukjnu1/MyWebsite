using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyWebsite.Models;
using MyWebsite.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MyWebsite.Fiters;

namespace MyWebsite.Controllers
{
    [AdminFilter]
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;
        public UserController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("WebsiteContext");
            _environment = environment;
        }

        public IActionResult Index()
        {
            var listUser = new List<UserVM>();
            try 
            {
                UserRepository userRepo = new UserRepository(_connectionString);
                listUser = userRepo.GetAll();
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return View(listUser);
        }

        public IActionResult Create()
        {
            var model = new UserVM();
            try 
            {
                RoleRepository roleRepo = new RoleRepository(_connectionString);
                var listRole = roleRepo.GetAll();
                List<SelectListItem> selectRoles = new List<SelectListItem>();
                foreach (var role in listRole)
                {
                    selectRoles.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleId.ToString() });
                }
                model.RoleOptions = selectRoles;
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository(_connectionString);
                TempData["message"] = userRepo.Add(model);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id, UserVM.QueryType queryType)
        {
            var model = new UserVM();
            try
            {
                UserRepository userRepo = new UserRepository(_connectionString);
                model = userRepo.GetById(id);
                if (model != null)
                {
                    RoleRepository roleRepo = new RoleRepository(_connectionString);
                    var listRole = roleRepo.GetAll();
                    List<SelectListItem> selectRoles = new List<SelectListItem>();
                    foreach (var role in listRole)
                    {
                        selectRoles.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleId.ToString() });
                    }
                    model.RoleOptions = selectRoles;
                    model.QueryTypes = queryType;
                }
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository(_connectionString);
                TempData["message"] = userRepo.Update(model);
                if (model.RoleId > 0)
                {
                    RoleRepository roleRepo = new RoleRepository(_connectionString);
                    roleRepo.SetUserRole(model);
                }
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditEmail(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository(_connectionString);
                TempData["message"] = userRepo.UpdateEmail(model);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditUsername(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository(_connectionString);
                TempData["message"] = userRepo.UpdateUsername(model);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditPassword(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository(_connectionString);
                TempData["message"] = userRepo.UpdatePassword(model);
            }
            catch (Exception ex)
            {
                ErrorVM error = new ErrorVM(_environment);
                error.WriteLog(ex.StackTrace);
                TempData["message"] = "Exception!";
            }
            return RedirectToAction("Index");
        }

        /*public IActionResult Privacy()
        {
            return View();
        }*/

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/

    }
}
