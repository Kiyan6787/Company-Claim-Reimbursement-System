using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Domain.DTOS
{
    public class ReimbursementDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public int ReimbursementTypeId { get; set; }
        public CurrencyDto Currency { get; set; }
        public ReimbursementTypeDTO ReimbursementType { get; set; }
        public decimal RequestedValue { get; set; }
        public int CurrencyId { get; set; }
        public string Image { get; set; }
        public int? ApprovedValue { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsProcessed { get; set; } = false;
        public string? ApprovedBy { get; set; }
        public string? InternalNotes { get; set; }
    }
}
