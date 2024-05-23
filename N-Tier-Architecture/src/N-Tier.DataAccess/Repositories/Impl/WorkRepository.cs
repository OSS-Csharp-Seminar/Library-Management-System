using Microsoft.EntityFrameworkCore;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using N_Tier.DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.DataAccess.Repositories.Impl
{
    public class WorkRepository : BaseRepository<Work>, IWorkRepository
    {
        public WorkRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Work> GetAll()
        {
            return DbSet
                .Include(w => w.Author)
                .OrderBy(w => w.Title)
                    .ThenBy(w => w.Description)
                        .ThenBy(w => w.Genre)
                .AsQueryable();

        }

        public IQueryable<Work> GetByAuthor(Author author)
        {
            return DbSet
                .Include(w => w.Author)
                .Where(w => w.Author == author)
                .OrderBy(w => w.Title)
                    .ThenBy(w => w.Description)
                        .ThenBy(w => w.Genre)
                .AsQueryable();
        }

        public async Task<Work> GetById(Guid id)
        {
            return await DbSet
                .Include(w => w.Author)
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
