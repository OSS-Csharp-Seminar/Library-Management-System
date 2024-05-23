using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.DataAccess.Repositories
{
    public interface IWorkRepository : IBaseRepository<Work>
    {
        IQueryable<Work> GetAll();

        IQueryable<Work> GetByAuthor(Author author);

        Task<Work> GetById(Guid id);
    }
}
