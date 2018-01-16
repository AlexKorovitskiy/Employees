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
    public class CompanyManager : AbstractManagerExecute<ICompany>// ManagerExecute<ICompany> //IManager<ICompany>
    {
        private static CompanyManager _manager;

        #region override

        protected override string LoadProcedureName
        {
            get
            {
                return "LoadCompany";
            }
        }

        protected override ICompany ParseLoadEntity(Dictionary<string, object> param)
        {
            throw new NotImplementedException();
        }

        protected override ICommandContext PreparerDelete(ICompany entity)
        {
            throw new NotImplementedException();
        }

        protected override ICommandContext PreparerLoadList(IFilter filter)
        {
            throw new NotImplementedException();
        }

        protected override ICommandContext PreparerSave()
        {
            throw new NotImplementedException();
        }

        #endregion

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
    }
}