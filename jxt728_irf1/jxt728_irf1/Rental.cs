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
    
    public partial class Rental
    {
        public int RentalSK { get; set; }
        public Nullable<int> MemberFK { get; set; }
        public Nullable<int> DVDFK { get; set; }
        public System.DateTime OutDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
    
        public virtual DVD DVD { get; set; }
        public virtual Member Member { get; set; }
    }
}