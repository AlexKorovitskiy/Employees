using API.Model;
using Model.API;
using System.Collections.Generic;

namespace Employees.BL.API
{
    public interface IManager<T> where T:IEntity 
    {
        /// <summary>
        /// Возвращает все сущности из базы
        /// </summary>
        /// <returns></returns>
        List<T> GetEntitys();

        /// <summary>
        /// Возвращает единственную сущность по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetEntityById(int id);
    }
}
