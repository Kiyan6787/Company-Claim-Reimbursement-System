using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Context.Entities
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }
        public string BankName { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
