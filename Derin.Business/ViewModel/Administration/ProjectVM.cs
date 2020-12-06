using Derin.Business.ViewModel.Base;
using System;

namespace Derin.Business.ViewModel.Administration
{
    public class ProjectVM : BaseVM
    {
        public long IdProject { get; set; }
        public long OperationIdUserRef { get; set; }
        public string OperationIP { get; set; }
        public System.DateTime OperationDate { get; set; }
        public short OperationIsDeleted { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public byte[] Picture { get; set; }
    }
}
