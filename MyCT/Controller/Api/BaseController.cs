using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCT.Core.Model.DTO;
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
        protected Object _object;
        public BaseController(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        protected IActionResult BadRequestMessage(string message = "Mandatory Fields not filled.")
        {
            return BadRequest(message);
        }

        protected void AddCreatedBy<T>(T dto) where T : BaseDTO
        {
            ((BaseDTO)dto).CreatedOn = DateTime.Now;
            // ((BaseDTO)dto).CreatedOn = DateTime.Now; created by
        }

        protected void AddModifiiedBy<T>(T dto) where T : BaseDTO
        {
            ((BaseDTO)dto).ModifiedOn = DateTime.Now;
            // ((BaseDTO)dto).CreatedOn = DateTime.Now; created by
        }
    }
}
