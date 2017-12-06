using Employees.Dispatcher;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.BL
{
    public class UserManager : ManagerExecute<User>
    {
        private static UserManager _manager;

        /// <summary>
        /// Возвращает объет, для работы с бизнес-логикой
        /// </summary>
        /// <returns></returns>
        public static UserManager GetManager()
        {//синглтон для менеджера
            if (_manager == null)
            {
                _manager = new UserManager();
            }
            return _manager;
        }

        public static Result CheckValidationUser(string login,string password)
        {
            Context context = new Context();
            context.ProcedureName = "ValidateUser";
            context.Params.Add("Login", login);
            context.Params.Add("Password", password);
            Result resultTrransaction;
            List<ContextResult> contextResultCollection = ExecuterDBAction.ProcessCommand(context, out resultTrransaction);
            if (!resultTrransaction.Success)
                return null;
            List<User> entitys = User.ParseLoadList(contextResultCollection);
            if (entitys.Count != 1)
            {
                return new Result(false, "Пользователь с таким логином и паролем не найден");
            }
            return new Result(true, "Вы авторизированы") { ResultObject = entitys[0] };
        }
    }
}