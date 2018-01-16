using Model.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public interface ICompany : IEntity
    {
        #region Fields
        //int? Id { get;} = null;

        [Display(Name = "Имя")]
        string Name { get; }

        [Display(Name = "Количество сотрудников")]
        int SizeCompany { get;}

        [Display(Name = "Форма собственности")]
        string OrganizationForm { get; }

        #endregion
    }
}
