using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebsite.Fiters;
using MyWebsite.Models;
using MyWebsite.Repositories;

namespace MyWebsite.Controllers
{
    [AdminFilter]
    public class PageContentsController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public PageContentsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

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
            PageContentRepository pcRepo = new PageContentRepository();
            var oPageContent = pcRepo.GetById(id);
            return View(oPageContent);
        }

        // POST: PageContentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PageContentVM model) //IFormCollection collection)
        {
            try
            {
                #region Media
                if (model.MediaFile != null && model.MediaFile.Length > 0)
                {
                    #region Create File
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string extension = Path.GetExtension(model.MediaFile.FileName);
                    //string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // to milliseconds
                    DateTime currentDateTime = DateTime.Now;
                    string timeStamp = currentDateTime.ToString("yyyyMMdd") + "_" + currentDateTime.ToString("HHmmss") + "_" + currentDateTime.ToString("fff");
                    string uniqueFileName = $"{timeStamp}{extension}";

                    var filePath = Path.Combine(uploadsFolder, Path.GetFileName(model.MediaFile.FileName));

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.MediaFile.CopyTo(stream);
                    }
                    #endregion
                    #region Delete File
                    if (!string.IsNullOrEmpty(model.FilePath))
                    {
                        string uploadPath = Path.Combine(_environment.WebRootPath, "img");
                        string delFilePath = Path.Combine(uploadPath, model.FilePath);

                        if (System.IO.File.Exists(delFilePath))
                        {
                            System.IO.File.Delete(delFilePath);
                        }
                    }
                    #endregion
                }
                #endregion

                int? UploadedBy = HttpContext.Session.GetInt32("UserID");
                PageContentRepository pcRepo = new PageContentRepository();
                model.UploadedBy = UploadedBy;
                TempData["message"] = pcRepo.Update(model);

                return RedirectToAction("Details", "Pages", new { slug = model.SlugPage });
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
