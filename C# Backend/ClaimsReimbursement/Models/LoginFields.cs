namespace ClaimsReimbursement.Models
{
    public class LoginFields
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Id { get; set; }
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
        public bool IsApprover { get; set; }
    }
}
