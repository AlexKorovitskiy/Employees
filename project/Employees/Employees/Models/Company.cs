using System.ComponentModel.DataAnnotations;
using Model.API;

namespace Employees.Models
{
    public class Company: IEntity
    {
        #region Properies
        public int? Id { get; set; } = null;

        [Display (Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Количество сотрудников")]
        public int SizeCompany { get; set; }

        [Display(Name = "Форма собственности")]
        public string OrganizationForm { get; set; }

        #endregion

        #region Method
 
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}