using Derin.Business.ViewModel.Base;
using System.ComponentModel.DataAnnotations;


namespace Derin.Business.ViewModel.Administration
{
    public class TopMenuVM : BaseVM
    {
        public long IdTopMenu { get; set; }
        public long OperationIdUserRef { get; set; }
        public string OperationIP { get; set; }
        public System.DateTime OperationDate { get; set; }
        public short OperationIsDeleted { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        [Required(ErrorMessage = "TopMenu Adı Boş Geçilemez")]
        public string TopMenuName { get; set; }
        [Required(ErrorMessage = "TopMenu Açıklaması Boş Geçilemez")]
        public string TopMenuDescription { get; set; }
        public short? TopMenuStatus { get; set; }
        public int? TopMenuOrder { get; set; }
        public string IconUrl { get; set; }
        public string ItemBackgroundColor { get; set; }
        public string ItemTextColor { get; set; }
        public string ItemIconColor { get; set; }

    }
}
