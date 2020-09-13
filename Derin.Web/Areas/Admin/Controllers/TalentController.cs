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
    public class TalentController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;


        public TalentController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(long idTalent)
        {
            return ViewComponent("TalentEdit", new { IdTalent = idTalent });
        }
        [HttpPost]
        public IActionResult List()
        {
            return ViewComponent("TalentList");
        }
        [HttpPost]
        public IActionResult Save(TalentVM model)
        {
            AjaxMessage aMsg = new AjaxMessage();
            //if (model != null)
            //{
            //    if (ModelState.IsValid)
            //    {


            //        Talent talent = new Talent();
            //        talent.Title = model.Title;
            //        talent.Description = model.Description;
            //        talent.OperationDate = DateTime.Now;
            //        talent.OperationIdUserRef = HttpRequestInfo.UserID;
            //        talent.OperationIP = HttpRequestInfo.IpAddress;
            //        talent.OperationIsDeleted = 1;



            //        if (model.IdTalent == 0)
            //        {
            //            _administrationBLLocator.TalentBL.CRUD.Insert(talent);
            //            _administrationBLLocator.TalentBL.Save();
            //            aMsg.Status = 1;
            //            aMsg.Message = "Kayıt Başarıyla Eklendi.";

            //        }
            //        else
            //        {
            //            talent.IdTalent = model.IdTalent;
            //            _administrationBLLocator.TalentBL.CRUD.Update(talent, HttpRequestInfo);
            //            _administrationBLLocator.TalentBL.Save();
            //            aMsg.Status = 1;
            //            aMsg.Message = "Güncelleme Başarılı.";

            //        }
            //    }
            //    else
            //    {
            //        aMsg.Status = 0;
            //        aMsg.Message = "Bir Hata oluştu";

            //    }
            //}
            return Json(aMsg);
        }

        public IActionResult Delete(int? idTalent)
        {

            AjaxMessage aMsg = new AjaxMessage();

            //if (idTalent != null)
            //{
            //    var dropItem = _administrationBLLocator.TalentBL.CRUD.GetById(idTalent);
            //    if (dropItem != null)
            //    {
            //        dropItem.OperationIsDeleted = (short)_Enumeration.IsOperationDeleted.Deleted;
            //        _administrationBLLocator.TalentBL.Save();
            //        aMsg.Status = 1;
            //        aMsg.Message = "Kayıt Başarıyla Silinmiştir.";
            //    }
            //    else
            //    {
            //        aMsg.Status = 0;
            //        aMsg.Message = "Kayıt Bulunamadı!";
            //    }
            //}
            //else
            //{
            //    aMsg.Status = 0;
            //    aMsg.Message = "Lütfen departman seçin!";
            //}
            return Json(aMsg);
        }
        public IActionResult ReInvokeEditComponent(int Department)
        {
            return ViewComponent("TalentUsEdit", new { Department });
        }

    }
}
