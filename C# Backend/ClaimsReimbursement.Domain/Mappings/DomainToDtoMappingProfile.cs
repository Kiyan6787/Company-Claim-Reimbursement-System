using AutoMapper;
using ClaimsReimbursement.Domain.DTOS;
using ClaimsReimbursement.Infrastructure.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsReimbursement.Domain.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Reimbursement, ReimbursementDTO>().ReverseMap();
            CreateMap<Currencies, CurrencyDto>().ReverseMap();
            CreateMap<ReimbursementTypes, ReimbursementTypeDTO>().ReverseMap();
            CreateMap<Bank, BankDTO>().ReverseMap();
        }
    }
}
