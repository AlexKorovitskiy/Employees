using API.Model;
using BL;
using Employees.Models;
using System.Collections.Generic;
using System.Linq;
using API;

namespace Employees.BL
{
    public class CompanyManager : AbstractManagerExecute<Company>
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

        protected override string DeleteProcedureName
        {
            get
            {
                return "DeleteCompany";
            }
        }

        protected override Company ParseLoadEntity(Dictionary<string, object> param)
        {
            Company entity = new Company();
            entity.Id = (int)param["Id"];
            entity.Name = param["Name"].ToString();
            if (param.Keys.Contains("SizeCompany"))
            {
                int parseSizeCompany = -1;
                if (int.TryParse(param["SizeCompany"].ToString(), out parseSizeCompany))
                    entity.SizeCompany = parseSizeCompany;
            }
            entity.OrganizationForm = param["OrganizationForm"].ToString().ToUpper();
            return entity;
        }
        
        protected override ICommandContext PreparerLoadList(IFilter filter)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "LoadCompanys";
            return context;
        }
        
        protected override ICommandContext PreparerSave(Company entity)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "SaveCompany";
            context.Params.Add("Id", entity.Id);
            context.Params.Add("Name", entity.Name);
            context.Params.Add("OrganizationForm", entity.OrganizationForm);
            return context;
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