using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class AdminMetaData
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [StringLength(255, MinimumLength = 2)]
        public string USERNAME;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(255, MinimumLength = 2)]
        public string PASSWORD;
    }
}