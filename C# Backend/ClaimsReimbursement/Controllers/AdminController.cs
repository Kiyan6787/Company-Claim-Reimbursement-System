using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsReimbursement.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminServices _services;

        public AdminController(IAdminServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Returns a list of all pending claims.
        /// </summary>
        /// <returns></returns>
        [HttpGet("pendingClaims")]
        public async Task<IActionResult> GetAllPendingClaims()
        {
            try
            {
                var res = await _services.GetAllPending();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method for approving a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("approveClaim")]
        public async Task<IActionResult> ApproveClaim([FromBody] ApproveClaimDTO dto, [FromQuery] int id)
        {
            Console.WriteLine("DTO: " + dto);
            Console.WriteLine("ID " + id);
            try
            {
                var res = await _services.ApproveClaim(id, dto.ApprovedBy, dto.ApprovedValue, dto.InternalNotes);

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
        /// Method for calling the service to decline a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("declineClaim")]
        public async Task<IActionResult> DeclineClaim([FromQuery] int id)
        {
            try
            {
                var res = await _services.DeclineClaim(id);

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
        /// Method that calls the service method for getting all approved claims.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getApprovedClaims")]
        public async Task<IActionResult> GetApprovedClaims()
        {
            try
            {
                var claims = await _services.GetAllApprovedClaims();

                return Ok(claims);
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }
        }

        /// <summary>
        /// Method that calls the service method for getting all declined claims.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getDeclinedClaims")]
        public async Task<IActionResult> GetDeclinedClaims()
        {
            try
            {
                var claims = await _services.GetAllDeclinedClaims();

                return Ok(claims);
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }
        }

        /// <summary>
        /// Method that calls the service method for getting all types and amount.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getTypesAndAmount")]
        public async Task<IActionResult> GetTypeAndAmount() { 
            try
            {
                var claims = await _services.GetTypeAndAmount();
                return Ok(claims);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("monthWiseReimbursements")]
        public async Task<IActionResult> GetMonthAndAmount()
        {
            try
            {
                var claims = await _services.GetMonthAndAmount();

                return Ok(claims);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getClaim")]
        public async Task<IActionResult> GetClaim([FromQuery] int id)
        {
            try
            {
                var res = await _services.GetReimbursementById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("allClaims")]
        public async Task<IActionResult> GetAllClaims()
        {
            try
            {
                var claims = await _services.GetClaims();

                return Ok(claims);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
