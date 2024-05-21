using N_Tier.Application.Models.Author;
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

        Task<UpdateAuthorReponseModel> UpdateAsync(Guid id, UpdateAuthorModel updateAuthorModel);

        Task<bool> DeleteAsync(Guid id);
    }
}
