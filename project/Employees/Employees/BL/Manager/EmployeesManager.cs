using API.Model;
using BL;
using Employees.Models;
using System;
using System.Collections.Generic;
using API;
using Employees.BL.Filter;

namespace Employees.BL
{
    public class EmployeesManager : AbstractManagerExecute<Employee>
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

        protected override string DeleteProcedureName
        {
            get
            {
                return "DeleteEmployee";
            }
        }

        protected override ICommandContext PreparerLoadList(IFilter filter)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "LoadEmployees";
            var currentFilter = filter as EmployeesFilter;
            if (currentFilter != null)
            {
                if(currentFilter.Company!=null)
                    context.Params.Add("IdCompany", currentFilter.Company.Id);
            }
            return context;
        }

        protected override ICommandContext PreparerSave(Employee entity)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "SaveEmployee";
            context.Params.Add("Id", entity.Id);
            context.Params.Add("FirstName", entity.FirstName);
            context.Params.Add("MidleName", entity.MidleName);
            context.Params.Add("SecondName", entity.SecondName);
            context.Params.Add("Position", entity.Position);
            context.Params.Add("CompanyId", entity.CompanyId);
            return context;
        }
        
        protected override Employee ParseLoadEntity(Dictionary<string, object> param)
        {
            Employee entity = new Employee();
            entity.Id = (int)param["Id"];
            entity.FirstName = param["FirstName"].ToString();
            entity.MidleName = param["MidleName"].ToString();
            entity.SecondName = param["SecondName"].ToString();

            var tempDate = param["Date"];
            if (tempDate != null)
            {
                DateTime parseDate;
                if (DateTime.TryParse(tempDate.ToString(), out parseDate))
                    entity.Date = parseDate;

            }
            entity.Position = (Position)Enum.ToObject(typeof(Position), param["Position"]);
            var commpanyIdTemp = param["CompanyId"];
            if (commpanyIdTemp != null)
            {
                entity.CompanyId = (int)commpanyIdTemp;
                {//Знаю, что это очень плохо, извините:(
                    var result = CompanyManager.GetManager().LoadEntity((int)entity.CompanyId);
                    if (result.Success)
                        entity.Company = result.ResultEntity;
                }
            }
            return entity;
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