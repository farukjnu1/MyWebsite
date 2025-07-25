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
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UserRepository userRepo = new UserRepository();
            return View(userRepo.GetAll());
        }

        public IActionResult Create()
        {
            var model = new UserVM();
            RoleRepository roleRepo = new RoleRepository();
            var listRole = roleRepo.GetAll();
            List<SelectListItem> selectRoles = new List<SelectListItem>();
            foreach (var role in listRole)
            {
                selectRoles.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleId.ToString() });
            }
            model.RoleOptions = selectRoles;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository();
                TempData["message"] = userRepo.Add(model);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id, UserVM.QueryType queryType)
        {
            var model = new UserVM();
            UserRepository userRepo = new UserRepository();
            model = userRepo.GetById(id);
            if (model != null)
            {
                RoleRepository roleRepo = new RoleRepository();
                var listRole = roleRepo.GetAll();
                List<SelectListItem> selectRoles = new List<SelectListItem>();
                foreach (var role in listRole)
                {
                    selectRoles.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleId.ToString() });
                }
                model.RoleOptions = selectRoles;
                model.QueryTypes = queryType;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository();
                TempData["message"] = userRepo.Update(model);
                if (model.RoleId > 0)
                {
                    RoleRepository roleRepo = new RoleRepository();
                    roleRepo.SetUserRole(model);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditEmail(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository();
                TempData["message"] = userRepo.UpdateEmail(model);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditUsername(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository();
                TempData["message"] = userRepo.UpdateUsername(model);
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditPassword(UserVM model)
        {
            try
            {
                model.CreateBy = HttpContext.Session.GetInt32("UserID");
                UserRepository userRepo = new UserRepository();
                TempData["message"] = userRepo.UpdatePassword(model);
            }
            catch (Exception ex)
            {
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
