using Microsoft.EntityFrameworkCore;
using N_Tier.Core.Entities;
using N_Tier.DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.DataAccess.Repositories.Impl
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Book> GetAll()
        {
            return DbSet
                .Include(b => b.Work)
                .OrderBy(b => b.Status)
                    .ThenBy(b => b.Availability)
                    .ThenBy(b => b.ReleaseDate)
                .AsQueryable();
        }

        public Book GetById(string id)
        {
            return DbSet
                .Include(b => b.Work)
                .Where(b => b.Id.ToString() == id)
                .FirstOrDefault();
        }

        public IQueryable<Book> GetByWork(Work work)
        {
            return DbSet
                .Include(b => b.Work)
                .Where(b => b.Work == work)
                .OrderBy(b => b.Status)
                    .ThenBy(b => b.Availability)
                    .ThenBy(b => b.ReleaseDate)
                .AsQueryable();
        }
    }
}
