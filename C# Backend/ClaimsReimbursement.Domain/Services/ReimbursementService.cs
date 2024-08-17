using AutoMapper;
using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Domain.Interfaces;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using ClaimsReimbursement.Infrastructure.Interfaces;

namespace ClaimsReimbursement.Domain.Services
{
    public class ReimbursementService : IReimbursementService
    {
        private readonly IMapper _mapper;
        private readonly IReimbursementRepository _repository;

        public ReimbursementService(IMapper mapper, IReimbursementRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Service method for creating a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ReimbursementDTO dto)
        {
            var newClaim = _mapper.Map<Reimbursement>(dto);
            var res = await _repository.InsertAsync(newClaim);

            return res;
        }

        /// <summary>
        /// Service method for deleting a claim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var claim = await _repository.GetByIdAsync(id);

            if (claim == null)
            {
                return false;
            }

            var res = await _repository.DeleteAsync(claim);

            return res;
        }

        /// <summary>
        /// Service method for getting a claim by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReimbursementDTO> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReimbursementDTO>(result);
        }

        /// <summary>
        /// Service methof for getting a list of currencies.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Currencies>> GetCurrenciesAsync()
        {
            return await _repository.GetCurrencies();
        }

        /// <summary>
        /// Service method for getting a list of claims associated with an employee.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<ReimbursementDTO>> GetEmployeeClaims(string email)
        {
            var claims = await _repository.GetEmployeeClaims(email);
            return _mapper.Map<List<ReimbursementDTO>>(claims); 
        }

        /// <summary>
        /// Service method for getting reimbursement types.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementTypes>> GetReimbursementTypesAsync()
        {
            return await _repository.GetReimbursementTypes();
        }

        /// <summary>
        /// Service method for updating a claim.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ReimbursementDTO dto, int id)
        {
            dto.Id = id;
            var map = _mapper.Map<Reimbursement>(dto);

            return await _repository.UpdateAsync(map);
        }
    }
}
