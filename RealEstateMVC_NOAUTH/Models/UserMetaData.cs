using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RealEstateMVC_NOAUTH.Models
{
    public class UserMetaData
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [StringLength(255, MinimumLength = 2)]
        public string EMAIL;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(255, MinimumLength = 2)]
        public string PASSWORD;

        [DataType(DataType.Text)]
        [Display(Name = "Full Name")]
        [StringLength(255, MinimumLength = 2)]
        public string FULLNAME;

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile")]
        [StringLength(10, MinimumLength = 10)]
        public string MOBILE;

        [DataType(DataType.MultilineText)]
        [Display(Name = "About Me")]
        [StringLength(1000, MinimumLength = 2)]
        public string ABOUT_ME;

        [DataType(DataType.DateTime)]
        [Display(Name = "Registration Date")]
        public Nullable<System.DateTime> REGISTRATION_DATE;
    }
}