using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;
using Derin.Web.Attributes;
using Derin.Web.WebCommon;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Derin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = new string[] { "SYSTEM_ADMIN", "ADMIN" })]

    public class BannerController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;
        private IHostingEnvironment _env;

        public BannerController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(BannerVM model)
        {
            AjaxMessage aMsg = new AjaxMessage();
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    var files = Request.Form.Files;
                    string fileName = Guid.NewGuid().ToString();
                    if (files.Count > 0)
                    {
                        IFormFile image = files[0];
                        string imageExtension = Path.GetExtension(image.FileName);


                        string filePath = Path.Combine(_env.WebRootPath, "images\\banner\\");
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }


                        using (MagickImage imageFile = new MagickImage(GetFormImageToByte(files[0])))
                        {

                            imageFile.AutoOrient();
                            imageFile.Write(Path.Combine(filePath, fileName + imageExtension));
                        }

                        Banner banner = new Banner();
                        banner.Description = model.Description;
                        banner.FileName = fileName;
                        banner.FilePath = filePath;
                        banner.OperationDate = DateTime.Now;
                        banner.OperationIdUserRef = HttpRequestInfo.UserID;
                        banner.OperationIP = HttpRequestInfo.IpAddress;
                        banner.OperationIsDeleted = 1;



                        if (model.IdBanner == 0)
                        {
                            _administrationBLLocator.BannerBL.CRUD.Insert(banner);
                            _administrationBLLocator.BannerBL.Save();
                            aMsg.Status = 1;
                            aMsg.Message = "Kayıt Başarıyla Eklendi.";

                        }
                        else
                        {
                            banner.IdBanner = model.IdBanner;
                            _administrationBLLocator.BannerBL.CRUD.Update(banner, HttpRequestInfo);
                            _administrationBLLocator.BannerBL.Save();
                            aMsg.Status = 1;
                            aMsg.Message = "Güncelleme Başarılı.";

                        }
                    }
                }
                else
                {
                    aMsg.Status = 0;
                    aMsg.Message = "Bir Hata oluştu";

                }
            }
            return Json(aMsg);
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