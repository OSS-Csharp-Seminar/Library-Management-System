using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.DataAccess.Repositories
{
    public interface ILoanRepository : IBaseRepository<Loan>
    {
        IQueryable<Loan> GetAll();

        IQueryable<Loan> GetByLibrarian(ApplicationUser librarian);

        IQueryable<Loan> GetByCustomer(ApplicationUser customer);

        Loan GetById(string id);
    }
}
