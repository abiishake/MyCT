using MyCT.Core.Model.Entities;
using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BOObjects
{
    public class BaseBO
    {
        protected IServiceLocator _serviceLocator;
        public BaseBO(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

      
    }
}
