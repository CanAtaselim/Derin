using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Derin.Web.Areas.Main.Controllers
{
    [Area("Main")]
    public class ProjectController : BaseController
    {
        private AdministrationBLLocator _adminlocator;
        private IHostingEnvironment _env;
        public ProjectController(AdministrationBLLocator adminLocator,  IHostingEnvironment env) : base(env)
        {
            _env = env;
            _adminlocator = adminLocator;
        }
        [ContactUsAttribute]
        public IActionResult Index()
        {
            ViewBag.ContactUs = JsonConvert.DeserializeObject<ContactUsVM>(HttpContext.Session.GetString("ContactUsData"));
            ViewBag.AboutUs = JsonConvert.DeserializeObject<AboutUsVM>(HttpContext.Session.GetString("AboutUsData"));
            ViewBag.Project = _adminlocator.ProjectBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active).ToList();
            return View();
        }
    }
}