﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFood.Models
{
    public class LoginCredential
    {
        [Required(ErrorMessage = "Email Address Required")]
        [Display(Name = "Email Address", Prompt = "Enter your email address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100)]
        public string? Email { get; set; }=string.Empty;

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }=string.Empty;
       
    }
}
