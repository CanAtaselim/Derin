using Derin.Business.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Business.ViewModel.Administration
{
    public class AboutUsVM : BaseVM
    {
        public long IdAboutUs { get; set; }

        [Required(ErrorMessage = "Lütfen hakkımızda bölümünü doldurunuz.")]
        public string Mission { get; set; }
        public string Vision { get; set; }
        public byte[] Picture { get; set; }

    }
}
