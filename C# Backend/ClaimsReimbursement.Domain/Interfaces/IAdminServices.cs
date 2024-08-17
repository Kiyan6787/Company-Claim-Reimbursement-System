using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Infrastructure;

namespace ClaimsReimbursement.Domain.Interfaces
{
    public interface IAdminServices
    {
        Task<List<ReimbursementDTO>> GetAllPending();
        Task<bool> ApproveClaim(int id, string approvedBy, decimal approvedValue, string internalNotes);
        Task<List<ReimbursementDTO>> GetAllApprovedClaims();
        Task<List<ReimbursementDTO>> GetAllDeclinedClaims();
        Task<bool> DeclineClaim(int id);
        Task<List<ReimbursementTypeTotalDTO>> GetTypeAndAmount();
        Task<List<MonthWiseReimbursements>> GetMonthAndAmount();
        Task<ReimbursementDTO> GetReimbursementById(int id);
        Task<List<ReimbursementDTO>> GetClaims();

    }
}
