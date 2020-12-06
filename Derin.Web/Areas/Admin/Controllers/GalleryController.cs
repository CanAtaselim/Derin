using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Web.Attributes;
using Derin.Web.WebCommon;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Derin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = new string[] { "SYSTEM_ADMIN", "ADMIN" })]
    public class GalleryController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;
        private IHostingEnvironment _env;


        public GalleryController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List()
        {
            return ViewComponent("GalleryEdit");
        }

        [HttpPost]
        public async Task<IActionResult> Save(GalleryVM galleryVM)
        {
            AjaxMessage aMsg = new AjaxMessage();
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var totalFileSize = 25 * 1024 * 1024;
                if (files.Sum(m => m.Length) > totalFileSize)
                {
                    aMsg.Status = 0;
                    aMsg.Message = "25 MB limitiniz aştınız.";
                }
                else
                {
                    string lowresDirectory = Path.Combine(_env.WebRootPath, "images\\gallery\\derin\\lowres");
                    string thumbnailDirectory = Path.Combine(_env.WebRootPath, "images\\gallery\\derin\\thumbnail");
                    if (!Directory.Exists(lowresDirectory))
                    {
                        Directory.CreateDirectory(lowresDirectory);
                    }
                    if (!Directory.Exists(thumbnailDirectory))
                    {
                        Directory.CreateDirectory(thumbnailDirectory);
                    }
                    foreach (IFormFile image in files)
                    {
                        int fileSizeLimit = 4 * 1024 * 1024;
                        if (image.Length < fileSizeLimit)
                        {
                            string imageName = Guid.NewGuid().ToString();
                            string imageExtension = Path.GetExtension(image.FileName);

                            try
                            {
                                ConvertLowres(lowresDirectory, image, imageName, imageExtension);
                            }
                            catch (Exception ex)
                            {

                            }

                            try
                            {
                                ConvertThumbnail(thumbnailDirectory, image, imageName, imageExtension);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    aMsg.Status = 1;
                    aMsg.Message = "Fotoğraf ekleme işlemi başarılı";
                }
            }
            else
            {
                aMsg.Status = 0;
                aMsg.Message = "Lütfen fotoğraf seçiniz.";

            }
            return Json(aMsg);
        }
        [HttpPost]
        public IActionResult Delete(string imageName)
        {
            AjaxMessage aMsg = new AjaxMessage();
            try
            {
                string lowresDirectory = Path.Combine(_env.WebRootPath, "images\\gallery\\derin\\lowres\\" + imageName);
                string thumbnailDirectory = Path.Combine(_env.WebRootPath, "images\\gallery\\derin\\thumbnail\\" + imageName);

                if (System.IO.File.Exists(thumbnailDirectory) && System.IO.File.Exists(lowresDirectory))
                {
                    System.IO.File.Delete(thumbnailDirectory);
                    System.IO.File.Delete(lowresDirectory);
                    aMsg.Status = 1;
                }
                else
                {
                    aMsg.Status = 0;
                    aMsg.Message = "Fotoğraf Bulunamadı.";
                }

            }
            catch (Exception ex)
            {
                aMsg.Status = 0;
                aMsg.Message = "Fotoğraf silme işlemi sırasında bir hata oluştu.";
            }
            return Json(aMsg);
        }
        private static void ConvertThumbnail(string thumbnailDirectory, IFormFile image, string imageName, string imageExtension)
        {

            using (MagickImage imageFile = new MagickImage(GetFormImageToByte(image)))
            {

                MagickGeometry reSize = new MagickGeometry(300);
                imageFile.AutoOrient();
                reSize.IgnoreAspectRatio = false;
                imageFile.Resize(reSize);
                imageFile.Write(Path.Combine(thumbnailDirectory, imageName + imageExtension));
            }
        }
        private static void ConvertLowres(string lowresDirectory, IFormFile image, string imageName, string imageExtension)
        {

            using (MagickImage imageFile = new MagickImage(GetFormImageToByte(image)))
            {

                MagickGeometry reSize = new MagickGeometry(new Percentage(90), new Percentage(90));
                imageFile.AutoOrient();
                reSize.IgnoreAspectRatio = false;
                imageFile.Resize(reSize);
                imageFile.Write(Path.Combine(lowresDirectory, imageName + imageExtension));
            }
        }
        public static byte[] GetFormImageToByte(IFormFile image)
        {
            byte[] data = null;
            if (image != null && image.Length > 0)
            {
                using (Stream inputStream = image.OpenReadStream())
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }

            }
            return data;
        }
    }
}