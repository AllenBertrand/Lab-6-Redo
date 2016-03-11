using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreDemo1.Models
{
    public class RegisterModel//this is for registration page
    {//THIS HAS BEEN HIJACKED BY ACCOUNTVIEWMODELS/REGISTERVIEWMODEL
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ForumNick { get; set; }
    }
}