﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = new JwtTokenService(configuration);
        }
        
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);

                    var userRoles = await _userManager.GetRolesAsync(appUser);

                    var jwt = await _jwtTokenService.GenerateJwtToken(model.Email, appUser, userRoles);
                    var returnObj = new ReturnObject
                    {
                        Token = jwt.ToString(),
                        Roles = userRoles.ToList()
                    };
                    var json = JsonConvert.SerializeObject(returnObj);
                    return Ok(json);
                }
            }
            catch
            {
               return BadRequest("Incorrect username or password entered."); 
            }

            return Ok();
        }
       
        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email, 
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Customer");

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var roles = new List<string>
                {
                    "Customer"
                };
                return await _jwtTokenService.GenerateJwtToken(model.Email, user, roles);
            }
            
            throw new ApplicationException("UNKNOWN_ERROR");
        }
        
        private class ReturnObject
        {
            public string Token { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}