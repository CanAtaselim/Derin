using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Derin.Web.WebCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Derin.Common.Derin_Exception;
using Derin.Common;

namespace Derin.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {

        public GlobalExceptionFilter()
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void OnException(ExceptionContext context)
        {
            Guid exCode = Guid.NewGuid();

            var response = new ErrorResponse()
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };
            var authInfo = context.HttpContext.User;
            int userId = -1;
            if (authInfo != null)
            {
                IEnumerable<Claim> claims = authInfo.Claims;
                userId = int.Parse(claims.Where(x => x.Type.ToLower().EndsWith("nameidentifier")).FirstOrDefault().Value);
            }
            GlobalExceptionObject ex = new GlobalExceptionObject();
            ex.Action = (string)context.RouteData.Values["action"];
            ex.Area = (string)context.RouteData.Values["area"];
            //ex.ClientBrowser = ((Http.FrameRequestHeaders)context.HttpContext.Request.Headers).HeaderUserAgent.FirstOrDefault();
            ex.Controller = (string)context.RouteData.Values["controller"];
            ex.ErrorMessage = response.Message;
            ex.IPAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            ex.StackTrace = response.StackTrace;
            ex.UserId = userId;
            ex.InnerException = context.Exception.InnerException == null ? string.Empty : context.Exception.InnerException.ToString();
            ex.ExCode = exCode;
            string iEx = "";
            if (ex.InnerException != null)
                iEx = ex.InnerException;
            Derin_Logging.WriteToQueue(Derin_Logging.Type.Exception, SerializeObject(ex));
            if (!EventLog.SourceExists(EventAppInfo.Source))
                EventLog.CreateEventSource(EventAppInfo.Source, EventAppInfo.Log);
            EventLog.WriteEntry(EventAppInfo.Source, response.Message + "---" + iEx, EventLogEntryType.Error);
            var ajaxMessage = new AjaxMessage()
            {
                Message = "Beklenmedik Bir Hata Oluştu!",
                Status = 4,
                Data = new { errCode = exCode, message = response.Message, innerException = iEx }
            };
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                context.Result = new ObjectResult(ajaxMessage)
                {
                    StatusCode = 500,
                    DeclaredType = typeof(AjaxMessage)
                };
            }
            else
            {
                BaseController bc = new BaseController();
                bc.ViewBag.Error = ex;
                context.Result = bc.RedirectToAction("Start", "Dashboard", new { area = "Main" });
            }
        }

        public class ErrorResponse
        {
            public string Message { get; set; }
            public string StackTrace { get; set; }
        }
    }
}
