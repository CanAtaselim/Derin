using Derin.Business.ViewModel.Base;

namespace Derin.Business.ViewModel.Administration
{
    public class SystemUserRoleVM : BaseVM
    {
        public long IdUser { get; set; }
        public long IdRole { get; set; }
        public long? IdParentRole { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public bool? LocationAdmin { get; set; }
    }
    public class AddUserRoleVM : BaseVM
    {
        public long IdUser { get; set; }
        public long IdRole { get; set; }
        public long? IdCity { get; set; }
        public long? IdTown { get; set; }
        public long? IdVillage { get; set; }
    }
}
