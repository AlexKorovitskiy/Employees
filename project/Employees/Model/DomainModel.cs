using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DomainModel
    {
        private DomainModel _domainModel;
        public DomainModel GetDomainModelObj()
        {
            if (_domainModel == null)
                _domainModel = new DomainModel();
            return _domainModel;
        }

        private DomainModel()
        {

        }

    }
}
