﻿using Employees.BL;
using Employees.Dispatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using API.Model;

namespace Employees.Models
{
    public class Employee : IEmployee
    {
        #region Fields

        public int? Id { get; set; } = null;

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string MidleName { get; set; }

        [Display(Name = "Дата приема")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Должность")]
        public Position Position { get; set; }

        [Display(Name = "Компания")]
        public int? CompanyId { get; set; }

        //[Display(Name = "Компания")]
        public ICompany Company { get; set; }

        #endregion
        //public Employee()
        //    : base(null)
        //{

        //}
        //public Employee(int? Id)
        //    : base(Id)
        //{

        //}

        #region Method

        #region Static

        /// <summary>
        /// Ззаполняем контекст своими полями для загрузки единственной сущности
        /// </summary>
        /// <param name="idCompany"></param>
        /// <returns></returns>
        public static IContext PrepareLoad(int idEmployee)
        {
            IContext context = new Context();
            context.ProcedureName = "LoadEmployee";
            context.Params.Add("Id", idEmployee);
            return context;
        }

        /// <summary>
        /// Ззаполняем контекст для загрузки всех сущностей
        /// </summary>
        /// <returns></returns>
        public static IContext PrepareLoadList(Company company)
        {
            IContext context = new Context();
            context.ProcedureName = "LoadEmployees";
            if (company != null && company.Id != null)
                context.Params.Add("IdCompany", company.Id);
            return context;
        }

        /// <summary>
        /// Парсим данные из резалта и возвращаем экземпляр нашего класса
        /// </summary>
        /// <param name="contextResult"></param>
        /// <returns></returns>
        public static Employee ParseLoad(ContextResult contextResult)
        {
            Employee entity = new Employee();
            entity.Id = (int)contextResult.Params["Id"];
            entity.FirstName = contextResult.Params["FirstName"].ToString();
            entity.MidleName = contextResult.Params["MidleName"].ToString();
            entity.SecondName = contextResult.Params["SecondName"].ToString();

            var tempDate = contextResult.Params["Date"];
            if (tempDate != null)
            {
                DateTime parseDate;
                if (DateTime.TryParse(tempDate.ToString(), out parseDate))
                    entity.Date = parseDate;

            }
            entity.Position = (Position)Enum.ToObject(typeof(Position), contextResult.Params["Position"]);
            var commpanyIdTemp = contextResult.Params["CompanyId"];
            if (commpanyIdTemp != null)
            {
                entity.CompanyId = (int)commpanyIdTemp;
                {//Знаю, что это очень плохо, извините:(
                    entity.Company = CompanyManager.GetManager().GetEntityById((int)entity.CompanyId);
                }
            }
            return entity;
        }

        /// <summary>
        /// Парсим  из резалта список наших сущностей и возвращаем их коллекцию
        /// </summary>
        /// <param name="contextResultCollection"></param>
        /// <returns></returns>
        public static List<Employee> ParseLoadList(List<ContextResult> contextResultCollection)
        {
            List<Employee> collection = new List<Employee>();
            foreach (var contextResult in contextResultCollection)
            {
                collection.Add(ParseLoad(contextResult));
            }
            return collection;
        }

        #endregion

        #endregion

        /// <summary>
        /// Создаем контекст для сохранения сущности
        /// </summary>
        /// <returns></returns>
        public IContext PrepareSave()
        {
            IContext context = new Context();
            context.ProcedureName = "SaveEmployee";
            context.Params.Add("Id", Id);
            context.Params.Add("FirstName", FirstName);
            context.Params.Add("MidleName", MidleName);
            context.Params.Add("SecondName", SecondName);
            context.Params.Add("Position", Position);
            context.Params.Add("CompanyId", CompanyId);
            return context;
        }

        /// <summary>
        /// Создаем контекст для удаления сущности
        /// </summary>
        /// <returns></returns>
        public IContext PrepareDelete()
        {
            IContext context = new Context();
            context.ProcedureName = "DeleteEmployee";
            context.Params.Add("Id", Id);
            return context;
        }
        //protected override Entity Clone()
        //{
        //    throw new NotImplementedException();
        //}

        //public override string DeleteProcedureName
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        //public override global::API.ICommandContext PreparerSave(Dictionary<string, object> param)
        //{
        //    throw new NotImplementedException();
        //}

        //public override string SaveProcedureName
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        //public override global::API.ICommandContext PreparerDelete()
        //{
        //    throw new NotImplementedException();
        //}
    }

}