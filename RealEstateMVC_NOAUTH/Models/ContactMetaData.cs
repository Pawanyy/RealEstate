using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class ContactMetaData
    {
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 2)]
        public string NAME;

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(255, MinimumLength = 2)]
        public string EMAIL;

        [Required]
        [Display(Name = "Subject")]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 2)]
        public string SUBJECT;

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10)]
        public string PHONE;

        [Required]
        [Display(Name = "Message")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 2)]
        public string MESSAGE;
    }
}