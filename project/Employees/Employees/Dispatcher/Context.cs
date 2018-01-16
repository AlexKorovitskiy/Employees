using API.Model;
using Employees.Dispatcher.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.Dispatcher
{
    public class Context : IContext
    {
        public string ProcedureName { get; set; } = string.Empty;
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}