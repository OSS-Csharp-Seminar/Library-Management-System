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
        Task<CreateLoanResponseModel> CreateAsync(CreateLoanModel createLoanModel);

        Task<IEnumerable<LoanResponseModel>> GetAllAsync(); 

        Task<IEnumerable<LoanResponseModel>> GetByLibrarianAsync(ApplicationUser librarian);

        Task<IEnumerable<LoanResponseModel>> GetByCustomerAsync(ApplicationUser customer);

        Task<Loan> GetByIdAsync(Guid id); 

        Task<UpdateLoanResponseModel> UpdateAsync(Guid id, UpdateLoanModel updateLoanModel);

        Task<UpdateLoanResponseModel> UpdateReturnDateAsync(Guid id);

        Task<bool> UpdateFinesAsync(IEnumerable<LoanResponseModel> loanResponseModels);

        IEnumerable<LoanResponseModel> Search(IEnumerable<LoanResponseModel> loans, string searchString);

        IEnumerable<LoanResponseModel> Filter(IEnumerable<LoanResponseModel> loans, DateTime? filterDateStart, DateTime? filterDateEnd, string filterString);

        IEnumerable<LoanResponseModel> Sort(IEnumerable<LoanResponseModel> loans, string sortString);
    }
}
