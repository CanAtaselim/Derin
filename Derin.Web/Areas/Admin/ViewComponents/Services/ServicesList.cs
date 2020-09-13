using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.Services
{
    public class ServicesList : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        public ServicesList(AdministrationBLLocator administrationBLLocator)
        {
            _administrationBLLocator = administrationBLLocator;
        }


        public Task<IViewComponentResult> InvokeAsync()
        {
            List<ServicesVM> services = _administrationBLLocator.ServicesBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active);
            return Task.FromResult<IViewComponentResult>(View(services ?? new List<ServicesVM>()));
        }
    }
}
