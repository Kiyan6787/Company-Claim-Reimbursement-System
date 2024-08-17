using ClaimsReimbursement.Infrastructure.Context;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using ClaimsReimbursement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDBContext _context;

        public AuthRepo(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Repository method foe returning a list of banks.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Bank>> GetAllBanks()
        {
            return await _context.Bank.ToListAsync();
        }
    }
}
