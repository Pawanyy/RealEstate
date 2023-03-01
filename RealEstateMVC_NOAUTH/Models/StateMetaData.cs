using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class StateMetaData
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        [StringLength(255, MinimumLength = 2)]
        public string NAME;

        [Display(Name = "Country")]
        public int COUNTRY_ID;
    }
}