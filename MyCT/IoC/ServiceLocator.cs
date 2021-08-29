using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MyCT.IoC
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;
        public ServiceLocator(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return this._serviceProvider.GetRequiredService<T>();
        }
    }
}
