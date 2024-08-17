using AutoMapper;
using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Domain.Interfaces;
using ClaimsReimbursement.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsReimbursement.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ReimbursementController : Controller
    {
        private readonly IReimbursementService _service;
        private readonly UserManager<AppUser> _userManager;

        public ReimbursementController(IReimbursementService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        /// <summary>
        /// Controller method for returning a list of claims of a specific employee.
        /// </summary>
        /// <param name="useremail"></param>
        /// <returns></returns>
        [HttpGet("claims")]
        public async Task<IActionResult> GetEmployeeClaims([FromQuery] string useremail)
        {
            Console.WriteLine("**Email**: " + useremail);
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not authenticated.");
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var email = user?.Email;
                Console.WriteLine("Email Address of user: " + email);

                var claims = await _service.GetEmployeeClaims(useremail);

                return Ok(claims);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller method for creating a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("createClaim")]
        public async Task<IActionResult> CreateClaim([FromBody] ReimbursementDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user?.Email;

            try
            {
                var claim = await _service.CreateAsync(dto);

                return Ok(claim);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller method for deleting a claim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteClaim")]
        public async Task<IActionResult> DeleteClaim([FromQuery] int id)
        {

            var res = await _service.DeleteAsync(id);
            Console.Write("Res:" + res);

            if (res)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Controller method for updating a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("updateClaim")]
        public async Task<IActionResult> EditClaim([FromBody] ReimbursementDTO dto, [FromQuery] int id)
        {
            try
            {
                var res = await _service.UpdateAsync(dto, id);

                if (res)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller method for getting a claim by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getClaim")]
        public async Task<IActionResult> GetClaim([FromQuery] int id)
        {
            try
            {
                var res = await _service.GetByIdAsync(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller method for getting a list of currencies.
        /// </summary>
        /// <returns></returns>
        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            try
            {
                var res = await _service.GetCurrenciesAsync();

                return Ok(res); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller method for getting types of reimbursement types.
        /// </summary>
        /// <returns></returns>
        [HttpGet("reimbursementTypes")]
        public async Task<IActionResult> GetReimbursementTypes()
        {
            try
            {
                var res = await _service.GetReimbursementTypesAsync();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
