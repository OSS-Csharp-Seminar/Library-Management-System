using N_Tier.Application.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services
{
    public interface IBookService
    {
        Task<CreateBookResponseModel> CreateAsync(CreateBookModel createBookModel);

        Task<IEnumerable<BookResponseModel>> GetAllAsync();

        Task<BookResponseModel> GetById(Guid id);

        Task<UpdateBookResponseModel> UpdateAsync(Guid id, BookResponseModel bookResponseModel);

        Task<bool> DeleteAsync(Guid id);
    }
}
