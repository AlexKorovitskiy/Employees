using BL;
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
    public class EmployeesManager: AbstractManagerExecute<Employee>//, IManager<Employee>
    {
        private static EmployeesManager _manager;
        
        /// <summary>
        /// Возвращает объет, для работы с бизнес-логикой
        /// </summary>
        /// <returns></returns>
        public static EmployeesManager GetManager()
        {
            if (_manager == null)
            {
                _manager = new EmployeesManager();
            }
            return _manager;
        }

        ///// <summary>
        ///// Возвращает список всех компаний
        ///// </summary>
        ///// <returns></returns>
        //public List<Employee> GetEntitys()
        //{
        //    Context context = Employee.PrepareLoadList(null);
        //    Result resultTrransaction;
        //    List<ContextResult> contextResultCollection = ExecuterDBAction.ProcessCommand(context, out resultTrransaction);
        //    if (!resultTrransaction.Success)
        //        return null;
        //    List<Employee> entitys = Employee.ParseLoadList(contextResultCollection);
        //    return entitys;
        //}

        ///// <summary>
        ///// Возвращает всех сотрудников для компании
        ///// </summary>
        ///// <param name="company"></param>
        ///// <returns></returns>
        //public List<Employee> GetEntitysForCompany(Company company)
        //{
        //    Context context = Employee.PrepareLoadList(company);
        //    Result resultTrransaction;
        //    List<ContextResult> contextResultCollection = ExecuterDBAction.ProcessCommand(context, out resultTrransaction);
        //    if (!resultTrransaction.Success)
        //        return null;
        //    List<Employee> entitys = Employee.ParseLoadList(contextResultCollection);
        //    return entitys;
        //}

        ///// <summary>
        ///// Возвращает компанию по id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public Employee GetEntityById(int id)
        //{
        //    Context context = Employee.PrepareLoad(id);
        //    Result resultTrransaction;
        //    List<ContextResult> contextResultCollection = ExecuterDBAction.ProcessCommand(context, out resultTrransaction);
        //    if (!resultTrransaction.Success && contextResultCollection.Count < 0)
        //        return null;
        //    Employee entity = Employee.ParseLoad(contextResultCollection[0]);
        //    return entity;
        //}

    }
}