using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCT.Core.Model.DTO;
using MyCT.Core.Model.Entities;
using MyCT.Interface.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace MyCT.Controller.Api
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        protected IServiceLocator _serviceLocator;
        private UserManager<CTUser> _userManager;
        protected Object _object;
        public BaseController(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
            
        }
        public BaseController(UserManager<CTUser> userManager)
        {
            this._userManager = userManager;
        }

        protected IActionResult BadRequestMessage(string message = "Mandatory Fields not filled.")
        {
            return BadRequest(message);
        }

        private int GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Convert.ToInt32(userId);
        }

        protected void AddCreatedBy<T>(T dto) where T : BaseDTO
        {
            ((BaseDTO)dto).CreatedOn = DateTime.Now;
            ((BaseDTO)dto).CreatedById = GetUserId();
        }

        protected void AddModifiiedBy<T>(T dto) where T : BaseDTO
        {
            ((BaseDTO)dto).ModifiedOn = DateTime.Now;
            ((BaseDTO)dto).ModifiedById = GetUserId();
        }
    }
}
