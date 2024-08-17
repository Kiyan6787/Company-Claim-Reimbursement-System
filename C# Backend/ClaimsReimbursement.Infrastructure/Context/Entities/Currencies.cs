using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Context.Entities
{
    public class Currencies
    {
        [Key]
        public int CurrencyId { get; set; }
        public string Code { get; set; }
        public ICollection<Reimbursement> Reimbursements { get; set; }
    }
}
