using Derin.Business.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Business.ViewModel.Administration
{
    public class GalleryVM : BaseVM
    {
        public List<GalleryItem> GalleryList { get; set; }
        public int Department { get; set; }
    }
    public class GalleryItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DepartmentName { get; set; }

    }
}
