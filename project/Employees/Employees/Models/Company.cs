using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Employees.Models.API;
using Employees.Dispatcher;

namespace Employees.Models
{
    public class Company: IEntity
    {
        #region Fields
        public int? Id { get; set; } = null;

        [Display (Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Количество сотрудников")]
        public int SizeCompany { get; set; }

        [Display(Name = "Форма собственности")]
        public string OrganizationForm { get; set; }

        #endregion

        #region Method

        #region Static

        /// <summary>
        /// Заполняем контекст своими полями для загрузки единственной сущности
        /// </summary>
        /// <param name="idCompany"></param>
        /// <returns></returns>
        public static Context PrepareLoad(int idCompany)
        {
            Context context = new Context();
            context.ProcedureName = "LoadCompany";
            context.Params.Add("Id", idCompany);
            return context;
        }

        /// <summary>
        /// Ззаполняем контекст для загрузки всех сущностей
        /// </summary>
        /// <returns></returns>
        public static Context PrepareLoadList()
        {
            Context context = new Context();
            context.ProcedureName = "LoadCompanys";
            return context;
        }

        /// <summary>
        /// Парсим данные из резалта и возвращаем экземпляр нашего класса
        /// </summary>
        /// <param name="contextResult"></param>
        /// <returns></returns>
        public static Company ParseLoad(ContextResult contextResult)
        {
            Models.Company entity = new Models.Company();
            entity.Id = (int)contextResult.Params["Id"];
            entity.Name = contextResult.Params["Name"].ToString();
            if (contextResult.Params.Keys.Contains("SizeCompany"))
            {
                int parseSizeCompany = -1;
                if (int.TryParse(contextResult.Params["SizeCompany"].ToString(), out parseSizeCompany))
                    entity.SizeCompany = parseSizeCompany;
            }
            entity.OrganizationForm = contextResult.Params["OrganizationForm"].ToString().ToUpper();
            return entity;
        }

        /// <summary>
        /// Парсим  из резалта список наших сущностей и возвращаем их коллекцию
        /// </summary>
        /// <param name="contextResultCollection"></param>
        /// <returns></returns>
        public static List<Company> ParseLoadList(List<ContextResult> contextResultCollection)
        {
            List<Company> collection = new List<Company>();
            foreach (var contextResult in contextResultCollection)
            {
                collection.Add(ParseLoad(contextResult));
            }
            return collection;
        }

        #endregion

        /// <summary>
        /// Создаем контекст для сохранения сущности
        /// </summary>
        /// <returns></returns>
        public Context PrepareSave()
        {
            Context context = new Context();
            context.ProcedureName = "SaveCompany";
            context.Params.Add("Id", Id);
            context.Params.Add("Name", Name);
            context.Params.Add("OrganizationForm", OrganizationForm);
            return context;
        }

        /// <summary>
        /// Создаем контекст для удаления сущности
        /// </summary>
        /// <returns></returns>
        public Context PrepareDelete()
        {
            Context context = new Context();
            context.ProcedureName = "DeleteCompany";
            context.Params.Add("Id", Id);
            return context;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}