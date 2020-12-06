using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Derin.Web.Areas.Main.Controllers
{
    [Area("Main")]
    public class HomeController : BaseController
    {
        private IMemoryCache _memoryCache;
        private AdministrationBLLocator _adminlocator;
        private IHostingEnvironment _env;
        public HomeController(AdministrationBLLocator adminLocator, IMemoryCache memoryCache, IHostingEnvironment env) : base(env)
        {
            _env = env;
            _memoryCache = memoryCache;
            _adminlocator = adminLocator;
        }

        [ContactUsAttribute]
        public IActionResult Index()
        {

            ViewBag.Services = _adminlocator.ServicesBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active).ToList();
            ViewBag.ContactUs = JsonConvert.DeserializeObject<ContactUsVM>(HttpContext.Session.GetString("ContactUsData"));
            ViewBag.AboutUs = JsonConvert.DeserializeObject<AboutUsVM>(HttpContext.Session.GetString("AboutUsData"));
            ViewBag.Project = _adminlocator.ProjectBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active).ToList();

            return View();
        }
        public FileContentResult GetImageFilePath(long idPerson)
        {
            try
            {
                if (idPerson == 0)
                {
                    var uploads = Path.Combine(_env.WebRootPath, "frontend/images");
                    byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(uploads, "noPersonImage.png"));
                    return new FileContentResult(fileBytes, "image/jpeg");
                }
                else
                {
                    var person = _adminlocator.PersonBL.CRUD.GetById(idPerson);
                    return new FileContentResult(person.Picture, "image/jpeg");
                }
            }
            catch (Exception)
            {
                var uploads = Path.Combine(_env.WebRootPath, "frontend/images");
                byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(uploads, "noPersonImage.png"));
                return new FileContentResult(fileBytes, "image/jpeg");
            }

        }

    }
}