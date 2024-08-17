using AutoMapper;
using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Domain.Interfaces;
using ClaimsReimbursement.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepo authRepo, IMapper mapper)
        {
            _authRepo = authRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Service method for returning a list of banks.
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankDTO>> GetAll()
        {
            var res = await _authRepo.GetAllBanks();
            return _mapper.Map<List<BankDTO>>(res);
        }
    }
}
