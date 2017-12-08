using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public interface IEntity
    {
        int? Id { get; set; }

        ///// <summary>
        ///// Создаем контекст для сохранения сущности
        ///// </summary>
        ///// <returns></returns>
        //Context PrepareSave();

        ///// <summary>
        ///// Создаем контекст для удаления сущности
        ///// </summary>
        ///// <returns></returns>
        // Context PrepareDelete();
    }
}
