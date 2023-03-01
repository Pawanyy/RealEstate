using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class CityMetaData
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        [StringLength(255, MinimumLength = 2)]
        public string NAME;

        [Display(Name = "State")]
        public int STATE_ID;
    }
}