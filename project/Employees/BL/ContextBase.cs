using Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ContextBase<T> where T : IEntity
    {
        Result<T> result { get; set; }
    }
}
