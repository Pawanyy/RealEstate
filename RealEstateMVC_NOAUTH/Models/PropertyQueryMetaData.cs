using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateMVC_NOAUTH.Models
{
    public class PropertyQueryMetaData
    {
        [DataType(DataType.Text)]
        [Display(Name = "Question")]
        [StringLength(255, MinimumLength = 2)]
        public string QUESTION;

        [DataType(DataType.Text)]
        [Display(Name = "Answer")]
        [StringLength(255, MinimumLength = 2)]
        public string ANSWER;

        [DataType(DataType.Text)]
        [Display(Name = "Question Date")]
        [StringLength(255, MinimumLength = 2)]
        public Nullable<System.DateTime> Q_DATE;

        [DataType(DataType.Text)]
        [Display(Name = "Answer Date")]
        [StringLength(255, MinimumLength = 2)]
        public Nullable<System.DateTime> A_DATE;
    }
}