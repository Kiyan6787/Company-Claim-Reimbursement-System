using AutoMapper;
using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Domain.Interfaces;
using ClaimsReimbursement.Infrastructure;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using ClaimsReimbursement.Infrastructure.Interfaces;

namespace ClaimsReimbursement.Domain.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IReimbursementRepository _repository;
        private readonly IMapper _mapper;

        public AdminServices(IReimbursementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Service method for approving a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ApproveClaim(int id, string approvedBy, decimal approvedValue, string internalNotes)
        {
            var claim = await _repository.GetClaimById(id);
            if (claim == null)
                throw new NullReferenceException("Claim not found");

            claim.IsApproved = true;
            claim.IsProcessed = true;
            claim.ApprovedBy = approvedBy;
            claim.ApprovedValue = (int?)approvedValue;
            claim.InternalNotes = internalNotes;

            try
            {
                var res = await _repository.UpdateAsync(claim);
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Service method for declining a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeclineClaim(int id)
        {
            var claim = await _repository.GetClaimById(id);
            if (claim == null)
                throw new NullReferenceException("Claim not found");

            claim.IsApproved = false;
            claim.IsProcessed = true;
            claim.ApprovedValue = 0;

            try
            {
                var res = await _repository.UpdateAsync(claim);
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Service method for getting all approved claims.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementDTO>> GetAllApprovedClaims()
        {
            var res = await _repository.GetAllApprovedClaims();
            return _mapper.Map<List<ReimbursementDTO>>(res);
        }

        /// <summary>
        /// Service method for getting all declined claims.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementDTO>> GetAllDeclinedClaims()
        {
            var res = await _repository.GetAllDeclinedClaims();
            return _mapper.Map<List<ReimbursementDTO>>(res);
        }

        /// <summary>
        /// Service method for getting all pending claims.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementDTO>> GetAllPending()
        {
            var res = await _repository.GetAllPendingClaims();
            return _mapper.Map<List<ReimbursementDTO>>(res);
        }

        public async Task<List<ReimbursementDTO>> GetClaims()
        {
            var res = await _repository.GetClaims();
            return _mapper.Map<List<ReimbursementDTO>>(res);
        }

        public async Task<List<MonthWiseReimbursements>> GetMonthAndAmount()
        {
            return await _repository.GetMonthAndAmount();
        }

        public async Task<ReimbursementDTO> GetReimbursementById(int id)
        {
            var res = await _repository.GetClaimById(id);
            return _mapper.Map<ReimbursementDTO>(res);
        }

        /// <summary>
        /// Service method for getting the types and total amount for that type.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementTypeTotalDTO>> GetTypeAndAmount()
        {
            return await _repository.GetTypeAndAmount();
        }
    }
}
