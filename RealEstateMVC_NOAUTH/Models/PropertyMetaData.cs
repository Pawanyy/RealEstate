using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateMVC_NOAUTH.Models
{
    public class PropertyMetaData
    {
        public int ID;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(255, MinimumLength = 2)]
        public string NAME;

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        [StringLength(1000, MinimumLength = 2)]
        public string DESCR;

        [Display(Name = "Type")]
        public Nullable<int> PROPERTY_TYPE_ID;

        [Display(Name = "Status")]
        public Nullable<int> STATUS_ID;

        [DataType(DataType.Text)]
        [Display(Name = "Location")]
        [StringLength(255, MinimumLength = 2)]
        public string LOCATION;

        [Display(Name = "Bedrooms")]
        [Range(0, 100)]
        public Nullable<int> BEDROOMS;

        [Display(Name = "Bathrooms")]
        [Range(0, 100)]
        public Nullable<int> BATHROOMS;

        [Display(Name = "Floors")]
        [Range(0, 100)]
        public Nullable<int> FLOORS;

        [Display(Name = "Garages")]
        [Range(0, 10)]
        public Nullable<int> GARAGES;

        [Display(Name = "Area")]
        [Range(1, 1000)]
        public string AREA;

        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        [Range(0, 1000000000)]
        public Nullable<double> PRICE;

        [DataType(DataType.Text)]
        [Display(Name = "Before Price Label")]
        [StringLength(255, MinimumLength = 2)]
        public string BEFORE_PRICE_LABEL;

        [DataType(DataType.Text)]
        [Display(Name = "After Price Label")]
        [StringLength(255, MinimumLength = 2)]
        public string AFTER_PRICE_LABEL;

        [DataType(DataType.Text)]
        [Display(Name = "Features")]
        [StringLength(1000, MinimumLength = 2)]
        public string FEATURES;

        [DataType(DataType.Upload)]
        [Display(Name = "Image 1")]
        public string IMG_1;

        [DataType(DataType.Upload)]
        [Display(Name = "Image 2")]
        public string IMG_2;

        [DataType(DataType.Upload)]
        [Display(Name = "Image 3")]
        public string IMG_3;

        [DataType(DataType.Upload)]
        [Display(Name = "Image 4")]
        public string IMG_4;

        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        [StringLength(1000, MinimumLength = 2)]
        public string ADDRESS;

        [Display(Name = "Country")]
        public Nullable<int> COUNTRY_ID;

        [Display(Name = "State")]
        public Nullable<int> STATE_ID;

        [Display(Name = "City")]
        public Nullable<int> CITY_ID;

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postol Code")]
        [StringLength(7, MinimumLength = 2)]
        public string POSTAL_CODE;

        [DataType(DataType.Text)]
        [Display(Name = "Neighbhorhood")]
        [StringLength(255, MinimumLength = 2)]
        public string NEIGHBORHOOD;

        [Display(Name = "Added By")]
        public Nullable<int> ADDED_BY_ID;

        [DataType(DataType.DateTime)]
        [Display(Name = "Added Date")]
        public System.DateTime ADDED_DATE;

    }
}