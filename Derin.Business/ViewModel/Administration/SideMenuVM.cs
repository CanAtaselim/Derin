using Derin.Business.ViewModel.Base;
using System.ComponentModel.DataAnnotations;


namespace Derin.Business.ViewModel.Administration
{
    public class SideMenuVM : BaseVM
    {
        public long IdSideMenu { get; set; }
        public long OperationIdUserRef { get; set; }
        public string OperationIP { get; set; }
        public System.DateTime OperationDate { get; set; }
        public short OperationIsDeleted { get; set; }
        public long? IdParentSideMenu { get; set; }
        public long? IdTopMenuRef { get; set; }
        [Required(ErrorMessage = "SideMenu Area Boş Geçilemez")]
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        [Required(ErrorMessage = "SideMenu Adı Boş Geçilemez")]
        public string SideMenuName { get; set; }
        public string SideMenuDescription { get; set; }
        [Range(1, short.MaxValue, ErrorMessage = "Tamsayı Değer Vermelisiniz")]
        public short? SideMenuStatus { get; set; }
        [Range(1, short.MaxValue, ErrorMessage = "Tamsayı değer Vermelisiniz")]
        public int? SideMenuOrder { get; set; }
        public string IconUrl { get; set; }
        public string ItemBackgroundColor { get; set; }
        public string ItemTextColor { get; set; }
        public string ItemIconColor { get; set; }
    }
}
