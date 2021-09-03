using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyCT.Core.Model.DTO;
using MyCT.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCT.Controller.Account
{
    //[EnableCors("_myAllowSpecificOrigins")]
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<CTRole> _roleManager;
        private readonly SignInManager<CTUser> _signInManager;
        private readonly IConfiguration _configuration;

        public RoleController(RoleManager<CTRole> roleManager, SignInManager<CTUser> signInManager, IConfiguration configuration)
        {
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRole([FromBody] RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {

                CTRole cTRole = new CTRole()
                {
                    Name = roleDTO.Name
                };

                IdentityResult identityResult  = await _roleManager.CreateAsync(cTRole);

                if (identityResult.Succeeded)
                {
                    return Created("", new { cTRole.Id, cTRole.Name });
                }
                else
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditRole([FromBody] RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {

                CTRole cTRole = await _roleManager.FindByIdAsync(roleDTO.Id.ToString());
                cTRole.Name = roleDTO.Name;
                
                IdentityResult identityResult = await _roleManager.UpdateAsync(cTRole);

                if (identityResult.Succeeded)
                {
                    return Ok(new { cTRole.Id, cTRole.Name });
                }
                else
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet()]
        public IActionResult GetAllRoles()
        {
            List<CTRole> roles = _roleManager.Roles.ToList();
            if (roles.Count > 0)
            {
                return Ok(roles);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
