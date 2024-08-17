using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Context.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
    }
}
