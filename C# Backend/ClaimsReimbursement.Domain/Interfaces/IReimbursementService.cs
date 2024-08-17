using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Domain.Interfaces
{
    public interface IReimbursementService
    {
        Task<ReimbursementDTO> GetByIdAsync(int id);
        Task<bool> CreateAsync(ReimbursementDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(ReimbursementDTO dto, int id);
        Task<List<ReimbursementDTO>> GetEmployeeClaims(string email);
        Task<List<Currencies>> GetCurrenciesAsync();
        Task<List<ReimbursementTypes>> GetReimbursementTypesAsync();
    }
}
