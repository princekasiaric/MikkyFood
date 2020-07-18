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
using MFR.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<AppSettings> _appSettings;
        private readonly ISendMail _sendMail;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IConfiguration configuration,
                              IOptions<AppSettings> appSettings,
                              ISendMail sendMail,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _appSettings = appSettings;
            _sendMail = sendMail;
            _mapper = mapper;
        }

        [HttpPost(template: "signup")]
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
                    newUser.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(newUser, request.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Visitor");
                        return StatusCode(201, new ApiResponse { Status = true, Message = "Success" });
                        //var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                        //var callBackLink = Url.ActionLink("ConfirmEmail", "Auth", new { token, userId = newUser.Id }, Request.Scheme);
                        //await _sendMail.SendMailAsync(_appSettings.Value.FromAddress, request.Email, "Confirm your email address...", callBackLink);
                    }
                    ModelState.AddModelError("Signup", string.Join("", result.Errors.Select(e => e.Description)));
                }
            }
            return BadRequest(new ApiResponse { Status = false, Message = "Validation Error" });
        }

        [HttpPost(template: "signin")]
        public async Task<IActionResult> Signin([FromBody]SigninRequest request)
        {
            if (ModelState.IsValid)
            {
                var issuer = _appSettings.Value.Tokens.Issuer;
                var audience = _appSettings.Value.Tokens.Audience;
                var key = _configuration["SecretKey:Key"];

                var user = await _userManager.FindByEmailAsync(request.Username);
                if (user == null)
                {
                    return StatusCode(403, new ApiResponse { Status = false, Message = "Authorization Error" });
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
                    var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credential);
                    return Ok(new ApiResponse
                    {
                        Status = true,
                        Message = "Success",
                        Result = new
                        {
                            username = user.UserName,
                            name = $"{user.FirstName} {user.LastName}",
                            token = new JwtSecurityTokenHandler().WriteToken(token)
                        }
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
                        var subject = "Your account has been locked out due to someone trying to access it, click link below to reset password if you are the one.";
                        await _sendMail.SendMailAsync(_appSettings.Value.FromAddress, request.Username, subject, forgotPasswordLink);
                    }
                }
            }
            return BadRequest(new ApiResponse { Status = false, Message = "Validation Error" });
        }

        [HttpPost(template: "signout")]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new ApiResponse { Status = true, Message = "Success" });
        }

        [HttpPost(template: "externalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
            var callBackUrl = Url.Action("ExternalLoginCallBack", "Auth", new { }, Request.Scheme);
            properties.RedirectUri = callBackUrl;
            return Challenge(properties, provider);
        }

        [HttpGet(template: "externalLoginCallback")]
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

        //[HttpPost(template: "forgot_pwd")]
        //public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(request.Email);
        //        if (user == null)
        //        {
        //            return StatusCode(403, new ApiResponse
        //            {
        //                Status = false,
        //                Message = "Authorization Error"
        //            });
        //        }
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        if (user.Email == request.Email)
        //        {
        //            var callBackUrl = Url.ActionLink("ResetPassword", "Auth", new { token, email = request.Email }, Request.Scheme);
        //            await _sendMail.SendMailAsync(_appSettings.Value.FromAddress, request.Email, "Password reset token...", callBackUrl);
        //        }
        //        return Ok(new ApiResponse { Status = true, Message = "Success" });
        //    }
        //    return BadRequest(new ApiResponse { Status = false, Message = "Validation Error" });
        //}

        //[HttpGet(template: "pwdreset1")]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    return StatusCode(204, new ResetPasswordRequest { Email = email, Token = token });
        //}

        //[HttpPost(template: "pwdreset")]
        //public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(request.Email);
        //        if (user != null)
        //        {
        //            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        //            if (result.Succeeded)
        //            {
        //                return Ok(new ApiResponse { Status = true, Message = "Success", Result = Url.Action("Signin", "Auth") });
        //            }
        //            ModelState.AddModelError("Password Reset", string.Join("", result.Errors.Select(e => e.Description)));
        //        }
        //        return NotFound(new ApiResponse { Status = false, Message = "Invalid Username or Password" });
        //    }
        //    return BadRequest(new ApiResponse { Status = false, Message = "Validation Error" });
        //}

        //[HttpGet(template: "confirm_email")]
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    IdentityResult result;
        //    if (user != null)
        //    {
        //        result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return Ok(new ApiResponse { Status = true, Message = "Success", Result = Url.Action("signin", "Auth") });
        //        }
        //    }
        //    return BadRequest(new ApiResponse { Status = false, Message = $"{user.Email} is invalid." });

        //}
    }
}