using AutoMapper;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.Person
{
    public class PersonEdit : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        private IMemoryCache _memoryCache;
        private IMapper _mapper;
        public PersonEdit(AdministrationBLLocator administrationBLLocator, IMemoryCache memoryCache, IMapper mapper)
        {
            _administrationBLLocator = administrationBLLocator;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }
        public static List<SelectListItem> EmployeeTypeList
        {
            get
            {
                return new List<SelectListItem>() {
                    new SelectListItem()
                    {
                        Text = _Enumeration.GetEnumDescription(_Enumeration._EmployeeType.Managers).ToString(),
                        Value = ((int)_Enumeration._EmployeeType.Managers).ToString()
                    },
                    new SelectListItem()
                    {
                        Text = _Enumeration.GetEnumDescription(_Enumeration._EmployeeType.MedicalStaff).ToString(),
                        Value = ((int)_Enumeration._EmployeeType.MedicalStaff).ToString()
                    },
                    new SelectListItem()
                    {
                        Text = _Enumeration.GetEnumDescription(_Enumeration._EmployeeType.OurTeam).ToString(),
                        Value = ((int)_Enumeration._EmployeeType.OurTeam).ToString()
                    },
                };
            }
        }

        public Task<IViewComponentResult> InvokeAsync(long IdPerson)
        {

            ViewBag.EmployeeType = EmployeeTypeList;
            ViewBag.DepartmentList = HttpInfo.DepartmentList;

            PersonVM person = _mapper.Map<PersonVM>(_administrationBLLocator.PersonBL.CRUD.GetById(IdPerson));



            return Task.FromResult<IViewComponentResult>(View(person ?? new PersonVM()));
        }

    }
}
