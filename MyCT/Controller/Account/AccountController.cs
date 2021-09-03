using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCT.Core.Model.DTO;
using MyCT.Core.Model.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MyCT.Controller.Account
{
    //[EnableCors("_myAllowSpecificOrigins")]
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<CTUser> _userManager;
        private readonly SignInManager<CTUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<CTUser> userManager, SignInManager<CTUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                CTUser cTUser = new CTUser()
                {
                    Email = userDTO.Email,
                    UserName = userDTO.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(cTUser, userDTO.Password);

                if (identityResult.Succeeded)
                {
                    return Created("", new { cTUser.Id, cTUser.Email });
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

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {

                CTUser ctUser = await _userManager.FindByEmailAsync(userDTO.Email);
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(ctUser, userDTO.Password, false);

                if (result.Succeeded)
                {
                    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                    {
                        Issuer = _configuration["BearerTokens:Issuer"],
                        Subject = (await _signInManager.CreateUserPrincipalAsync(ctUser)).Identities.First(),
                        Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["BearerTokens:ExpiryMins"])),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"])), SecurityAlgorithms.HmacSha256Signature)
                        
                    };
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    SecurityToken secToken = new JwtSecurityTokenHandler().CreateToken(descriptor);
                    return Ok(new { success = true, token = handler.WriteToken(secToken) });
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPost("signout")]
        //public async Task<IActionResult> ApiSignOut()
        //{
        //    await SignInManager.SignOutAsync();
        //    return Ok();
        //}
    }
}
