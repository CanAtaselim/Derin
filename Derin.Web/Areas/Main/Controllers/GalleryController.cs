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
    public class GalleryController : BaseController
    {
        private IMemoryCache _memoryCache;
        private AdministrationBLLocator _adminlocator;
        private IHostingEnvironment _env;
        public GalleryController(AdministrationBLLocator adminLocator, IMemoryCache memoryCache, IHostingEnvironment env) : base(env)
        {
            _env = env;
            _memoryCache = memoryCache;
            _adminlocator = adminLocator;
        }
        [ContactUsAttribute]
        public IActionResult Index()
        {
            ViewBag.ContactUs = JsonConvert.DeserializeObject<ContactUsVM>(HttpContext.Session.GetString("ContactUsData"));
            ViewBag.AboutUs = JsonConvert.DeserializeObject<AboutUsVM>(HttpContext.Session.GetString("AboutUsData"));

            GalleryVM galleryVM = new GalleryVM();
            galleryVM.GalleryList = new List<GalleryItem>();
            galleryVM.GalleryList.AddRange(GetImageList());

            return View(galleryVM);
        }

        private List<GalleryItem> GetImageList()
        {
            string thumbnailDirectory = Path.Combine(_env.WebRootPath, "images\\gallery\\derin\\thumbnail");
            DirectoryInfo di = new DirectoryInfo(thumbnailDirectory);
            List<GalleryItem> galleryList = new List<GalleryItem>();
            if (di.Exists)
            {
                List<string> ext = new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".tif" };

                FileInfo[] rgFiles = di.EnumerateFiles(".", SearchOption.AllDirectories)
                            .Where(path => ext.Contains(Path.GetExtension(path.Name)))
                            .Select(x => new FileInfo(x.FullName)).ToArray();
                GalleryItem gallery = null;
                foreach (FileInfo item in rgFiles.OrderByDescending(x => x.LastWriteTime))
                {
                    gallery = new GalleryItem();
                    gallery.FileName = item.Name;
                    gallery.FilePath = Path.Combine(_env.WebRootPath, "images\\gallery\\derin");

                    galleryList.Add(gallery);
                }

            }
            return galleryList;
        }
    }
}