using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.Gallery
{
    public class GalleryEdit : ViewComponent
    {
        private IHostingEnvironment _env;


        public GalleryEdit(IHostingEnvironment env)
        {
            _env = env;
        }

        public Task<IViewComponentResult> InvokeAsync(int Department = (int)_Enumeration._Department.Cayyolu)
        {
            ViewBag.DepartmentList = HttpInfo.DepartmentList;

            string departmentName = Department == 1 ? "cayyolu" : "polatli";
            string thumbnailDirectory = Path.Combine(_env.WebRootPath, "images\\gallery\\" + departmentName + "\\thumbnail");
            DirectoryInfo di = new DirectoryInfo(thumbnailDirectory);
            GalleryVM galleryVM = new GalleryVM();
            galleryVM.GalleryList = new List<GalleryItem>();
            galleryVM.Department = 1;
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
                    gallery.FilePath = Path.Combine(_env.WebRootPath, "images\\gallery\\" + departmentName);

                    galleryVM.GalleryList.Add(gallery);
                }

            }
            return Task.FromResult<IViewComponentResult>(View(galleryVM));
        }
    }
}
