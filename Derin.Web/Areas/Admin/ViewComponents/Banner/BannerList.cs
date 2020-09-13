using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.Banner
{
    public class BannerList : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        public BannerList(AdministrationBLLocator administrationBLLocator)
        {
            _administrationBLLocator = administrationBLLocator;
        }
        public Task<IViewComponentResult> InvokeAsync()
        {

            List<BannerVM> banner = _administrationBLLocator.BannerBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active);
            return Task.FromResult<IViewComponentResult>(View(banner ?? new List<BannerVM>()));
        }

    }
}
