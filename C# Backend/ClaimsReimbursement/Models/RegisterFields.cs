using ClaimsReimbursement.Domain.DTOS;

namespace ClaimsReimbursement.Models
{
    public class RegisterFields
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PANNumber { get; set; }
        public int BankId { get; set; }
        public int BankAccountNumber { get; set; }
    }
}
