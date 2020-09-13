using Derin.Business.ViewModel.Base;


namespace Derin.Business.ViewModel
{
    public class RoleTopMenuVM : BaseVM
    {
        //Her iki alan DropDownListten seçildiğinden boş geçilme imkanı yok. 
        public long IdRoleTopMenu { get; set; }
        public long IdRoleRef { get; set; }
        public long IdTopMenuRef { get; set; }
        public string RoleName { get; set; }
        public string TopMenuName { get; set; }
    }

}
