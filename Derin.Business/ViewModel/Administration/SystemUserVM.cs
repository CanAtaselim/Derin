using Derin.Business.ViewModel.Base;
using System;

namespace Derin.Business.ViewModel.Administration
{
    public class SystemUserVM : BaseVM
    {
        public long IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePictureURL { get; set; }
        public DateTime? BirthDate { get; set; }
        public string IdNo { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public string Address { get; set; }
        public Guid? TBSID { get; set; }
    }
    public class TBSIDNOVM : BaseVM
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "TC Kimlik Bilgisi Boş Olamaz!")]
        public string IdNo { get; set; }
    }
    public class AddUserVM : BaseVM
    {
        public short Type { get; set; }
        public string IdNo { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
