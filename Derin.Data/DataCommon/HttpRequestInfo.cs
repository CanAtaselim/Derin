using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Data.DataCommon
{
    public class HttpRequestInfo
    {
        public long UserID { get; set; }
        public string IpAddress { get; set; }
        public long WorkingModuleId { get; set; }
        public List<string> Roles { get; set; }
        public List<Role_List_Result> UserAuth { get; set; }
    }
}
