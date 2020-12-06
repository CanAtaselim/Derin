using System;
using System.IO;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Data.Model;
using Derin.Web.Attributes;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Derin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = new string[] { "SYSTEM_ADMIN", "ADMIN" })]

    public class ProjectController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;
        public ProjectController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
        }
        [HttpPost]
        public IActionResult Edit(long idProject)
        {
            return ViewComponent("ProjectEdit", new { IdProject = idProject });
        }

        [HttpPost]
        public IActionResult List()
        {
            return ViewComponent("ProjectList");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(ProjectVM model)
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

                    Project project = new Project();
                    project.Title = model.Title;
                    project.Detail = model.Detail;
                    project.StartDate = model.StartDate;
                    project.EndDate = model.EndDate;
                    project.Picture = imageData != null ? imageData : model.Picture;
                    project.OperationDate = DateTime.Now;
                    project.OperationIdUserRef = HttpRequestInfo.UserID;
                    project.OperationIP = HttpRequestInfo.IpAddress;
                    project.OperationIsDeleted = 1;



                    if (model.IdProject == 0)
                    {
                        _administrationBLLocator.ProjectBL.CRUD.Insert(project);
                        _administrationBLLocator.ProjectBL.Save();
                        aMsg.Status = 1;
                        aMsg.Message = "Kayıt Başarıyla Eklendi.";

                    }
                    else
                    {
                        project.IdProject = model.IdProject;
                        _administrationBLLocator.ProjectBL.CRUD.Update(project, HttpRequestInfo);
                        _administrationBLLocator.ProjectBL.Save();
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
        public IActionResult Delete(int? idProject)
        {

            AjaxMessage aMsg = new AjaxMessage();

            if (idProject != null)
            {
                var dropItem = _administrationBLLocator.ProjectBL.CRUD.GetById(idProject);
                if (dropItem != null)
                {
                    dropItem.OperationIsDeleted = (short)_Enumeration.IsOperationDeleted.Deleted;
                    _administrationBLLocator.ProjectBL.Save();
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