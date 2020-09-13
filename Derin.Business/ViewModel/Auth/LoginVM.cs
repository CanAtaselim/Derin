using Derin.Business.ViewModel.Base;
using System.ComponentModel.DataAnnotations;



namespace Derin.Business.ViewModel.Login
{
    public class LoginVM : BaseVM
    {
        [Required(ErrorMessage = "Kullanıcı Adınızı Giriniz.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifrenizi Giriniz.")]
        public string Password { get; set; }
        public string IpAddress { get; set; }
    }
}
