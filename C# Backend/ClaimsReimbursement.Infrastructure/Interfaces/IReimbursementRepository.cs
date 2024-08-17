using ClaimsReimbursement.Infrastructure.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Interfaces
{
    public interface IReimbursementRepository : IRepository<Reimbursement>
    {
        Task<List<Reimbursement>> GetEmployeeClaims(string email);
        Task<List<Currencies>> GetCurrencies();
        Task<List<ReimbursementTypes>> GetReimbursementTypes();
        Task<List<Reimbursement>> GetAllPendingClaims();
        Task<List<Reimbursement>> GetAllApprovedClaims();
        Task<List<Reimbursement>> GetAllDeclinedClaims();
        Task<List<ReimbursementTypeTotalDTO>> GetTypeAndAmount();
        Task<List<MonthWiseReimbursements>> GetMonthAndAmount();
        Task<Reimbursement> GetClaimById(int id);
        Task<List<Reimbursement>> GetClaims();

    }
}
