using AutoMapper;
using Microsoft.EntityFrameworkCore;
using N_Tier.Application.Models.Book;
using N_Tier.Application.Models.Loan;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using N_Tier.DataAccess.Repositories;
using N_Tier.DataAccess.Repositories.Impl;
using Org.BouncyCastle.Utilities;
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

        public IEnumerable<LoanResponseModel> Search(IEnumerable<LoanResponseModel> loans, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                loans = loans.Where(item => item.Book.Work.Title.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Customer.FirstName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Customer.LastName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Librarian.FirstName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                            || item.Librarian.LastName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                            .ToList();
            }

            return loans;
        }

        public IEnumerable<LoanResponseModel> Filter(IEnumerable<LoanResponseModel> loans, DateTime? filterDateStart, DateTime? filterDateEnd, string filterString)
        {
            if (filterString == "LoanDate")
            {
                if (filterDateStart.HasValue)
                {
                    loans = loans.Where(item => item.LoanDate >= filterDateStart);
                }
                if (filterDateEnd.HasValue)
                {
                    loans = loans.Where(item => item.LoanDate <= filterDateEnd);
                }
            }

            if (filterString == "DueDate")
            {
                if (filterDateStart.HasValue)
                {
                    loans = loans.Where(item => item.DueDate >= filterDateStart);
                }
                if (filterDateEnd.HasValue)
                {
                    loans = loans.Where(item => item.DueDate <= filterDateEnd);
                }
            }

            if (filterString == "ReturnDate")
            {
                if (filterDateStart.HasValue)
                {
                    loans = loans.Where(item => item.ReturnDate >= filterDateStart);
                }
                if (filterDateEnd.HasValue)
                {
                    loans = loans.Where(item => item.ReturnDate <= filterDateEnd);
                }
            }

            return loans;
        }

        public IEnumerable<LoanResponseModel> Sort(IEnumerable<LoanResponseModel> loans, string sortString)
        {
            switch (sortString)
            {
                case "TitleDesc":
                    loans = loans.OrderByDescending(item => item.Book.Work.Title).ToList(); break;
                case "LoanDateAsc":
                    loans = loans.OrderBy(item => item.LoanDate).ToList(); break;
                case "LoanDateDesc":
                    loans = loans.OrderByDescending(item => item.LoanDate).ToList(); break;
                case "DueDateAsc":
                    loans = loans.OrderBy(item => item.DueDate).ToList(); break;
                case "DueDateDesc":
                    loans = loans.OrderByDescending(item => item.DueDate).ToList(); break;
                case "ReturnDateAsc":
                    loans = loans.OrderBy(item => item.ReturnDate).ToList(); break;
                case "ReturnDateDesc":
                    loans = loans.OrderByDescending(item => item.ReturnDate).ToList(); break;
                case "FineAsc":
                    loans = loans.OrderBy(item => item.Fine).ToList(); break;
                case "FineDesc":
                    loans = loans.OrderByDescending(item => item.Fine).ToList(); break;
                case "CustomerAsc":
                    loans = loans.OrderBy(item => item.Customer.Email).ToList(); break;
                case "CustomerDesc":
                    loans = loans.OrderByDescending(item => item.Customer.Email).ToList(); break;
                case "LibrarianAsc":
                    loans = loans.OrderBy(item => item.Librarian.Email).ToList(); break;
                case "LibrarianDesc":
                    loans = loans.OrderByDescending(item => item.Librarian.Email).ToList(); break;
                default:
                    loans = loans.OrderBy(item => item.Book.Work.Title).ToList(); break;
            }

            return loans;
        }
    }
}
