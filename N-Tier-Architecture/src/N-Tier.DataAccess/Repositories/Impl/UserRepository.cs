using Microsoft.EntityFrameworkCore;
using N_Tier.Core.Entities.Identity;
using N_Tier.DataAccess.Persistence;

namespace N_Tier.DataAccess.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<ApplicationUser> DbSet;

        public UserRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = context.Set<ApplicationUser>();
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await DbSet
                .Include(u => u.LibrarianLoan)
                    .ThenInclude(u => u.Customer)
                .FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
