using Employees.BL.API;
using Employees.Dispatcher;
using Employees.Dispatcher.API;
using Employees.Models;
using Employees.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.BL
{
    public class CompanyManager : ManagerExecute<Company>, IManager<Company>
    {
        private static CompanyManager _manager;

        /// <summary>
        /// Возвращает объет, для работы с бизнес-логикой
        /// </summary>
        /// <returns></returns>
        public static CompanyManager GetManager()
        {//синглтон для менеджера
            if (_manager == null)
            {
                _manager = new CompanyManager();
            }
            return _manager;
        }

        /// <summary>
        /// Возвращает список всех компаний
        /// </summary>
        /// <returns></returns>
        public List<Company> GetEntitys()
        {
            Context context = Company.PrepareLoadList();
            Result resultTrransaction;
            List<ContextResult> contextResultCollection = ExecuterDBAction.ProcessCommand(context, out resultTrransaction);
            if (!resultTrransaction.Success)
                return null;
            List<Company> entitys = Company.ParseLoadList(contextResultCollection);
            return entitys;
        }

        /// <summary>
        /// Возвращает компанию по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Company GetEntityById(int id)
        {
            Context context = Company.PrepareLoad(id);
            Result resultTrransaction;
            List<ContextResult> contextResultCollection = ExecuterDBAction.ProcessCommand(context, out resultTrransaction);
            if (!resultTrransaction.Success && contextResultCollection.Count < 0)
                return null;
            Company entity = Company.ParseLoad(contextResultCollection[0]);
            return entity;
        }

    }
}