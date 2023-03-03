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

    [MetadataType(typeof(PropertyQueryMetaData))]
    public partial class PROPERTY_QUERY
    {
        public int ID { get; set; }
        public string QUESTION { get; set; }
        public string ANSWER { get; set; }
        public Nullable<int> PROPERTY_ID { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public Nullable<int> VENDOR_ID { get; set; }
        public Nullable<System.DateTime> Q_DATE { get; set; }
        public Nullable<System.DateTime> A_DATE { get; set; }
    
        public virtual PROPERTY PROPERTY { get; set; }
        public virtual USER USER { get; set; }
        public virtual USER USER1 { get; set; }
    }
}
