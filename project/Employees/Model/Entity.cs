using API;
using Model.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Entity : IEntity
    {
        public int? Id { get; set; } = null;
        public abstract string SaveProcedureName { get; }
        public abstract string DeleteProcedureName { get; }

        protected abstract void PreparerSave(ICommandContext context);
        protected abstract void PreparerDelete(ICommandContext context);
    }
}
