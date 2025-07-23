using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebsite.EF;
using MyWebsite.Models;
using MyWebsite.Repositories;

namespace MyWebsite.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserVM model)
        {
            try
            {
                UserRepository userRepo = new UserRepository();
                UserVM oUser = userRepo.Login(model);
                if (oUser != null) 
                {
                    if (oUser.IsActive == true)
                    {
                        HttpContext.Session.SetInt32("UserID", oUser.UserID);
                        HttpContext.Session.SetString("Username", oUser.Username);
                        return RedirectToAction("Index", "Pages");
                    }
                    else
                    {
                        TempData["message"] = "User not valid.";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout(int? UserID)
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index");
        }

    }
}
