using ClaimsReimbursement.Infrastructure.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Infrastructure.Interfaces
{
    public interface IAuthRepo
    {
        Task<List<Bank>> GetAllBanks();
    }
}
