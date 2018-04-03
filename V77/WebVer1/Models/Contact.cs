using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVer1.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Required]
        public string Message { get; set; }
    }
}