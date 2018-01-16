using System;
using System.ComponentModel.DataAnnotations;
using Model.API;

namespace Employees.Models
{
    public class Employee : IEntity
    {
        #region Fields

        public int? Id { get; set; } = null;

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string MidleName { get; set; }

        [Display(Name = "Дата приема")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Должность")]
        public Position Position { get; set; }

        [Display(Name = "Компания")]
        public int? CompanyId { get; set; }

        //[Display(Name = "Компания")]
        public Company Company { get; set; }

        #endregion


        #region Method
        public override string ToString()
        {
            return SecondName + FirstName + MidleName;
        }
        #endregion
    }
    public enum Position
    {
        Разработчик = 0,
        Тестировщик = 1,
        Бизнес_аналитик = 2,
        Менеджер = 3
    }
}