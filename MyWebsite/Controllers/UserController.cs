using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyWebsite.Models;
using MyWebsite.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyWebsite.Controllers
{
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
                UserRepository userRepo = new UserRepository();
                userRepo.Add(model);
            }
            catch (Exception ex)
            {
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
