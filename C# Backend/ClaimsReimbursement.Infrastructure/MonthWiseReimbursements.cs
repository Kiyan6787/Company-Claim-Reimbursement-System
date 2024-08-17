using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure
{
    public class MonthWiseReimbursements
    {
        public string MonthYear { get; set; } 
        public decimal TotalAmount { get; set; }
    }
}
