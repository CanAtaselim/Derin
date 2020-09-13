using Derin.Business.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Business.ViewModel.Administration
{
    public class BannerVM : BaseVM
    {
        public long IdBanner { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
    }
}
