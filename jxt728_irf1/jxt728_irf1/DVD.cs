//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jxt728_irf1
{
    using System;
    using System.Collections.Generic;
    
    public partial class DVD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DVD()
        {
            this.Rentals = new HashSet<Rental>();
        }
    
        public int DVDSK { get; set; }
        public string Title { get; set; }
        public Nullable<int> CategoryFK { get; set; }
        public Nullable<decimal> NetPrice { get; set; }
        public Nullable<int> LanguageFK { get; set; }
        public Nullable<int> Stock { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
