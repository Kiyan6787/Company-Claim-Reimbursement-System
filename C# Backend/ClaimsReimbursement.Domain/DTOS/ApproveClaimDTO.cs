using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Domain.DTOS
{
    public class ApproveClaimDTO
    {
        public string ApprovedBy { get; set; }
        public decimal ApprovedValue { get; set; }
        public string InternalNotes { get; set; }
    }
}
