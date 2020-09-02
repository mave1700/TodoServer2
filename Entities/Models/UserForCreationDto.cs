﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(45, ErrorMessage = "First name cannot be longer than 45 characters")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(45, ErrorMessage = "Last name cannot be longer than 45 characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [StringLength(30, ErrorMessage = "Username can't be longer than 30 characters")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [StringLength(30, ErrorMessage = "Password can't be longer than 30 characters")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
