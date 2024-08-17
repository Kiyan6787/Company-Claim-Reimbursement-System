using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Context.Entities
{
    public class Reimbursement : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Date { get; set; }
        
        [Required]
        [ForeignKey("ReimbursementTypes")]
        public int ReimbursementTypeId { get; set; }
        public ReimbursementTypes ReimbursementType { get; set; }

        [Required]
        public decimal RequestedValue { get; set; }

        [Required]
        [ForeignKey("Currencies")]
        public int CurrencyId { get; set; }
        public Currencies Currency { get; set; }

        public string? Image { get; set; }
        public int? ApprovedValue { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool IsProcessed { get; set; } = false;
        public string? ApprovedBy { get; set; }
        public string? InternalNotes { get; set; }
    }
}
