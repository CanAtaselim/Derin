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
    
    public partial class Universities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Universities()
        {
            this.Faculties = new HashSet<Faculties>();
            this.Person = new HashSet<Person>();
        }
    
        public long IdUniversity { get; set; }
        public long OperationIdUserRef { get; set; }
        public string OperationIP { get; set; }
        public System.DateTime OperationDate { get; set; }
        public short OperationIsDeleted { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faculties> Faculties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> Person { get; set; }
    }
}
