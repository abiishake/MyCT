﻿using Microsoft.AspNetCore.Http;
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
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<CTUser> _userManager;
        private SignInManager<CTUser> _signInManager;
        private IConfiguration _configuration;
        public AccountController(UserManager<CTUser> userManager, SignInManager<CTUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityResult identityResult = await _userManager.CreateAsync(new CTUser() { Email = userDTO.Email, UserName = userDTO.Email }, userDTO.Password);

                if (identityResult.Succeeded)
                {
                    return Ok(new { Status = "Created" });
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
                IdentityUserClaim<int> identityUserClaim = new IdentityUserClaim<int>();

                CTUser ctUser = await _userManager.FindByEmailAsync(userDTO.Email);
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(ctUser, userDTO.Password, false);

                if (result.Succeeded)
                {
                    SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                    {
                        Subject = (await _signInManager.CreateUserPrincipalAsync(ctUser)).Identities.First(),
                        Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["BearerTokens:ExpiryMins"])),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["BearerTokens:Key"])),SecurityAlgorithms.HmacSha256Signature)
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
