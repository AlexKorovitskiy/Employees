using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public interface IManager<T>
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

        void Save(T entity);
        void Delete(T entity);
        void Create(Dictionary<string, object> parametrs);

    }
}
