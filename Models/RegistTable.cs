using MessagePack;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace RegistrationProject.Models
{
    
    public partial class RegistTable
    {
        [Key]
        public int Id { get; set; }


        [System.ComponentModel.DataAnnotations.Required(ErrorMessage ="Please Enter Name")]
        
        public string Name { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Enter EmailId")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Select Your Gender")]
        public string Gender { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Enter your Monbile Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit mobile number")]
        [Display(Name ="Mobile Number")]
        public string MobileNumber { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Select your Country")]
        public string Country { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please Submit an Image")]
        [Display(Name ="Profile Image")]
        public string ProfileImage { get; set; } = null!;
    }
}
