//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstateMVC_NOAUTH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PROPERTY_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROPERTY_TYPE()
        {
            this.PROPERTies = new HashSet<PROPERTY>();
        }
    
        public int ID { get; set; }
    
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Type")]
        [StringLength(255, MinimumLength = 2)]
        public string NAME { get; set; }
    }
}
