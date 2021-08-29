using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MyCT.Controller.Api
{
    
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IServiceProvider _serviceProvider;
        public BaseController(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
    }
}
