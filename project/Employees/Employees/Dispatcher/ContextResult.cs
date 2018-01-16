using API.Model;
using Employees.Dispatcher.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees
{
    public class ContextResult : IContext, IResult
    {
        public string ProcedureName { get; set; }
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();

        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}