using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employees.Models.Autorisation
{
    public class Login
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Пожалуйста, введите свой логин")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пожалуйста, введите свой пароль")]
        public string Password { get; set; }
    }
}