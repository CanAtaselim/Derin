using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Derin.Common
{
    public static class SessionVariable
    {
        #region ModuleName: GENERAL SESSION VARIABLES
        public static long _IdUserRef
        {
            get
            {
                if (HttpContext.Current.Session["_CurrentUser_IdUser"] == null)
                {
                    return 0;
                }
                return (long)HttpContext.Current.Session["_CurrentUser_IdUser"];
            }
            set
            {
                HttpContext.Current.Session["_CurrentUser_IdUser"] = value;
            }
        }
        public static string _Username
        {
            get
            {
                if (HttpContext.Current.Session["_CurrentUser_Username"] == null)
                {
                    return string.Empty;
                }
                return (string)HttpContext.Current.Session["_CurrentUser_Username"];
            }
            set
            {
                HttpContext.Current.Session["_CurrentUser_Username"] = value;
            }
        }
        public static string _OperationIP
        {
            get
            {
                if (HttpContext.Current.Session["_CurrentUser_OperationUserIP"] == null)
                {
                    HttpContext context = HttpContext.Current;
                    string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        string[] addresses = ipAddress.Split(',');
                        if (addresses.Length != 0)
                        {
                            HttpContext.Current.Session["_CurrentUser_OperationUserIP"] = addresses[0];
                            return (string)HttpContext.Current.Session["_CurrentUser_OperationUserIP"];
                        }
                    }

                    HttpContext.Current.Session["_CurrentUser_OperationUserIP"] = context.Request.ServerVariables["REMOTE_ADDR"];
                }

                return (string)HttpContext.Current.Session["_CurrentUser_OperationUserIP"];
            }
        }
        public static string _MachineName
        {
            get
            {
                if (HttpContext.Current.Session["_CurrentUser_MachineName"] == null)
                {
                    HttpContext.Current.Session["_CurrentUser_MachineName"] = HttpContext.Current.Server.MachineName;
                }

                return (string)HttpContext.Current.Session["_CurrentUser_MachineName"];
            }
        }
        public static string _MachineIp
        {
            get
            {
                if (HttpContext.Current.Session["_CurrentUser_MachineIp"] == null)
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
                    IPAddress ipAddress = ipHostInfo.AddressList[0];

                    return (string)(HttpContext.Current.Session["_CurrentUser_MachineIp"] = ipAddress.ToString());
                }

                return (string)HttpContext.Current.Session["_CurrentUser_MachineIp"];
            }
        }
        public static string _ClientBrowser
        {
            get
            {
                if (HttpContext.Current.Session["_CurrentUser_ClientBrowser"] == null)
                {
                    return string.Empty;
                }

                return (string)HttpContext.Current.Session["_CurrentUser_ClientBrowser"];
            }
            set
            {
                HttpContext.Current.Session["_CurrentUser_ClientBrowser"] = value;
            }
        }
        #endregion
    }
}