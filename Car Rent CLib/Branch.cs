//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Car_Rent_CLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            this.Cars_for_Rent = new HashSet<Cars_for_Rent>();
        }
    
        public int Branch_Id { get; set; }
        public string Branch_name { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Longatude { get; set; }
        public Nullable<decimal> Latitude { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cars_for_Rent> Cars_for_Rent { get; set; }
    }
}
