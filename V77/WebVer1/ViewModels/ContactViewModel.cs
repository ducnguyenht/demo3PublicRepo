using System;
using System.ComponentModel.DataAnnotations;


    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Message:")]
        public string Message { get; set; }
    }
