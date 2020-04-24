using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MFR.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IConfiguration configuration,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost("signup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Siginup([FromBody]SignupRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(request);
                if (await _userManager.FindByEmailAsync(user.Email) == null)
                {
                    var newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.Email,
                        Email = user.Email
                    };
                    var result = await _userManager.CreateAsync(newUser, request.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Visitor");
                    }
                    else
                    {
                        ModelState.AddModelError("Signup", string.Join("", result.Errors.Select(e => e.Description)));
                    }
                }
                return StatusCode(201, new ApiResponse
                {
                    Status = true,
                    Message = "Successful"
                });
            }
            return BadRequest(new ApiResponse
            {
                Status = false,
                Message = "Validation error"
            });
        }

        [HttpPost("signin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin([FromBody]SigninRequest request)
        {
            if (ModelState.IsValid)
            {
                var issuer = _configuration["Tokens:Issuer"];
                var audience = _configuration["Tokens:Audience"];
                var key = _configuration["Tokens:Key"];

                var user = await _userManager.FindByEmailAsync(request.Username);
                if (user == null)
                {
                    return StatusCode(403, new ApiResponse { Status = false, Message = "Authorization failure" });
                }
                var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
                if (result.Succeeded)
                {
                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName)
                    };
                    var credential = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: credential);
                    return Ok(new ApiResponse
                    {
                        Status = true,
                        Message = "Successful",
                        Result = new { token = new JwtSecurityTokenHandler().WriteToken(token) }
                    });
                }
            }
            return BadRequest(new ApiResponse { Status = false, Message = "Validation error" });
        }

        [HttpPost("signout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new ApiResponse { Status = true, Message = "Successful" });
        }
    }
}