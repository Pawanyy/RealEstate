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
    
    [MetadataType(typeof(ContactMetaData))]
    public partial class CONTACT
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string SUBJECT { get; set; }
        public string PHONE { get; set; }
        public string MESSAGE { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
    }
}
