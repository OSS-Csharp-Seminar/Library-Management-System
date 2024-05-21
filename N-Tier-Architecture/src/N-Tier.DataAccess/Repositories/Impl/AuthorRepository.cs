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
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await DbSet
                .ToListAsync();
        }

        public async Task<Author> GetById(Guid id)
        {
            return await DbSet.Where(a => a.Id == id).FirstOrDefaultAsync();
        }
    }
}
