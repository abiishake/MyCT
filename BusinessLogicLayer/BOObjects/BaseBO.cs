using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BOObjects
{
    public class BaseBO
    {
        protected IServiceProvider _serviceProvider;
        public BaseBO(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
    }
}
