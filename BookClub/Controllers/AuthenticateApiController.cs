﻿using AutoMapper;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Westwind.AspNetCore.Security;

namespace BookClub.Controllers.api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly SignInManager<LoginUser> _signInManager;

        public AuthenticateController(UserManager<LoginUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            ILogger<AuthenticateController> logger,
            IMapper mapper,
            SignInManager<LoginUser> signInManager
            )
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user != null)
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                        if (result.Succeeded)
                        {
                            var claims = new List<Claim>();
                            claims.Add(new Claim("username", user.Email));
                            claims.Add(new Claim("displayname", user.UserName));
                            claims.Add(new Claim("Jti", Guid.NewGuid().ToString()));
                            claims.Add(new Claim("Id", user.Id.ToString()));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            var tokenOptions = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                                issuer: _config["Tokens:Issuer"],
                                audience: _config["Tokens:Audience"],
                                claims: claims.ToArray(),
                                expires: DateTime.Now.AddMinutes(90),
                                signingCredentials: creds
                            );
                            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                            return Ok(new { Token = tokenString });
                        }
                        return Unauthorized();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to create token: {ex}");
                }
            }
            return BadRequest();
        }

    }
}

