using ClaimsReimbursement.Areas.Identity.Pages.Account;
using ClaimsReimbursement.Domain.Interfaces;
using ClaimsReimbursement.Infrastructure.Context;
using ClaimsReimbursement.JwtFeatures;
using ClaimsReimbursement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ClaimsReimbursement.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IAuthService _service;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtHandler jwtHandler, IAuthService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
            _service = service;
        }

        /// <summary>
        /// Controller method for registering a new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterFields model)
        {
            Console.WriteLine($"Received registration request: {JsonSerializer.Serialize(model)}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { 
                UserName = model.Email, 
                Email = model.Email,
                FullName = model.FullName,
                PANNumber = model.PANNumber,
                BankId = model.BankId,
                BankAccountNumber = model.BankAccountNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Controller method for logging out.
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Controller method for logging in to the application.
        /// </summary>
        /// <param name="userForAuthentication"></param>
        /// <returns></returns>
        [HttpPost("userLogin")]
        public async Task<IActionResult> Login1([FromBody] LoginFields userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            var approver = user.IsApprover;

            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, Email = user.Email, IsApprover = approver, Id = user.Id });
        }

        /// <summary>
        /// Controller method for getting the list of banks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getBanks")]
        public async Task<IActionResult> GetBanks()
        {
            try
            {
                var banks = await _service.GetAll();

                return Ok(banks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
