using AutoMapper;
using N_Tier.Application.Models.Book;
using N_Tier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookResponseModel>();
            CreateMap<BookResponseModel, Book>();
        }
    }
}
