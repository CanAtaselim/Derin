using Derin.Business.BusinessLogic.Locator;
using Derin.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Authentication.Cookies;
using Derin.Common;
using Derin.Business.ViewModel.Administration;

namespace Derin.Web.Attributes
{
    public class ContactUsAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                AdministrationBLLocator _locator = new AdministrationBLLocator();
                if (filterContext.HttpContext.Session.GetString("ContactUsData") == null)
                {
                    List<ContactUsVM> contactUs = _locator.ContactUsBL.GetVM(filter: m => m.OperationIsDeleted == (short)_Enumeration.IsOperationDeleted.Active);
                    filterContext.HttpContext.Session.SetString("ContactUsData", JsonConvert.SerializeObject(contactUs));
                }

            }
            catch (System.Exception ex)
            {

            }

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
