using API;
using Model.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class EntityBaseCollection<T> : IEnumerable where T:IEntity 
    {
        public IEntity this[int id]
        {
            get
            {
                foreach (T item in this)
                {
                    if (item.Id == id)
                        return item;
                }
                return null;
            }
        }

        protected IFilter Filter { get; set; }

        public IEnumerator GetEnumerator()
        {
            foreach (T item in this)
            {
                yield return item;
            }
        }

        public List<T> LastOperationObjectCollection { get; set; } = new List<T>();

        public abstract ICommandContext PreparerLoad(IFilter filter);
    }
}
