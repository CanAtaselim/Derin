using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public class ServicesController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;
        public ServicesController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
        }
        [HttpPost]
        public IActionResult Edit(long idServices)
        {
            return ViewComponent("ServicesEdit", new { IdServices = idServices });
        }

        [HttpPost]
        public IActionResult List()
        {
            return ViewComponent("ServicesList");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(ServicesVM model)
        {
            AjaxMessage aMsg = new AjaxMessage();
            if (model != null)
            {
                if (ModelState.IsValid)
                {


                    Services services = new Services();
                    services.Title = model.Title;
                    services.FullText = model.FullText;
                    services.Summary = model.Summary;
                    services.Icon = model.Icon;
                    services.OperationDate = DateTime.Now;
                    services.OperationIdUserRef = HttpRequestInfo.UserID;
                    services.OperationIP = HttpRequestInfo.IpAddress;
                    services.OperationIsDeleted = 1;



                    if (model.IdServices == 0)
                    {
                        _administrationBLLocator.ServicesBL.CRUD.Insert(services);
                        _administrationBLLocator.ServicesBL.Save();
                        aMsg.Status = 1;
                        aMsg.Message = "Kayıt Başarıyla Eklendi.";

                    }
                    else
                    {
                        services.IdServices = model.IdServices;
                        _administrationBLLocator.ServicesBL.CRUD.Update(services, HttpRequestInfo);
                        _administrationBLLocator.ServicesBL.Save();
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
        [HttpPost]
        public IActionResult Delete(int? idServices)
        {

            AjaxMessage aMsg = new AjaxMessage();

            if (idServices != null)
            {
                var dropItem = _administrationBLLocator.ServicesBL.CRUD.GetById(idServices);
                if (dropItem != null)
                {
                    dropItem.OperationIsDeleted = (short)_Enumeration.IsOperationDeleted.Deleted;
                    _administrationBLLocator.ServicesBL.Save();
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
    }
}