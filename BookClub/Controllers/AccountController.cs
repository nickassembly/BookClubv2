using AutoMapper;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Data.Entities.User;
using BookClub.Utils;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        private readonly BookClubContext _context;

        public AccountController(ILogger<AccountController> logger,
            SignInManager<LoginUser> signInManager,
            UserManager<LoginUser> userManager,
            IConfiguration config,
            IMapper mapper,
            BookClubContext context)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _context = context;
        }
        public IActionResult Login()
        {
            if (ModelState.IsValid)
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        Redirect(Request.Query["ReturnUrl"].First());
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }


        public JsonResult SearchUsers(string searchParam)
        {
            var model = _userManager.Users.Where(user => user.UserName.Contains(searchParam)).ToList();

            return Json(model);
        }


        public void AddUser([FromForm] LoginUser newUser)
        {
            var loggedInUser = UserUtils.GetLoggedInUser(this.User);

            var userToAdd = _context.Users.Where(u => u.Id == newUser.Id).FirstOrDefault();

            bool isValidFriendRequest = IsValidFriendRequest(userToAdd);

            if (userToAdd != null && isValidFriendRequest)
            {
                var friendToAdd = new LoginUserFriendship
                {
                    UserId = loggedInUser,
                    UserFriendId = userToAdd.Id
                };

                _context.LoginUserFriendships.Add(friendToAdd);
                _context.SaveChanges();

            }
        }

        public ActionResult RemoveUser(string userId)
        {
            var loggedInUser = UserUtils.GetLoggedInUser(this.User);

            var userToRemove = _context.LoginUserFriendships.Where(fid => fid.UserFriendId == userId).FirstOrDefault();

            if (userToRemove != null)
            {
                _context.LoginUserFriendships.Remove(userToRemove);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Profile");
        }

        public async Task<IActionResult> APILoginAsync([FromBody] LoginViewModel model)
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

        public async Task<IActionResult> Register([FromForm] RegisterViewModel modelUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _mapper.Map<LoginUser>(modelUser);

                    var result = await _userManager.CreateAsync(user, modelUser.Password);
                    if (result.Succeeded)
                    {
                        if (Request.Query.Keys.Contains("ReturnUrl"))
                        {
                            Redirect(Request.Query["ReturnUrl"].First());
                        }
                        return RedirectToAction("UserBookList", "Book");
                    }
                    ModelState.AddModelError("", "Failed to Login");
                    return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValidFriendRequest(LoginUser userToAdd)
        {
            // TODO: Add mechanism to confirm with user being requested before adding friend
            // This should only return a toast message for now, then another function needs to be added to confirm with other user upon loggin in. 
            return !_context.LoginUserFriendships.Where(f => f.UserFriendId == userToAdd.Id).Any();
        }
    }
}


