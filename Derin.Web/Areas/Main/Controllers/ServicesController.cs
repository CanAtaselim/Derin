using System;
using System.Collections.Generic;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Derin.Web.Areas.Main.Controllers
{
    [Area("Main")]
    public class ServicesController : BaseController
    {
        private IMemoryCache _memoryCache;
        private AdministrationBLLocator _adminlocator;
        private IHostingEnvironment _env;
        public ServicesController(AdministrationBLLocator adminLocator, IMemoryCache memoryCache, IHostingEnvironment env) : base(env)
        {
            _env = env;
            _memoryCache = memoryCache;
            _adminlocator = adminLocator;
        }
        [ContactUsAttribute]
        public IActionResult Index()
        {
            ViewBag.ContactUs = JsonConvert.DeserializeObject<List<ContactUsVM>>(HttpContext.Session.GetString("ContactUsData"));
            ViewBag.Services = _adminlocator.ServicesBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active);

            return View();
        }
    }
}