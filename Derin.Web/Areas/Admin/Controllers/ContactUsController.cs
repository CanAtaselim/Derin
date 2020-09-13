using System;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Data.Model;
using Derin.Web.Attributes;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Derin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = new string[] { "SYSTEM_ADMIN", "ADMIN" })]
    public class ContactUsController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;
        public ContactUsController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(ContactUsVM model)
        {
            AjaxMessage aMsg = new AjaxMessage();
            if (model != null)
            {
                if (ModelState.IsValid)
                {


                    ContactUs contactUs = new ContactUs();
                    contactUs.Department = model.Department;
                    contactUs.Address = model.Address;
                    contactUs.Email = model.Email;
                    contactUs.Phone = model.Phone;
                    contactUs.GSM = model.GSM;
                    contactUs.Fax =  model.Fax;
                    contactUs.Facebook  = model.Facebook;
                    contactUs.Twitter   = model.Twitter;
                    contactUs.Instagram  = model.Instagram;
                    contactUs.Youtube   = model.Youtube;
                    contactUs.Linkedin  = model.Linkedin;
                    contactUs.GooglePlus = model.GooglePlus;
                    contactUs.OperationDate = DateTime.Now;
                    contactUs.OperationIdUserRef = HttpRequestInfo.UserID;
                    contactUs.OperationIP = HttpRequestInfo.IpAddress;
                    contactUs.OperationIsDeleted = 1;



                    if (model.IdContactUs == 0)
                    {
                        _administrationBLLocator.ContactUsBL.CRUD.Insert(contactUs);
                        _administrationBLLocator.ContactUsBL.Save();
                        aMsg.Status = 1;
                        aMsg.Message = "Kayıt Başarıyla Eklendi.";

                    }
                    else
                    {
                        contactUs.IdContactUs = model.IdContactUs;
                        _administrationBLLocator.ContactUsBL.CRUD.Update(contactUs, HttpRequestInfo);
                        _administrationBLLocator.ContactUsBL.Save();
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
        public IActionResult Delete(int? idContactUs)
        {

            AjaxMessage aMsg = new AjaxMessage();

            if (idContactUs != null)
            {
                var dropItem = _administrationBLLocator.ContactUsBL.CRUD.GetById(idContactUs);
                if (dropItem != null)
                {
                    dropItem.OperationIsDeleted = (short)_Enumeration.IsOperationDeleted.Deleted;
                    _administrationBLLocator.ContactUsBL.Save();
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
        public IActionResult ReInvokeEditComponent(int Department)
        {
            return ViewComponent("ContactUsEdit", new { Department });
        }
    }
}