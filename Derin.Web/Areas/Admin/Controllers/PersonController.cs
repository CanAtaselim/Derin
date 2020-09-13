using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Data.Model;
using Derin.Web.Attributes;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Derin.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = new string[] { "SYSTEM_ADMIN", "ADMIN" })]

    public class PersonController : BaseController
    {
        private AdministrationBLLocator _administrationBLLocator;


        private IHostingEnvironment _env;

        public PersonController(AdministrationBLLocator administrationBLLocator, IHostingEnvironment env) : base(env)
        {
            _administrationBLLocator = administrationBLLocator;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(long idPerson)
        {
            return ViewComponent("PersonEdit", new { IdPerson = idPerson });
        }

        [HttpPost]
        public IActionResult List()
        {
            return ViewComponent("PersonList");
        }


        [HttpPost]
        public IActionResult Save(PersonVM model)
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


                    string delimiter = ",";
                    string Employees = model.EmployeeTypeList.Aggregate((i, j) => i + delimiter + j);
                    string Departmens = model.DepartmentList.Aggregate((i, j) => i + delimiter + j);


                    Person person = new Person();
                    person.EmployeeType = model.EmployeeType;
                    person.Department = model.Department;
                    person.Name = model.Name;
                    person.Surname = model.Surname;
                    person.Title = model.Title;
                    person.Profession = model.Profession;
                    person.Phone = model.Phone;
                    person.Gsm = model.Gsm;
                    person.About = model.About;
                    person.Picture = imageData != null ? imageData : model.Picture;
                    person.OperationDate = DateTime.Now;
                    person.OperationIdUserRef = HttpRequestInfo.UserID;
                    person.OperationIP = HttpRequestInfo.IpAddress;
                    person.OperationIsDeleted = 1;
                    //person.EmployeeTypeList = model.IdPerson > 0 ? Employees : model.Employees;
                    //person.DepartmentList = model.IdPerson > 0 ? Departmens : model.Departments;





                    if (model.IdPerson == 0)
                    {
                        _administrationBLLocator.PersonBL.CRUD.Insert(person);
                        _administrationBLLocator.PersonBL.Save();
                        aMsg.Status = 1;
                        aMsg.Message = "Kayıt Başarıyla Eklendi.";

                    }
                    else
                    {
                        person.IdPerson = model.IdPerson;
                        _administrationBLLocator.PersonBL.CRUD.Update(person, HttpRequestInfo);
                        _administrationBLLocator.PersonBL.Save();
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
        public IActionResult Delete(int? idPerson)
        {

            AjaxMessage aMsg = new AjaxMessage();

            if (idPerson != null)
            {
                var dropItem = _administrationBLLocator.PersonBL.CRUD.GetById(idPerson);
                if (dropItem != null)
                {
                    dropItem.OperationIsDeleted = (short)_Enumeration.IsOperationDeleted.Deleted;
                    _administrationBLLocator.PersonBL.Save();
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