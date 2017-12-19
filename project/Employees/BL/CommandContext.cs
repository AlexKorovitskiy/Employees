using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CommandContext : ICommandContext
    {
        public string ProcedureName { get; set; }
        public Dictionary<string, object> Params { get; set; } = new Dictionary<string, object>();
    }
}
