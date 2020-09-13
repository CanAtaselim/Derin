using Derin.Business.ViewModel.Base;
using System.ComponentModel.DataAnnotations;

namespace Derin.Business.ViewModel.Administration
{
    public class ServicesVM : BaseVM
    {
        public long IdServices { get; set; }
        [Required(ErrorMessage = "Hizmet adı zorunlu.")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "İçerik zorunlu.")]
        [MaxLength(3000)]
        public string FullText { get; set; }
        [MaxLength(300)]
        public string Summary { get; set; }
        public string Icon { get; set; }

    }
}
