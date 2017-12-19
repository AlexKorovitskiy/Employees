using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class AbstractFilter : IFilter
    {
        public List<int> Ids { get; set; } = new List<int>();

        public virtual string GetIdsString
        {
            get
            {
                StringBuilder strBuild = new StringBuilder();
                foreach (int id in Ids)
                {
                    strBuild.Append(id + ";");
                }
                return strBuild.ToString();
            }
        }
    }
}
