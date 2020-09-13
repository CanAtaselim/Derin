using Derin.Business.ViewModel.Base;
using Derin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Business.ViewModel.Administration
{
    public class UniversitiesVM : BaseVM
    {
        public long IdUniversity { get; set; }
        public string Name { get; set; }
    }
}
