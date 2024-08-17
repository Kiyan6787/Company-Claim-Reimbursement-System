using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClaimsReimbursement.Infrastructure.Context;

// Add profile data for application users by adding properties to the AppUser class
public class AppUser : IdentityUser
{
    [StringLength(255)]
    public string FullName { get; set; }

    [MinLength(10),MaxLength(10)]
    public string PANNumber { get; set; }

    [ForeignKey("Bank")]
    public int BankId { get; set; }
    public Bank Bank { get; set; }

    [MinLength(12), MaxLength(12)]
    public int BankAccountNumber { get; set; }
    public bool IsApprover { get; set; } = false;
}

