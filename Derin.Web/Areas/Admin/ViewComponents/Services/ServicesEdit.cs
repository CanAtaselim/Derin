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

namespace Derin.Web.Areas.Admin.ViewComponents.Services
{
    public class ServicesEdit : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        private IMapper _mapper;
        public ServicesEdit(AdministrationBLLocator administrationBLLocator, IMapper mapper)
        {
            _administrationBLLocator = administrationBLLocator;
            _mapper = mapper;
        }

        public Task<IViewComponentResult> InvokeAsync(long IdServices)
        {
            ServicesVM services = _mapper.Map<ServicesVM>(_administrationBLLocator.ServicesBL.CRUD.GetById(IdServices));
            return Task.FromResult<IViewComponentResult>(View(services ?? new ServicesVM()));
        }

    }
}
