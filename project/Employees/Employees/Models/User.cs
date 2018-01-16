using Employees.Dispatcher;
using Microsoft.AspNet.Identity;
using Model.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employees.Models
{
    //public class User: IEntity
    //{
    //    public int? Id { get; set; }

    //    [Display(Name = "Логин")]
    //    [Required(ErrorMessage ="Пожалуйста, введите свой логин")]
    //    public string Login { get; set; }

    //    [Display(Name = "Имя")]
    //    [Required(ErrorMessage = "Пожалуйста, введите свое имя")]
    //    public string Name { get; set; }

    //    [DataType (DataType.Password)]
    //    [Display(Name = "Пароль")]
    //    [Required(ErrorMessage = "Пожалуйста, введите свой пароль")]
    //    public string Password { get; set; }


    //    /// <summary>
    //    /// Создаем контекст для сохранения сущности
    //    /// </summary>
    //    /// <returns></returns>
    //    public Context PrepareSave()
    //    {
    //        Context context = new Context();
    //        context.ProcedureName = "SaveUser";
    //        context.Params.Add("Id", Id);
    //        context.Params.Add("Name", Name);
    //        context.Params.Add("Login", Login);
    //        context.Params.Add("Password", Password);
    //        return context;
    //    }

    //    /// <summary>
    //    /// Создаем контекст для удаления сущности
    //    /// </summary>
    //    /// <returns></returns>
    //    public Context PrepareDelete()
    //    {
    //        Context context = new Context();
    //        context.ProcedureName = "DeleteUser";
    //        context.Params.Add("Id", Id);
    //        return context;
    //    }

    //    public override string ToString()
    //    {
    //        return Name;
    //    }

    //    #region Static

    //    /// <summary>
    //    /// Ззаполняем контекст своими полями для загрузки единственной сущности
    //    /// </summary>
    //    /// <param name="idCompany"></param>
    //    /// <returns></returns>
    //    public static Context PrepareLoad(int id)
    //    {
    //        Context context = new Context();
    //        context.ProcedureName = "LoadUser";
    //        context.Params.Add("Id", id);
    //        return context;
    //    }

    //    /// <summary>
    //    /// Ззаполняем контекст для загрузки всех сущностей
    //    /// </summary>
    //    /// <returns></returns>
    //    public static Context PrepareLoadList()
    //    {
    //        Context context = new Context();
    //        context.ProcedureName = "LoadUsers";
    //        return context;
    //    }

    //    /// <summary>
    //    /// Парсим данные из резалта и возвращаем экземпляр нашего класса
    //    /// </summary>
    //    /// <param name="contextResult"></param>
    //    /// <returns></returns>
    //    public static User ParseLoad(ContextResult contextResult)
    //    {
    //        User entity = new User();
    //        entity.Id = (int)contextResult.Params["Id"];
    //        entity.Name = contextResult.Params["Name"].ToString();
    //        entity.Password = contextResult.Params["Password"].ToString();
    //        entity.Login = contextResult.Params["Login"].ToString();
    //        return entity;
    //    }

    //    /// <summary>
    //    /// Парсим  из резалта список наших сущностей и возвращаем их коллекцию
    //    /// </summary>
    //    /// <param name="contextResultCollection"></param>
    //    /// <returns></returns>
    //    public static List<User> ParseLoadList(List<ContextResult> contextResultCollection)
    //    {
    //        List<User> collection = new List<User>();
    //        foreach (var contextResult in contextResultCollection)
    //        {
    //            collection.Add(ParseLoad(contextResult));
    //        }
    //        return collection;
    //    }

    //    #endregion

    //}
}