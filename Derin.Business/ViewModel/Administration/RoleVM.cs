using Derin.Business.ViewModel.Base;
using System.ComponentModel.DataAnnotations;


namespace Derin.Business.ViewModel
{
    public class RoleVM : BaseVM
    {
        public long IdRole { get; set; }
        [Required(ErrorMessage = "Üst Rol Adı Boş Geçilemez")]
        public long? IdParentRoleRef { get; set; }
        [Required(ErrorMessage = "Rol Açıklaması Boş Geçilemez")]
        public string RoleDescription { get; set; }
        [Required(ErrorMessage = "Rol Adı Boş Geçilemez")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Rol Kodu Boş Geçilemez")]
        public string RoleCode { get; set; }
        public long OperationIdUserRef { get; set; }
        [Required(ErrorMessage = "Statik Rol Boş Geçilemez")]
        public bool StaticRole { get; set; }
        [Required(ErrorMessage = "Yerel Yöetici Boş Geçilemez")]
        public bool LocationAdmin { get; set; }
        [Required(ErrorMessage = "Dağıtık Rol Boş Geçilemez (Kullanıcı Yönetiminde Görünmesi İçin Gerekli)")]
        public bool IsRoleDistribution { get; set; }


    }

}
