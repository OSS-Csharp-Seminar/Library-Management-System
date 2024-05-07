using N_Tier.Application.Models.Loan;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services
{
    public interface ILoanService
    {
        Task<LoanResponseModel> CreateAsync(CreateLoanModel createLoanModel);

        Task<IEnumerable<LoanResponseModel>> GetAllAsync(); 

        Task<IEnumerable<LoanResponseModel>> GetByLibrarianAsync(ApplicationUser librarian);

        Task<IEnumerable<LoanResponseModel>> GetByCustomerAsync(ApplicationUser customer);

        Task<Loan> GetByIdAsync(string id); 

        Task<Loan> UpdateAsync(Loan loan);
    }
}
