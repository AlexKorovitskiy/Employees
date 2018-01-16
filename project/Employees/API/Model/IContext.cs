using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IContext
    {
        string ProcedureName { get; set; }
        Dictionary<string, object> Params { get; set; }
    }
}
