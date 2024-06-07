using N_Tier.Application.Models.Author;
using N_Tier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services
{
    public interface IAuthorService
    {
        Task<CreateAuthorResponseModel> CreateAsync(CreateAuthorModel createAuthorModel);

        Task<IEnumerable<AuthorResponseModel>> GetAllAsync();

        Task<AuthorResponseModel> GetById(Guid id);

        Task<Author> GetByIdAsync(Guid id);

        Task<UpdateAuthorReponseModel> UpdateAsync(Guid id, AuthorResponseModel updateAuthorModel);

        Task<bool> DeleteAsync(Guid id);

        IEnumerable<AuthorResponseModel> Search(IEnumerable<AuthorResponseModel> authors, string searchString);

        IEnumerable<AuthorResponseModel> FilterByDate(IEnumerable<AuthorResponseModel> authors, DateOnly? filterDateStart, DateOnly? filterDateEnd);

        IEnumerable<AuthorResponseModel> Sort(IEnumerable<AuthorResponseModel> authors, string sortString);
    }
}
