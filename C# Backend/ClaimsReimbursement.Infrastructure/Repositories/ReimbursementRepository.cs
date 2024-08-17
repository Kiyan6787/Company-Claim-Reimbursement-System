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
    public class ReimbursementRepository : Repository<Reimbursement>, IReimbursementRepository
    {
        private readonly AppDBContext _context;

        public ReimbursementRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all approved claims.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Reimbursement>> GetAllApprovedClaims()
        {
            return await _context.Reimbursements.Where(x => x.IsApproved == true).Include(x => x.ReimbursementType).Include(x => x.Currency).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all declined claims.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Reimbursement>> GetAllDeclinedClaims()
        {
            return await _context.Reimbursements.Where(x => x.IsApproved == false && x.IsProcessed == true).Include(x => x.ReimbursementType).Include(x => x.Currency).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all pending claims.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Reimbursement>> GetAllPendingClaims()
        {
            return await _context.Reimbursements.Where(x => x.IsProcessed == false).Include(x => x.Currency).Include(x => x.ReimbursementType).ToListAsync();
        }

        /// <summary>
        /// Gets the entire claim including associated currency and reimbursement type from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Reimbursement> GetClaimById(int id)
        {
            return await _context.Reimbursements.Where(x => x.Id == id).Include(x => x.Currency).Include(x => x.ReimbursementType).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns all claims and their associated currency and reimbursement type.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Reimbursement>> GetClaims()
        {
            return await _context.Reimbursements.Include(x => x.Currency).Include(x => x.ReimbursementType).ToListAsync();
        }

        /// <summary>
        /// Returns a list of all currencies stored in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Currencies>> GetCurrencies()
        {
            return await _context.Currencies.ToListAsync();
        }

        /// <summary>
        /// Returns a list of claims created by a specific employee.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<Reimbursement>> GetEmployeeClaims(string email)
        {
            Console.WriteLine($"Fetching claims for email: {email}");
            var claims = await _context.Reimbursements.AsNoTracking().Where(x => x.Email == email).Include(x => x.Currency).Include(s => s.ReimbursementType)
                         .ToListAsync();
            Console.WriteLine($"Found {claims.Count} claims");
            return claims;
        }

        public async Task<List<MonthWiseReimbursements>> GetMonthAndAmount()
        {
            return await _context.Reimbursements.GroupBy(r => new
            {
                Month = r.Date.Substring(0,7)
            })
            .Select(g => new MonthWiseReimbursements
            {
                MonthYear = g.Key.Month,
                TotalAmount = (decimal)g.Sum(r => r.ApprovedValue)
            }).ToListAsync();
        }

        /// <summary>
        /// Returns a list of reimbursement types.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementTypes>> GetReimbursementTypes()
        {
            return await _context.ReimbursementTypes.ToListAsync();
        }

        /// <summary>
        /// Returns a list of all types and the total amount for that type.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReimbursementTypeTotalDTO>> GetTypeAndAmount()
        {
            return await _context.Reimbursements.GroupBy(c => c.ReimbursementTypeId)
                   .Select(g => new ReimbursementTypeTotalDTO
                   {
                       ReimbursementTypeId = g.Key,
                       TotalAmount = (decimal)g.Sum(c => c.ApprovedValue)
                   })
                   .ToListAsync();
        }
    }
}
