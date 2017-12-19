
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
        public Entity(int? id)
        {
            Id = id;
        }
        public abstract string SaveProcedureName { get; }
        public abstract string DeleteProcedureName { get; }

        public abstract ICommandContext PreparerSave(Dictionary<string, object> param);
        public abstract ICommandContext PreparerDelete();

        protected abstract Entity Clone();
    }
}
