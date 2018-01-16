using BL;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API;
using Server.API;

namespace Employees.BL.Manager
{
    public class UserManager : AbstractManagerExecute<User>
    {
        private static UserManager _manager;

        public static UserManager GetManager
        {
            get
            {
                if (_manager == null)
                    _manager = new UserManager();
                return _manager;
            }
        }
        private UserManager() { }

        #region override

        protected override string DeleteProcedureName
        {
            get
            {
                return "DeleteUser";
            }
        }

        protected override string LoadProcedureName
        {
            get
            {
                return "LoadUser";
            }
        }

        protected override User ParseLoadEntity(Dictionary<string, object> param)
        {
            User entity = new User();
            entity.Id = (int)param["Id"];
            entity.Name = param["Name"].ToString();
            entity.Password = param["Password"].ToString();
            entity.Login = param["Login"].ToString();
            return entity;
        }

        protected override ICommandContext PreparerLoadList(IFilter filter)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "LoadUsers";
            return context;
        }

        protected override ICommandContext PreparerSave(User entity)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "SaveUser";
            context.Params.Add("Id", entity.Id);
            context.Params.Add("Name", entity.Name);
            context.Params.Add("Login", entity.Login);
            context.Params.Add("Password", entity.Password);
            return context;
        }

        #endregion

        public Result<User> GetUserByNameAndPassword(string login,string password)
        {
            ICommandContext context = new CommandContext();
            context.ProcedureName = "ValidateUser";
            context.Params.Add("Login", login);
            context.Params.Add("Password",password);
            var result = RelatedServer.ExcecuteComand(context);
            if (!result.Success)
                return new Result<User>(false, result.Message);
            if (result.ResultValuesList.Count != 1)
                return new Result<User>(false, "Не удалось найти такого пользователя");
            User user = ParseLoadEntity(result.ResultValuesList[0]);
            return new Result<User>() { ResultEntity = user };
        }
    }
}