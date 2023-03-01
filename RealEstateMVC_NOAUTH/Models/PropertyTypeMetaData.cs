using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateMVC_NOAUTH.Models
{
    public class PropertyTypeMetaData
    {

        [Required]
        [Display(Name = "Property Type")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string NAME { get; set; }
    }
}