using ClaimsReimbursement.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<List<BankDTO>> GetAll();
    }
}
