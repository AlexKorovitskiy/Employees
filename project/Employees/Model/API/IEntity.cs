using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.API
{
    public interface IEntity
    {
        int? Id { get; set; }
        
        string SaveProcedureName { get; }
        string DeleteProcedureName { get; }

        ICommandContext PreparerSave(Dictionary<string, object> param);
        ICommandContext PreparerDelete();

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
