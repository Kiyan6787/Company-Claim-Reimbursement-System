using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Context.Entities
{
    public class ReimbursementTypes
    {
        [Key]
        public int ReimbursementTypeId { get; set; }
        public string Type { get; set; }
        public ICollection<Reimbursement> Reimbursements { get; set; }
    }
}
