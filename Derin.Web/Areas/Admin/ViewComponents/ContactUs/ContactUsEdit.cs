using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.ContactUs
{
    public class ContactUsEdit : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        public ContactUsEdit(AdministrationBLLocator administrationBLLocator)
        {
            _administrationBLLocator = administrationBLLocator;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {

            ContactUsVM contactUs = new ContactUsVM();

            contactUs = _administrationBLLocator.ContactUsBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active).FirstOrDefault();

            return Task.FromResult<IViewComponentResult>(View(contactUs ?? new ContactUsVM()));
        }

    }
}
