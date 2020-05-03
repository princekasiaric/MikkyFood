using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MFR.Core.DTO.Request;
using MFR.Core.DTO.Response;
using MFR.DomainModels.Identity;
using MFR.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MFR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ISendMail _sendMail;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IConfiguration configuration,
                              ISendMail sendMail,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _sendMail = sendMail;
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

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        var callBackLink = Url.ActionLink("ConfirmEmail", "Auth", new { token, userId = newUser.Id }, Request.Scheme);
                        await _sendMail.SendMailAsync("princekasiaric@gmail.com", request.Email, "Confirm your email address", callBackLink);
                    }
                    else
                    {
                        ModelState.AddModelError("Signup", string.Join("", result.Errors.Select(e => e.Description)));
                    }
                }
                return StatusCode(201, new ApiResponse
                {
                    Status = true,
                    Message = "Success"
                });
            }
            return BadRequest(new ApiResponse
            {
                Status = false,
                Message = "Validation Failure"
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
                    return StatusCode(403, new ApiResponse { Status = false, Message = "Not Authorized" });
                }
                var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);
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
                        Message = "Success",
                        Result = new { token = new JwtSecurityTokenHandler().WriteToken(token) }
                    });
                }
                if (!result.Succeeded)
                {
                    return BadRequest(new ApiResponse { Status = false, Message = "Invalid Username or Password" });
                }
                if (result.IsLockedOut)
                {
                    if (user.UserName == request.Username)
                    {
                        var forgotPasswordLink = Url.ActionLink("ForgotPassword", "Auth", new { }, Request.Scheme);
                        var subject = "Your account is locked out, click link below to reset password";
                        await _sendMail.SendMailAsync("princekasiaric@gmail.com", request.Username, subject, forgotPasswordLink);
                    }
                }
            }
            return BadRequest(new ApiResponse { Status = false, Message = "Validation Failure" });
        }

        [HttpPost("signout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new ApiResponse { Status = true, Message = "Success" }); 
        }

        [HttpPost("external_login")]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
            var callBackUrl = Url.Action("ExternalLoginCallBack", "Auth", new { }, Request.Scheme);
            properties.RedirectUri = callBackUrl;
            return Challenge(properties, provider);
        }

        [HttpGet("login")]
        public async Task<IActionResult> ExternalLoginCallBack()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var emailClaim = info.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var user = new User
            {
                Email = emailClaim.Value,
                UserName = emailClaim.Value
            };

            if (await _userManager.FindByEmailAsync(user.Email) == null)
            {
                await _userManager.CreateAsync(user);
                await _userManager.AddLoginAsync(user, info);
            }
            await _signInManager.SignInAsync(user, false);
            return Ok(new ApiResponse { Status = true, Message = "Success" });
        }

        [HttpPost("email")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return StatusCode(403, new ApiResponse 
                    { 
                        Status = false, 
                        Message = "Not Authorized"
                    });
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (user.Email == request.Email)
                {
                    var callBackUrl = Url.ActionLink("ResetPassword", "Auth", new { token, email = request.Email }, Request.Scheme);
                    await _sendMail.SendMailAsync("princekasiaric@gmail.com", request.Email, "Password reset token...", callBackUrl);
                }
                return Ok(new ApiResponse { Status = true, Message = "Success" });
            }
            return BadRequest(new ApiResponse { Status = false, Message = "Validation Failure" });
        }

        [HttpGet("token")]
        public IActionResult ResetPassword(string token, string email)
        {
            return StatusCode(204, new ResetPasswordRequest { Email = email, Token = token });
        }

        [HttpPost("password")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
                    if (result.Succeeded)
                    {
                        return Ok(new ApiResponse { Status = true, Message = "Success", Result = Url.Action("Signin") });
                    }
                    ModelState.AddModelError("Password Reset", string.Join("", result.Errors.Select(e => e.Description))); 
                }
                return NotFound(new ApiResponse { Status = false, Message = "Invalid Username or Password" });
            }
            return BadRequest(new ApiResponse { Status = false, Message = "Validation Failure" });
        }

        [HttpGet("confirm_email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new ApiResponse { Status = true, Message = "Success", Result = Url.Action("signin") });
            }
            return BadRequest(new ApiResponse { Status = false, Message = $"{user.Email} is invalid." });
        }
    }
}