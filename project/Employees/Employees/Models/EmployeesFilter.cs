using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class EmployeesFilter : IFilter
    {
        public ICompany Company { get; set; }
    }
}
