using API;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employees.BL.Filter
{
    public class EmployeesFilter:IFilter
    {
        public Company Company { get; set; }
    }
}