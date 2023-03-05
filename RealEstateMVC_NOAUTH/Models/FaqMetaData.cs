using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class FaqMetaData
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Question")]
        [StringLength(255, MinimumLength = 2)]
        public string QUEST;

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 2)]
        [Display(Name = "Answer")]
        public string ANSWER;
    }
}