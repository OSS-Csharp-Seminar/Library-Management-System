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
                .Where(l => l.Customer == customer)
                .OrderBy(l => l.LoanDate)
                    .ThenBy(l => l.DueDate)
                .AsQueryable();
        }

        public Loan GetById(string id)
        {
            return DbSet
                .Include(l => l.Librarian)
                .Include(l => l.Customer)
                .Include(l => l.Book)
                .Where(l => l.Id.ToString() == id)
                .FirstOrDefault();
        }

        public IQueryable<Loan> GetByLibrarian(ApplicationUser librarian)
        {
            return DbSet
                .Include(l => l.Librarian)
                .Include(l => l.Customer)
                .Include(l => l.Book)
                .Where(l => l.Librarian == librarian)
                .OrderBy(l => l.LoanDate)
                    .ThenBy(l => l.DueDate)
                .AsQueryable();
        }
    }
}
