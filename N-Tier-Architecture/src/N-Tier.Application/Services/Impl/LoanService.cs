using AutoMapper;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Loan;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using N_Tier.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier.Application.Services.Impl
{
    public class LoanService : ILoanService
    {
        private readonly IMapper _mapper;

        private readonly ILoanRepository _loanRepository;

        public LoanService(IMapper mapper, ILoanRepository loanRepository)
        {
            _mapper = mapper;
            _loanRepository = loanRepository;
        }

        public async Task<LoanResponseModel> CreateAsync(CreateLoanModel createLoanModel)
        {
            var loan = _mapper.Map<Loan>(createLoanModel);

            return new LoanResponseModel
            {
                Id = (await _loanRepository.AddAsync(loan)).Id
            };
        }

        public async Task<IEnumerable<LoanResponseModel>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<LoanResponseModel>>(loans);
        }

        public async Task<IEnumerable<LoanResponseModel>> GetByCustomerAsync(ApplicationUser customer)
        {
            var loans = await _loanRepository.GetByCustomer(customer).ToListAsync();

            return _mapper.Map<IEnumerable<LoanResponseModel>>(loans);  
        }

        public async Task<Loan> GetByIdAsync(string id)
        {
            var loan = (await _loanRepository.GetAllAsync(l => l.Id.ToString() == id)).FirstOrDefault();

            return loan;
        }

        public async Task<IEnumerable<LoanResponseModel>> GetByLibrarianAsync(ApplicationUser librarian)
        {
            var loans = await _loanRepository.GetByLibrarian(librarian).ToListAsync();

            return _mapper.Map<IEnumerable<LoanResponseModel>>(loans);
        }

        public async Task<Loan> UpdateAsync(Loan loan)
        {
            return await _loanRepository.UpdateAsync(loan);
        }
    }
}
