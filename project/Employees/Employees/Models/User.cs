using Microsoft.AspNet.Identity;
using Model.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employees.Models
{
    public class User : IEntity
    {
        public int? Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Пожалуйста, введите свой логин")]
        public string Login { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пожалуйста, введите свой пароль")]
        public string Password { get; set; }
        
        public override string ToString()
        {
            return Login;
        }
    }
}