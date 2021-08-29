using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyCT.Controller.Api
{
    
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IServiceLocator _serviceLocator;
        public BaseController(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }
    }
}
