using AutoMapper;
using N_Tier.Application.Models.Work;
using N_Tier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.MappingProfiles
{
    public class WorkProfile : Profile
    {
        public WorkProfile() 
        {
            CreateMap<CreateWorkModel, Work>();
            CreateMap<Work, WorkResponseModel>();
            CreateMap<WorkResponseModel, Work>();
        }
    }
}
