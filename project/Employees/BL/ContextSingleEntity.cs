using Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class ContextSingleEntity<T> : ContextBase where T : IEntity
    {
        T Entity { get; set; }
        //ContextSingleEntity<T>()
    }
}
