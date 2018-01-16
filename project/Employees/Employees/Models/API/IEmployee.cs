using Model.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IEmployee : IEntity
    {
        [Display(Name = "Имя")]
        string FirstName { get; }

        [Display(Name = "Фамилия")]
        string SecondName { get; }

        [Display(Name = "Отчество")]
        string MidleName { get; }

        [Display(Name = "Дата приема")]
        [DataType(DataType.Date)]
        DateTime? Date { get; }

        [Display(Name = "Должность")]
        Position Position { get; }

        [Display(Name = "Компания")]
        int? CompanyId { get; }

        //[Display(Name = "Компания")]
        ICompany Company { get; }

        
    }
    public enum Position
    {
        Разработчик = 0,
        Тестировщик = 1,
        Бизнес_аналитик = 2,
        Менеджер = 3
    }
}
