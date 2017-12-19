using Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;

namespace Model
{
    public class BaseCollection<T> : EntityBaseCollection<T> where T : IEntity
    {
        public override ICommandContext PreparerLoad(IFilter filter)
        {
            return null;
        }
    }
}
