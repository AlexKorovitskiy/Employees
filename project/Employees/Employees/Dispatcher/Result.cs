using Employees.Dispatcher.API;
using Employees.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.Dispatcher
{
    public class Result : IResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
       
        /// <summary>
        /// Возвращает объект операции, при успешном ее завершении
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public IEntity ResultObject { get; set; }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public Result()
        {

        }
    }
}