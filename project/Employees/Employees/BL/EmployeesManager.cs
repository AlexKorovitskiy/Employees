using API.Model;
using BL;
using Employees.BL.API;
using Employees.Dispatcher;
using Employees.Dispatcher.API;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API;

namespace Employees.BL
{
    public class EmployeesManager : AbstractManagerExecute<IEmployee>//, IManager<Employee>
    {
        private static EmployeesManager _manager;

        #region override

        protected override string LoadProcedureName
        {
            get
            {
                return "LoadEmployee";
            }
        }

        protected override ICommandContext PreparerLoadList(IFilter filter)
        {
            throw new NotImplementedException();
        }

        protected override ICommandContext PreparerSave()
        {
            throw new NotImplementedException();
        }

        protected override ICommandContext PreparerDelete(IEmployee entity)
        {
            throw new NotImplementedException();
        }

        protected override IEmployee ParseLoadEntity(Dictionary<string, object> param)
        {
            throw new NotImplementedException();
        }

        #endregion

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
    }
}