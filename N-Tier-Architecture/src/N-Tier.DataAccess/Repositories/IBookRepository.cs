using N_Tier.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.DataAccess.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        IQueryable<Book> GetAll();

        IQueryable<Book> GetByWork(Work work);

        Book GetById(string id);
    }
}
