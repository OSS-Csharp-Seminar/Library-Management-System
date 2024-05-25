using AutoMapper;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Book;
using N_Tier.Application.Models.Loan;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using N_Tier.DataAccess.Repositories;
using N_Tier.DataAccess.Repositories.Impl;
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
        private readonly IBookRepository _bookRepository;

        public LoanService(IMapper mapper, ILoanRepository loanRepository, IBookRepository bookRepository)
        {
            _mapper = mapper;
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
        }

        public async Task<CreateLoanResponseModel> CreateAsync(CreateLoanModel createLoanModel)
        {
            var book = await _bookRepository.GetById(createLoanModel.BookId);
            var loan = _mapper.Map<Loan>(createLoanModel);

            loan.Book = book;

            return new CreateLoanResponseModel
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

        public async Task<Loan> GetByIdAsync(Guid id)
        {
            var loan = (await _loanRepository.GetAllAsync(l => l.Id == id)).FirstOrDefault();

            return loan;
        }

        public async Task<IEnumerable<LoanResponseModel>> GetByLibrarianAsync(ApplicationUser librarian)
        {
            var loans = await _loanRepository.GetByLibrarian(librarian).ToListAsync();

            return _mapper.Map<IEnumerable<LoanResponseModel>>(loans);
        }

        public async Task<UpdateLoanResponseModel> UpdateAsync(Guid id, UpdateLoanModel updateLoanModel)
        {
            var loan = await _loanRepository.GetById(id);

            loan.DueDate = updateLoanModel.DueDate;
            loan.Fine = updateLoanModel.Fine;

            return new UpdateLoanResponseModel
            {
                Id = (await _loanRepository.UpdateAsync(loan)).Id,
            };
        }

        public async Task<UpdateLoanResponseModel> UpdateReturnDateAsync(Guid id)
        {
            var loan = await _loanRepository.GetById(id);

            loan.ReturnDate = DateTime.Now;

            return new UpdateLoanResponseModel
            {
                Id = (await _loanRepository.UpdateAsync(loan)).Id,
            };
        }

        public async Task<bool> UpdateFinesAsync(IEnumerable<LoanResponseModel> loanResponseModels)
        {
            foreach (var loanResponseModel in loanResponseModels)
            {
                var loan = await _loanRepository.GetById(loanResponseModel.Id);
                if (DateTime.Now > loan.DueDate && loan.ReturnDate == null)
                {
                    decimal fine = (decimal)(10 * (DateTime.Now - loan.DueDate).TotalDays);

                    loan.Fine = Math.Round(fine, 2);

                    await _loanRepository.UpdateAsync(loan);
                }
            }

            return true;
        }
    }
}
