using AutoMapper;
using N_Tier.Application.Models.Author;
using N_Tier.Core.Entities;

namespace N_Tier.Application.MappingProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, AuthorResponseModel>();
            CreateMap<Author, UpdateAuthorModel>();
        }
    }
}
