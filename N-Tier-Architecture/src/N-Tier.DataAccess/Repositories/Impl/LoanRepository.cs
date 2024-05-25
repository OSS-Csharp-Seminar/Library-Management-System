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
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository
    {
        public LoanRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Loan> GetAll()
        {
            return DbSet
                .Include(l => l.Librarian)
                .Include(l => l.Customer)
                .Include(l => l.Book)
                    .ThenInclude(b => b.Work)
                .OrderBy(l => l.LoanDate)
                    .ThenBy(l => l.DueDate)
                .AsQueryable();
        }

        public IQueryable<Loan> GetByCustomer(ApplicationUser customer)
        {
            return DbSet
                .Include(l => l.Librarian)
                .Include(l => l.Customer)
                .Include(l => l.Book)
                    .ThenInclude(b => b.Work)
                .Where(l => l.Customer == customer)
                .OrderBy(l => l.LoanDate)
                    .ThenBy(l => l.DueDate)
                .AsQueryable();
        }

        public async Task<Loan> GetById(Guid id)
        {
            return await DbSet
                .Include(l => l.Librarian)
                .Include(l => l.Customer)
                .Include(l => l.Book)
                    .ThenInclude(b => b.Work)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        public IQueryable<Loan> GetByLibrarian(ApplicationUser librarian)
        {
            return DbSet
                .Include(l => l.Librarian)
                .Include(l => l.Customer)
                .Include(l => l.Book)
                    .ThenInclude(b => b.Work)
                .Where(l => l.Librarian == librarian)
                .OrderBy(l => l.LoanDate)
                    .ThenBy(l => l.DueDate)
                .AsQueryable();
        }
    }
}
