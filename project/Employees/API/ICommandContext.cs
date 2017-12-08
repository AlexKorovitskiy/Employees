using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public interface ICommandContext
    {
        string ProcedureName { get; set; }
        Dictionary<string, object> Params { get; set; }
    }
}
