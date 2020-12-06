namespace Derin.Web.Areas.Admin.Controllers
{
    using Derin.Business.BusinessLogic.Locator;
    using Derin.Business.ViewModel.Administration;
    using Derin.Common;
    using Derin.Data.Model;
    using Derin.Web.Attributes;
    using Derin.Web.WebCommon;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.IO;

    [Area("Admin")]
    [Authorize(Roles = new string[] { "SYSTEM_ADMIN", "ADMIN" })]
    public class AboutUsController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;


        public AboutUsController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(AboutUsVM model)
        {
            AjaxMessage aMsg = new AjaxMessage();
            if (model != null)
            {
                if (ModelState.IsValid)
                {

                    var files = Request.Form.Files;
                    byte[] imageData = null;
                    if (files.Count > 0)
                    {
                        imageData = GetFormImageToByte(files[0]);
                    }

                    AboutUs aboutUs = new AboutUs();
                    aboutUs.Mission = model.Mission;
                    aboutUs.Vision = model.Vision;
                    aboutUs.Picture = imageData != null ? imageData : model.Picture;
                    aboutUs.OperationDate = DateTime.Now;
                    aboutUs.OperationIdUserRef = HttpRequestInfo.UserID;
                    aboutUs.OperationIP = HttpRequestInfo.IpAddress;
                    aboutUs.OperationIsDeleted = 1;

                    if (model.IdAboutUs == 0)
                    {
                        _administrationBLLocator.AboutUsBL.CRUD.Insert(aboutUs);
                        _administrationBLLocator.AboutUsBL.Save();
                        aMsg.Status = 1;
                        aMsg.Message = "Kayıt Başarıyla Eklendi.";

                    }
                    else
                    {
                        aboutUs.IdAboutUs = model.IdAboutUs;
                        _administrationBLLocator.AboutUsBL.CRUD.Update(aboutUs, HttpRequestInfo);
                        _administrationBLLocator.AboutUsBL.Save();
                        aMsg.Status = 1;
                        aMsg.Message = "Güncelleme Başarılı.";

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

        public IActionResult Delete(int? idAboutUs)
        {

            AjaxMessage aMsg = new AjaxMessage();

            if (idAboutUs != null)
            {
                var dropItem = _administrationBLLocator.AboutUsBL.CRUD.GetById(idAboutUs);
                if (dropItem != null)
                {
                    dropItem.OperationIsDeleted = (short)_Enumeration.IsOperationDeleted.Deleted;
                    _administrationBLLocator.AboutUsBL.Save();
                    aMsg.Status = 1;
                    aMsg.Message = "Kayıt Başarıyla Silinmiştir.";
                }
                else
                {
                    aMsg.Status = 0;
                    aMsg.Message = "Kayıt Bulunamadı!";
                }
            }
            else
            {
                aMsg.Status = 0;
                aMsg.Message = "Lütfen departman seçin!";
            }
            return Json(aMsg);
        }
        public IActionResult ReInvokeEditComponent()
        {
            return ViewComponent("AboutUsEdit");
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
