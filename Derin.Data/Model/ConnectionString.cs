using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Data.Model
{
    public static class ConnectionStrings
    {
        public static string Derin_Prod
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings.Get("EnvMode") == "Prod")
                {
                    return @"metadata=res://*/Model.Derin_Model.csdl|res://*/Model.Derin_Model.ssdl|res://*/Model.Derin_Model.msl;provider=System.Data.SqlClient; provider connection string=""data source=(LocalDB)\MSSQLLocalDB;initial catalog=Derin;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework""";
                }
                return @"metadata=res://*/Model.Derin_Model.csdl|res://*/Model.Derin_Model.ssdl|res://*/Model.Derin_Model.msl;provider=System.Data.SqlClient; provider connection string=""data source=(LocalDB)\MSSQLLocalDB;initial catalog=Derin;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework""";
            }
        }

    }
}
