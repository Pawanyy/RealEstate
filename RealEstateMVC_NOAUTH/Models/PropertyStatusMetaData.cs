using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class PropertyStatusMetaData
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Status")]
        [StringLength(255, MinimumLength = 2)]
        public string STATUS;
    }
}