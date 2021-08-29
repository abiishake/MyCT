using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCT.Interface.ServiceLocator
{
    public interface IServiceLocator
    {
        T Resolve<T>();
    }
}
