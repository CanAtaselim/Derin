//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Derin.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Banner
    {
        public long IdBanner { get; set; }
        public long OperationIdUserRef { get; set; }
        public string OperationIP { get; set; }
        public System.DateTime OperationDate { get; set; }
        public short OperationIsDeleted { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
    }
}
