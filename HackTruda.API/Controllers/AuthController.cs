﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HackTruda.API.Models;
using HackTruda.DataModels.Requests;
using HackTruda.DataModels.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HackTruda.API.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string _callbackScheme = "hacktruda";
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IdentityContext _context;

        
        public AuthController(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager, IdentityContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// Авторизация через сторонних провайдеров (соцсети)
        /// </summary>
        /// <param name="scheme"></param>
        /// <returns></returns>
        [HttpGet("{scheme}")]
        public async Task Get(string scheme)
        {
            AuthenticateResult auth = await Request.HttpContext.AuthenticateAsync(scheme);

            if (!auth.Succeeded ||
                auth?.Principal == null ||
                !auth.Principal.Identities.Any(id => id.IsAuthenticated) ||
                string.IsNullOrEmpty(auth.Properties.GetTokenValue("access_token")))
            {
                // Not authenticated, challenge
                await Request.HttpContext.ChallengeAsync(scheme);
            }
            else
            {
                var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;
                string email = string.Empty;

                email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;

                // Get parameters to send back to the callback
                var qs = new Dictionary<string, string>
                {
                    { "access_token", auth.Properties.GetTokenValue("access_token") },
                    { "refresh_token", auth.Properties.GetTokenValue("refresh_token") ?? string.Empty },
                    { "expires", (auth.Properties.ExpiresUtc?.ToUnixTimeSeconds() ?? -1).ToString() },
                    { "email", email }
                };

                // Build the result url
                var url = _callbackScheme + "://#" + string.Join(
                    "&",
                    qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                    .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

                // Redirect to final url
                Request.HttpContext.Response.Redirect(url);
            }
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<string> Get()
        {
            return User.Identity.Name;
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                AuthUser user = new AuthUser { UserName = request.UserName, PhoneNumber=request.Phone};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);

                    var u = _context.Users
                       .Where(x => x.UserName == request.UserName)
                       .FirstOrDefault();
                    return Ok(new AuthResponse() { UserId = u.UserId });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok();
        }

        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .Where(x => x.PhoneNumber == request.Phone)
                    .FirstOrDefault();

                if (user==null)
                {
                    return NotFound();
                }

                var result =
                    await _signInManager.PasswordSignInAsync(user, request.Password,true, false);

                if (result.Succeeded)
                {
                    return Ok(new AuthResponse() { UserId = user.UserId });
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
