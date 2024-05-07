using AutoMapper;
using N_Tier.Application.Models.Loan;
using N_Tier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.MappingProfiles
{
    public class LoanProfile : Profile
    {
        public LoanProfile() 
        {
            CreateMap<CreateLoanModel, Loan>();
            CreateMap<Loan, LoanResponseModel>();
            CreateMap<LoanResponseModel, Loan>();
        }
    }
}
