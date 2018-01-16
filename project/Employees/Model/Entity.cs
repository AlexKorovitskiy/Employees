
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

        public Entity()
        {

        }

        public Entity(int? id)
        {
            Id = id;
        }
        public virtual string SaveProcedureName { get; }
        public virtual string DeleteProcedureName { get; }

        public virtual ICommandContext PreparerSave(Dictionary<string, object> param)
        {
            throw new Exception("Не переопределен метод PreparerSave");
        }
        public virtual ICommandContext PreparerDelete()
        {
            throw new Exception("Не переопределен метод PreparerDelete");
        }

        protected virtual Entity Clone()
        {
            return null;// new Entity(Id);
        }
    }
}
